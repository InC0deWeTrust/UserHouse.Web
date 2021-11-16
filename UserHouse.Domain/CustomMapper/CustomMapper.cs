using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;

namespace UserHouse.Application.CustomMapper
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
