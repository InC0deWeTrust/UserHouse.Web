using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;

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
            });

            mapper = new Mapper(config);
        }
    }
}
