using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserHouse.Application.Models.Roles;

namespace UserHouse.Application.Roles
{
    public interface IRoleService
    {
        Task SetBasicRole(int userId);

        Task<List<RoleModel>> GetRolesOfUser(int userId);
    }
}
