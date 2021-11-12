using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Web.Dtos.Users;

namespace UserHouse.Application
{
    public static class CustomMapper
    {
        public static Mapper mapper;

        static CustomMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>().ReverseMap();
                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<UserDto, User>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.UserId));
            });

            mapper = new Mapper(config);
        }
    }
}
