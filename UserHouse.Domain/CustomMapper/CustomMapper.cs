using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Application.Dtos.Users;
using UserHouse.Application.Models.Roles;
using UserHouse.Infrastructure.Entities.Roles;

namespace UserHouse.Application.CustomMapper
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            //User
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<CreateUserDto, UserModel>().ReverseMap();
            CreateMap<UserDto, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.UserId));

            //Role
            CreateMap<Role, RoleModel>().ReverseMap();
        }
    }
}
