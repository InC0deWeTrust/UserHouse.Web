using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
            //TODO: Think of it
            //if (user == null)
            //{
            //    _logger.LogInformation("Unable to find user and set role for him");
            //    throw new CustomUserFriendlyException($"Unable to set role for the user with Id: {userId}");
            //}

            var basicRole = new Role {Id = Convert.ToInt32(RoleEnum.Basic)};

            var newBasicUser = new UserRole
            {
                UserId = userId,
                RoleId = basicRole.Id
            };

            await _userRoleRepository.Add(newBasicUser);
            //_userRoleRepository.Save();
        }

        public async Task<List<RoleModel>> GetRolesOfUser(int userId)
        {
            var allUserRoles = await _userRoleRepository.GetAll();

            var specificIdRolesForUser = allUserRoles
                .Where(x => x.UserId == userId)
                .Select(y => y.RoleId)
                .ToList();

            var allRoles = await _roleRepository.GetAll();

            //TODO: Solve it tmr
            //var specificRolesForUser = allRoles
            //    .Where(x => x.Id == allRoles)

            return _mapper.Map<List<RoleModel>>(specificRolesForUser);
        }
    }
}
