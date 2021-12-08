using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Entities.Roles;

namespace UserHouse.Infrastructure.Entities.Users
{
    [Table("UserRoles")]
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
