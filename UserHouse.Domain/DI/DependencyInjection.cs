using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserHouse.Application.Users;
using UserHouse.Data.ContextDb;
using UserHouse.Data.DI;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Repositories.Users;
using UserHouse.Infrastructure.Repositories.Generic;
using UserHouse.Application.Validators.Users;

namespace UserHouse.Application.DI
{
    public static class DependencyInjection
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserDtoValidator>());
            services.AddScoped<IUserService, UserService>();
            services.RegisterDataServices();
        }
    }
}
