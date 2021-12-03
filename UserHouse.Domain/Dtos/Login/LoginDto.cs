using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserHouse.Application.Dtos.Login
{
    public class LoginDto
    {
        public string FirstName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
