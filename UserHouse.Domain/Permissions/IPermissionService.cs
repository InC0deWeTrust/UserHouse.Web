using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserHouse.Application.Models.Permissions;

namespace UserHouse.Application.Permissions
{
    public interface IPermissionService
    {
        Task<List<PermissionModel>> GetAll();

        Task<PermissionModel> GetById(int permissionId);

        Task Create(PermissionModel permissionModel);

        void Update(PermissionModel roleModel);

        void Delete(int permissionId);

        Task AddPermissionToRole(int roleId, int permissionId);

        Task RemovePermissionFromRole(int roleId, int permissionId);
    }
}
