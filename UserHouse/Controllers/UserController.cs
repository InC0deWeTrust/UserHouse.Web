using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using UserHouse.Application;
using UserHouse.Data.Entities;
using UserHouse.Application.Models;
using UserHouse.Application.Users;
using UserHouse.Application.Dtos.Users;
using UserHouse.Application.Helpers;
using UserHouse.Infrastructure.Entities.Roles;

namespace UserHouse.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userAppService;

        public UserController(
            IMapper mapper,
            IUserService userAppService)
        {
            _mapper = mapper;
            _userAppService = userAppService;
        }

        [HttpPost]
        [Authorize(Roles = "Super, Admin")]
        [Route("Create")]
        public void CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto != null)
            {
                var newUserModel = _mapper.Map<UserModel>(createUserDto);

                _userAppService.Create(newUserModel);
            }
            else
            {
                throw new CustomUserFriendlyException("Empty data for creating a new user!");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Super, Admin, Basic")]
        [Route("GetById")]
        public async Task<UserModel> GetUserById([FromHeader] int userId)
        {
            if (userId >= 1)
            {
                return await _userAppService.GetById(userId);
            }
            else
            {
                throw new CustomUserFriendlyException("Given id is not valid!");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Super, Admin, Basic")]
        [Route("GetAll")]
        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _userAppService.GetAll();
        }

        [HttpPut]
        [Authorize(Roles = "Super")]
        [Route("Update")]
        public void UpdateUser([FromBody] UserDto userDto)
        {
            if (userDto != null)
            {
                var user = _mapper.Map<UserModel>(userDto);

                _userAppService.Update(user);
            }
            else
            {
                throw new CustomUserFriendlyException("Empty data for updating a user!");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Super")]
        [Route("Delete")]
        public void DeleteUser([FromHeader] int userId)
        {
            if (userId >= 1)
            {
                _userAppService.Delete(userId);
            }
            else
            {
                throw new CustomUserFriendlyException("Given id is not valid!");
            }
        }
    }
}
