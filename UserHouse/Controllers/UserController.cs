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
        //[Authorize(Roles = "Super, Admin")]
        [Route("Create")]
        public void CreateUser([FromBody] CreateUserDto createUserDto)
        {
            //throw new CustomUserFriendlyException("Given id is not valid!");
            _userAppService.Create(_mapper.Map<UserModel>(createUserDto));
        }

        [HttpGet]
        //[Authorize(Roles = "Super, Admin, Basic")]
        [Route("GetById")]
        public async Task<UserModel> GetUserById([FromHeader] int userId)
        {
            if (userId <= 0)
            {
                throw new CustomUserFriendlyException("Given id is not valid!");
            }

            return await _userAppService.GetById(userId);
        }

        [HttpGet]
        //[Authorize(Roles = "Super, Admin, Basic")]
        [Route("GetAll")]
        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _userAppService.GetAll();
        }

        [HttpPut]
        //[Authorize(Roles = "Super")]
        [Route("Update")]
        public void UpdateUser([FromBody] UserDto userDto)
        {
            _userAppService.Update(_mapper.Map<UserModel>(userDto));
        }

        [HttpDelete]
        //[Authorize(Roles = "Super")]
        [Route("Delete")]
        public void DeleteUser([FromHeader] int userId)
        {
            if (userId <= 0)
            {
                throw new CustomUserFriendlyException("Given id is not valid!");
            }

            _userAppService.Delete(userId);
        }
    }
}
