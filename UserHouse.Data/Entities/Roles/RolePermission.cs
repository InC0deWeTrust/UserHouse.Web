using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UserHouse.Infrastructure.Entities.Permissions;

namespace UserHouse.Infrastructure.Entities.Roles
{
    [Table("RolePermissions")]
    public class RolePermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}
