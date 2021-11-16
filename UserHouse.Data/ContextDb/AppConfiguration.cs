using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserHouse.Data.ContextDb;

namespace UserHouse.Infrastructure.ContextDb
{
    public class AppConfiguration
    {
        public string ConnectionString { get; set; }

        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            configBuilder.AddJsonFile(path);

            var root = configBuilder.Build();

            var appSetting = root.GetSection("ConnectionStrings:Default");

            ConnectionString = appSetting.Value;
        }
    }
}
