using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UserHouse.Infrastructure.Entities.Permissions;
using UserHouse.Infrastructure.Entities.Users;

namespace UserHouse.Infrastructure.Entities.Roles
{
    [Table("Roles")]
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
