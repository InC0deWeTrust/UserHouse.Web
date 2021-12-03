using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using UserHouse.Application.Helpers;
using UserHouse.Application.Models;
using UserHouse.Application.Models.Roles;
using UserHouse.Data.Entities;
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
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public RoleService(
            ILogger<RoleService> logger,
            IMapper mapper,
            IRepository<UserRole> userRoleRepository,
            IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task SetBasicRole(int userId)
        {
            var basicRole = new Role {Id = Convert.ToInt32(RoleEnum.Basic)};

            var newBasicUser = new UserRole
            {
                UserId = userId,
                RoleId = basicRole.Id
            };

            await _userRoleRepository.Add(newBasicUser);
        }

        public async Task<List<RoleModel>> GetRolesOfUser(int userId)
        {
            var allUserRoles = await _userRoleRepository.GetAll();

            //Get all existing Ids of roles for specific user
            var specificIdRolesForUser = allUserRoles
                .Where(x => x.UserId == userId)
                .Select(y => y.RoleId)
                .ToList();

            var allRoles = await _roleRepository.GetAll();

            //Get all roles for specific user by comparing UserRolesIds with AllRoles
            var specificRolesForUser = allRoles
                .Where(x => specificIdRolesForUser.Contains(x.Id));

            return _mapper.Map<List<RoleModel>>(specificRolesForUser);
        }
    }
}
