using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserHouse.Data.Entities;
using UserHouse.Application.Models;
using UserHouse.Application.Users;

namespace UserHouse.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserAppService _userAppService;

        public UserController(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        //TODO: Change parameters to model 
        [HttpPost]
        [Route("Create")]
        public void CreateUser(string firstName, string lastName, DateTime dateOfBirth)
        {
            var newUserModel = new UserModel
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            _userAppService.Create(newUserModel);
        }

        [HttpGet]
        [Route("GetById")]
        public UserModel GetUserById(int userId)
        {
            return _userAppService.GetById(userId);
        }

        [HttpGet]
        [Route("GetAll")]
        public List<UserModel> GetAllUsers()
        {
            return _userAppService.GetAll();
        }

        [HttpPut]
        [Route("UpdateUser")]
        public void UpdateUser(int userId, string firstName, string lastName, string dateOfBirth)
        {
            var userModel = new UserModel
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = Convert.ToDateTime(dateOfBirth)
            };

            _userAppService.Update(userModel, userId);
        }
    }
}
