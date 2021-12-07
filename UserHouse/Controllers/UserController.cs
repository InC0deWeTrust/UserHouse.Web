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
using UserHouse.Infrastructure.Entities.Permissions;
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
        [Route("Create")]
        public async Task CreateUser([FromBody] CreateUserDto createUserDto)
        {
            //I'm mapping here to show that mapper works on this layer too
            var newUser = _mapper.Map<UserModel>(createUserDto);

            await _userAppService.Create(newUser);
        }

        [HttpGet]
        [Authorize(Roles = "Super, Admin, Basic, Custom")]
        [Route("GetById")]
        public async Task<UserDto> GetUserById([FromHeader] int userId)
        {
            var user = await _userAppService.GetById(userId);

            return _mapper.Map<UserDto>(user);
        }

        [HttpGet]
        [Authorize(Roles = "Super, Admin, Basic")]
        [Route("GetAll")]
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userAppService.GetAll();

            return _mapper.Map<List<UserDto>>(users);
        }

        [HttpPut]
        [Authorize(Roles = "Super, Admin")]
        [Route("Update")]
        public void UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var updatedUser = _mapper.Map<UserModel>(updateUserDto);

            _userAppService.Update(updatedUser);
        }

        [HttpDelete]
        [Authorize(Roles = "Super")]
        [Route("Delete")]
        public void DeleteUser([FromHeader] int userId)
        {
            _userAppService.Delete(userId);
        }

        [HttpPost]
        [Authorize(Roles = "Super")]
        [Route("AddRoleForUser")]
        public async Task AddRoleForUser([FromHeader] int userId, int roleId)
        {
            await _userAppService.AddRole(userId, roleId);
        }

        [HttpDelete]
        [Authorize(Roles = "Super")]
        [Route("RemoveRoleFromUser")]
        public async Task RemoveRoleFromUser([FromHeader] int userId, int roleId)
        {
            await _userAppService.RemoveRole(userId, roleId);
        }

        [HttpPut]
        [Authorize(Roles = "Super, Admin, Basic")]
        [Route("ChangePassword")]
        public async Task ChangePassword([FromBody] UpdateUserPasswordDto updateUserPasswordDto)
        {
            var updatedUser = _mapper.Map<UserModel>(updateUserPasswordDto);

            await _userAppService.ChangePassword(updatedUser);
        }
    }
}
