using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Extensions;
using UserHouse.Application.Dtos.Roles;
using UserHouse.Application.Helpers;
using UserHouse.Application.Models.Roles;
using UserHouse.Application.Roles;
using UserHouse.Infrastructure.Entities.Roles;

namespace UserHouse.Web.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RoleController(
            IMapper mapper,
            IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        [HttpPost]
        [Authorize(Roles = "Super, Admin")]
        [Route("Create")]
        public async Task CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            var newRole = _mapper.Map<RoleModel>(createRoleDto);

            await _roleService.Create(newRole);
        }

        [HttpGet]
        [Authorize(Roles = "Super, Admin, Basic, Custom")]
        [Route("GetById")]
        public async Task<RoleModel> GetRoleById([FromHeader] int roleId)
        {
            return await _roleService.GetById(roleId);
        }

        [HttpGet]
        [Authorize(Roles = "Super, Admin, Basic")]
        [Route("GetAll")]
        public async Task<List<RoleModel>> GetAllRoles()
        {
            return await _roleService.GetAll();
        }

        [HttpPut]
        [Authorize(Roles = "Super")]
        [Route("Update")]
        public void UpdateRole([FromBody] RoleDto roleDto)
        {
            var updatedRole = _mapper.Map<RoleModel>(roleDto);

            _roleService.Update(updatedRole);
        }

        [HttpDelete]
        [Authorize(Roles = "Super")]
        [Route("Delete")]
        public void DeleteRole([FromHeader] int roleId)
        {
            _roleService.Delete(roleId);
        }

        [HttpPost]
        [Authorize(Roles = "Super")]
        [Route("AddPermissionForRole")]
        public async Task AddPermissionForRole([FromHeader] int roleId, int permissionId)
        {
            await _roleService.AddPermission(roleId, permissionId);
        }

        [HttpDelete]
        [Authorize(Roles = "Super")]
        [Route("RemovePermissionFromRole")]
        public async Task RemovePermissionFromRole([FromHeader] int roleId, int permissionId)
        {
            await _roleService.RemovePermission(roleId, permissionId);
        }
    }
}
