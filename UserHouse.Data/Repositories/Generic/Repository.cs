using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserHouse.Data.ContextDb;

namespace UserHouse.Infrastructure.Repositories.Generic
{
    //Basically just a class that implements data access logic.
    //It should be implemented in data access layer and provide data access service to the app.
    public class Repository<T> : IRepository<T> where T : class
    {
        private UserHouseDbContext _userHouseDbContext;

        public Repository(UserHouseDbContext userHouseDbContext)
        {
            _userHouseDbContext = userHouseDbContext;
        }

        public async Task Add(T entity)
        {
            await _userHouseDbContext.Set<T>().AddAsync(entity);
            Save();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _userHouseDbContext.Set<T>().FindAsync(id);
        }

        public T Get(int id)
        {
            return _userHouseDbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _userHouseDbContext.Set<T>();
        }

        public void Update(T entity)
        {
            _userHouseDbContext.Set<T>().Update(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _userHouseDbContext.Set<T>().Remove(entity);
            Save();
        }

        public void Save()
        {
            _userHouseDbContext.SaveChanges();
        }
    }
}
