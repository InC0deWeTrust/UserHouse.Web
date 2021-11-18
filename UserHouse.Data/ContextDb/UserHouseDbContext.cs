using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserHouse.Data.Entities;

namespace UserHouse.Data.ContextDb
{
    public class UserHouseDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public UserHouseDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserHouseDbContext(DbContextOptions<UserHouseDbContext> options) 
            :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_configuration.GetConnectionString("Default"));
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
