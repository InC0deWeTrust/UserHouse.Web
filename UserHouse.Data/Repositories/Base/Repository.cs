using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserHouse.Data.ContextDb;

namespace UserHouse.Data.Repositories.Base
{
    public class Repository : IRepository<Repository>
    {
        private readonly UserHouseDbFactory _userHouseDbFactory;
        private readonly UserHouseDbContext _userHouseDbContext;

        protected UserHouseDbContext DbSet
        {
            get => _userHouseDbFactory.UserHouseDbContext();
        }

        public void Add(Repository entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(Repository entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable GetAll(Repository entity)
        {
            return null;
        }

        public void Update(Repository entity)
        {
            DbSet.Update(entity);
            DbSet.SaveChanges();
        }
    }
}
