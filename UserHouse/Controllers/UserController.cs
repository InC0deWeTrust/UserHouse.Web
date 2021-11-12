using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserHouse.Application;
using UserHouse.Data.Entities;
using UserHouse.Application.Models;
using UserHouse.Application.Users;
using UserHouse.Web.Dtos.Users;

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

        [HttpPost]
        [Route("Create")]
        public void CreateUser([FromBody] CreateUserDto createUserDto)
        {
            _userAppService.Create(createUserDto);
        }

        [HttpGet]
        [Route("GetById")]
        public UserModel GetUserById([FromHeader] int userId)
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
        [Route("Update")]
        public void UpdateUser([FromBody] UserDto userDto)
        {
            _userAppService.Update(userDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public void DeleteUser([FromHeader] int userId)
        {
            _userAppService.Delete(userId);
        }
    }
}
