using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserHouse.Web.Dtos.Users
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
