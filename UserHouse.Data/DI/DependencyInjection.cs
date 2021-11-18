using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserHouse.Data.ContextDb;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Repositories.Users;
using UserHouse.Infrastructure.Repositories.Generic;
using UserHouse.Infrastructure.UnitOfWork;

namespace UserHouse.Data.DI
{
    public static class DependencyInjection
    {
        public static void RegisterDataServices(this IServiceCollection services)
        {
            services.AddScoped<UserHouseDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
