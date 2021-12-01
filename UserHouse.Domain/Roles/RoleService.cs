using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Entities.Roles;
using UserHouse.Infrastructure.Entities.Users;
using UserHouse.Infrastructure.Repositories.Generic;

namespace UserHouse.Application.Roles
{
    public class RoleService
    {
        private readonly ILogger<RoleService> _logger;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<User> _userRepository;

        public RoleService(
            ILogger<RoleService> logger,
            IRepository<UserRole> userRoleRepository,
            IRepository<User> userRepository)
        {
            _logger = logger;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        public async Task SetBasicUserRole()
        {
            var users = await _userRepository.GetAll();
            var user = users.LastOrDefault();

            if (user == null)
            {
                _logger.LogInformation("Unable to find last user and set role for him");
                throw new Exception("Unable to set role for the user");
            }

            var basicRole = new Role {Id = Convert.ToInt32(RoleEnum.Basic)};

            var newBasicUser = new UserRole
            {
                UserId = user.Id,
                RoleId = basicRole.Id
            };

            await _userRoleRepository.Add(newBasicUser);
        }
    }
}
