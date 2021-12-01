using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UserHouse.Infrastructure.Entities;
using UserHouse.Infrastructure.Entities.Users;

namespace UserHouse.Data.Entities
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
