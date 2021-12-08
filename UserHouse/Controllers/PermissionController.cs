using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using UserHouse.Application.Dtos.Permissions;
using UserHouse.Application.Helpers;
using UserHouse.Application.Models.Permissions;
using UserHouse.Application.Permissions;

namespace UserHouse.Web.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;

        public PermissionController(
            IMapper mapper,
            IPermissionService permissionService)
        {
            _mapper = mapper;
            _permissionService = permissionService;
        }

        [HttpPost]
        [Authorize(Roles = "Super, Admin")]
        [Route("Create")]
        public async Task CreatePermission([FromBody] CreatePermissionDto createPermissionDto)
        {
            var newRole = _mapper.Map<PermissionModel>(createPermissionDto);

            await _permissionService.Create(newRole);
        }

        [HttpGet]
        [Authorize(Roles = "Super, Admin, Basic")]
        [Route("GetById")]
        public async Task<PermissionModel> GetPermissionById([FromHeader] int permissionId)
        {
            return await _permissionService.GetById(permissionId);
        }

        [HttpGet]
        [Authorize(Roles = "Super, Admin, Basic")]
        [Route("GetAll")]
        public async Task<List<PermissionModel>> GetAllPermissions()
        {
            return await _permissionService.GetAll();
        }

        [HttpPut]
        [Authorize(Roles = "Super")]
        [Route("Update")]
        public void UpdatePermission([FromBody] PermissionDto permissionDto)
        {
            var updatedRole = _mapper.Map<PermissionModel>(permissionDto);

            _permissionService.Update(updatedRole);
        }

        [HttpDelete]
        [Authorize(Roles = "Super")]
        [Route("Delete")]
        public void DeletePermission([FromHeader] int permissionId)
        {
            _permissionService.Delete(permissionId);
        }
    }
}
