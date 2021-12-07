using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using UserHouse.Application.Helpers;
using UserHouse.Application.Models;
using UserHouse.Application.Models.Roles;
using UserHouse.Application.Permissions;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Entities.Permissions;
using UserHouse.Infrastructure.Entities.Roles;
using UserHouse.Infrastructure.Entities.Users;
using UserHouse.Infrastructure.Repositories.Generic;

namespace UserHouse.Application.Roles
{
    public class RoleService : IRoleService
    {
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IPermissionService _permissionService;

        public RoleService(
            ILogger<RoleService> logger,
            IMapper mapper,
            IRepository<UserRole> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<Permission> permissionRepository,
            IRepository<RolePermission> rolePermissionRepository,
            IPermissionService permissionService)
        {
            _logger = logger;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionService = permissionService;
        }

        public async Task SetBasicRole(int userId)
        {
            var basicRole = new Role
            {
                Id = Convert.ToInt32(RoleEnum.Basic)
            };

            var newBasicUser = new UserRole
            {
                UserId = userId,
                RoleId = basicRole.Id
            };

            await _userRoleRepository.Add(newBasicUser);
        }

        public async Task AddRoleForUser(int userId, int roleId)
        {
            var existingUserRole = await _userRoleRepository
                .GetAll()
                .AnyAsync(x => x.UserId == userId && x.RoleId == roleId);

            if (existingUserRole)
            {
                throw new CustomUserFriendlyException("User with this role already exist!");
            }

            var newUserRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            await _userRoleRepository.Add(newUserRole);
        }

        public async Task RemoveRoleFromUser(int userId, int roleId)
        {
            var existingUserRole = await _userRoleRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);

            if (existingUserRole == null)
            {
                throw new CustomUserFriendlyException("Unable to find user with this role and remove it!");
            }

            _userRoleRepository.Delete(existingUserRole);
        }

        public async Task<List<RoleModel>> GetRolesOfUser(int userId)
        {
            //Get all existing Ids of roles for specific user
            var specificIdRolesForUser = await _userRoleRepository
                .GetAll()
                .Where(x => x.UserId == userId)
                .Select(y => y.RoleId)
                .ToListAsync();

            //Get all roles for specific user by comparing UserRolesIds with AllRoles
            var specificRolesForUser = await _roleRepository
                .GetAll()
                .Where(x => specificIdRolesForUser.Contains(x.Id))
                .ToListAsync();

            return _mapper.Map<List<RoleModel>>(specificRolesForUser);
        }

        public async Task<List<RoleModel>> GetAll()
        {
            var roles = await _roleRepository
                .GetAll()
                .ToListAsync();

            return _mapper.Map<List<RoleModel>>(roles);
        }

        public async Task<RoleModel> GetById(int roleId)
        {
            var role = await _roleRepository.GetAsync(roleId);

            if (role == null)
            {
                _logger.LogInformation($"Unable to find role with Id: {roleId}");
                throw new CustomUserFriendlyException("Unable to get specified role!");
            }

            return _mapper.Map<RoleModel>(role);
        }

        public async Task Create(RoleModel roleModel)
        {
            //Check for existing role
            var existingRole = await _roleRepository
                .GetAll()
                .AnyAsync(x => x.RoleName == roleModel.RoleName);

            if (existingRole)
            {
                throw new CustomUserFriendlyException("This role already exist!");
            }

            var newRole = _mapper.Map<Role>(roleModel);

            await _roleRepository.Add(newRole);
        }

        public void Update(RoleModel roleModel)
        {
            var currentRole = _roleRepository.Get(roleModel.Id);

            if (currentRole == null)
            {
                _logger.LogError($"Unable to find role with Id: {roleModel.Id} and update it!");
                throw new CustomUserFriendlyException("Unable to get specified role and update it!");
            }

            currentRole.RoleName = roleModel.RoleName;

            _roleRepository.Update(currentRole);
        }

        public void Delete(int roleId)
        {
            var role = _roleRepository.Get(roleId);

            if (role == null)
            {
                _logger.LogError($"Unable to find role with Id: {roleId} and delete it");
                throw new CustomUserFriendlyException("Unable to delete specified role!");
            }

            _roleRepository.Delete(role);
        }

        public async Task AddPermission(int roleId, int permissionId)
        {
            await CheckExistingRolePermission(roleId, permissionId);

            await _permissionService.AddPermissionToRole(roleId, permissionId);
        }

        public async Task RemovePermission(int roleId, int permissionId)
        {
            await CheckExistingRolePermission(roleId, permissionId);

            await _permissionService.RemovePermissionFromRole(roleId, permissionId);
        }

        private async Task CheckExistingRolePermission(int roleId, int permissionId)
        {
            var existingRole = await _roleRepository.GetAsync(roleId);

            var existingPermission = await _permissionRepository.GetAsync(permissionId);

            if (existingRole == null || existingPermission == null)
            {
                _logger.LogError($"Role with Id: {roleId} and permission with Id: {permissionId} don't exist!");
                throw new CustomUserFriendlyException($"Specified role or permission don't exist!");
            }

            var existingRolePermission = await _rolePermissionRepository
                .GetAll()
                .AnyAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);

            if (existingRolePermission)
            {
                _logger.LogError($"Role with Id: {roleId} and permission with Id: {permissionId} already set!");
                throw new CustomUserFriendlyException($"Role already have this permission!");
            }
        }
    }
}
