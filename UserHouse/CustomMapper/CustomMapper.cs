using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Web.Host.Dtos.Users;

namespace UserHouse.Web.Host.CustomMapper
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<CreateUserDto, UserModel>().ReverseMap();
            CreateMap<UserDto, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.UserId));
        }
    }
}
