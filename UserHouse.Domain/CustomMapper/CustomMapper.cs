using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Application.Dtos.Users;

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
        }
    }
}
