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

        Task AddRoleForUser(int userId, int roleId);

        Task RemoveRoleFromUser(int userId, int roleId);

        Task<List<RoleModel>> GetRolesOfUser(int userId);

        Task<List<RoleModel>> GetAll();

        Task<RoleModel> GetById(int roleId);

        Task Create(RoleModel roleModel);

        void Update(RoleModel roleModel);

        void Delete(int roleId);

        Task AddPermission(int roleId, int permissionId);

        Task RemovePermission(int roleId, int permissionId);
    }
}
