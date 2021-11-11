using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserHouse.Data.Entities;

namespace UserHouse.Data.ContextDb
{
    public class UserHouseDbContext : DbContext
    {
        public UserHouseDbContext()
        {
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
