using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UserHouse.Infrastructure.Entities.Roles;

namespace UserHouse.Infrastructure.Entities.Permissions
{
    [Table("Permissions")]
    public class Permission
    {
        public int Id { get; set; }

        public string PermissionName { get; set; }

        public ICollection<RolePermission> RolesPermissions { get; set; }
    }
}
