﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using UserHouse.Data.Repositories.Users;

namespace UserHouse.Application.DI
{
    public static class DependencyInjection
    {
        public static void RegisterDomainServices(this IServiceCollection collection)
        {
            collection.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
