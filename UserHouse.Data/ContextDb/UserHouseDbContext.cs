using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Entities.Permissions;
using UserHouse.Infrastructure.Entities.Roles;
using UserHouse.Infrastructure.Entities.Users;

namespace UserHouse.Data.ContextDb
{
    public class UserHouseDbContext : DbContext
    {
        public UserHouseDbContext(DbContextOptions<UserHouseDbContext> options) 
            :base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(z => z.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(z => z.UserId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(x => x.Role)
                .WithMany(xa => xa.RolePermissions)
                .HasForeignKey(y => y.PermissionId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(x => x.Permission)
                .WithMany(xa => xa.RolesPermissions)
                .HasForeignKey(y => y.RoleId);
        }
    }
}
