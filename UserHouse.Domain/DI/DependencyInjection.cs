using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserHouse.Application.Users;
using UserHouse.Data.ContextDb;
using UserHouse.Data.DI;
using UserHouse.Data.Repositories.Users;

namespace UserHouse.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IUserAppService, UserAppService>();
            services.RegisterDataServices();

            return services;
        }
    }
}
