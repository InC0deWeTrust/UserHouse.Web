using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UserHouse.Application.Dtos.Permissions;
using UserHouse.Application.Dtos.Roles;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Application.Dtos.Users;
using UserHouse.Application.Models.Permissions;
using UserHouse.Application.Models.Roles;
using UserHouse.Infrastructure.Entities.Permissions;
using UserHouse.Infrastructure.Entities.Roles;

namespace UserHouse.Application.CustomMapper
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            //Users
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<CreateUserDto, UserModel>().ReverseMap();
            CreateMap<UserDto, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.UserId))
                .ReverseMap();
            CreateMap<UpdateUserDto, UserModel>();
            CreateMap<UpdateUserPasswordDto, UserModel>();

            //Roles
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<CreateRoleDto, RoleModel>().ReverseMap();
            CreateMap<RoleDto, RoleModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.RoleId))
                .ReverseMap();

            //Permissions
            CreateMap<Permission, PermissionModel>().ReverseMap();
            CreateMap<CreatePermissionDto, PermissionModel>().ReverseMap();
            CreateMap<PermissionDto, PermissionModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.PermissionId))
                .ReverseMap();
        }
    }
}
