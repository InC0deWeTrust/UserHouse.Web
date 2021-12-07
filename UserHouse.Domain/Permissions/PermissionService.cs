using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserHouse.Application.Helpers;
using UserHouse.Application.Models.Permissions;
using UserHouse.Infrastructure.Entities.Permissions;
using UserHouse.Infrastructure.Entities.Roles;
using UserHouse.Infrastructure.Repositories.Generic;

namespace UserHouse.Application.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly ILogger<PermissionService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IRepository<Permission> _permissionRepository;
        public PermissionService(
            ILogger<PermissionService> logger,
            IMapper mapper,
            IRepository<RolePermission> rolePermissionRepository,
            IRepository<Permission> permissionRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task Create(PermissionModel permissionModel)
        {
            //Check for existing permission
            var existingPermission = await _permissionRepository
                .GetAll()
                .AnyAsync(x => x.PermissionName == permissionModel.PermissionName);

            if (existingPermission)
            {
                throw new CustomUserFriendlyException("This permission already exist!");
            }

            var newPermission = _mapper.Map<Permission>(permissionModel);

            await _permissionRepository.Add(newPermission);
        }

        public void Delete(int permissionId)
        {
            var permission = _permissionRepository.Get(permissionId);

            if (permission == null)
            {
                _logger.LogError($"Unable to find permission with Id: {permissionId} and delete it");
                throw new CustomUserFriendlyException("Unable to delete specified permission!");
            }

            _permissionRepository.Delete(permission);
        }

        public async Task<List<PermissionModel>> GetAll()
        {
            var permissions = await _permissionRepository
                .GetAll()
                .ToListAsync();

            return _mapper.Map<List<PermissionModel>>(permissions);
        }

        public async Task<PermissionModel> GetById(int permissionId)
        {
            var permission = await _permissionRepository.GetAsync(permissionId);

            if (permission == null)
            {
                _logger.LogError($"Unable to find permission with Id: {permissionId}");
                throw new CustomUserFriendlyException("Unable to get specified permission!");
            }

            return _mapper.Map<PermissionModel>(permission);
        }

        public void Update(PermissionModel permissionModel)
        {
            var currentPermission = _permissionRepository.Get(permissionModel.Id);

            if (currentPermission == null)
            {
                _logger.LogError($"Unable to find permission with Id: {permissionModel.Id} and update it!");
                throw new CustomUserFriendlyException("Unable to get specified permission and update it!");
            }

            currentPermission.PermissionName = permissionModel.PermissionName;

            _permissionRepository.Update(currentPermission);
        }

        public async Task AddPermissionToRole(int roleId, int permissionId)
        {
            var existingRolePermission = await _rolePermissionRepository
                .GetAll()
                .AnyAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);

            if (existingRolePermission)
            {
                throw new CustomUserFriendlyException("Role with this permission already exist!");
            }

            var newRolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            await _rolePermissionRepository.Add(newRolePermission);
        }

        public async Task RemovePermissionFromRole(int roleId, int permissionId)
        {
            var existingRolePermission = await _rolePermissionRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);

            if (existingRolePermission == null)
            {
                throw new CustomUserFriendlyException("Unable to find role with this permission and remove it!");
            }

            _rolePermissionRepository.Delete(existingRolePermission);
        }
    }
}
