using Microsoft.Extensions.DependencyInjection;
using UserHouse.Data.Repositories.Users;

namespace UserHouse.Data.DI
{
    public static class DependencyInjection
    {
        public static void RegisterDataServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
