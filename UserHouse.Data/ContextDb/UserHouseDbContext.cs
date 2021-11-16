using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.ContextDb;

namespace UserHouse.Data.ContextDb
{
    public class UserHouseDbContext : DbContext
    {
        //public DbContextOptionsBuilder<UserHouseDbContext> DbContextOptionsBuilder { get; set; }

        //public DbContextOptions<UserHouseDbContext> DbContextOptions { get; set; }

        //private AppConfiguration  Settings { get; set; }

        public UserHouseDbContext()
        {
            //Settings = new AppConfiguration();
            //DbContextOptionsBuilder = new DbContextOptionsBuilder<UserHouseDbContext>();
            //DbContextOptionsBuilder.UseMySql(Settings.ConnectionString);
            //DbContextOptions = DbContextOptionsBuilder.Options;
        }

        public UserHouseDbContext(DbContextOptions<UserHouseDbContext> options)
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost; Database=userhouseDb; user=root; password=123qwe;");
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
