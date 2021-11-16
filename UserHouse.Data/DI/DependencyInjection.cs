using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserHouse.Data.ContextDb;
using UserHouse.Data.Entities;
using UserHouse.Data.Repositories.Users;
using UserHouse.Infrastructure.Repositories.Generic;
using UserHouse.Infrastructure.UnitOfWork;

namespace UserHouse.Data.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
