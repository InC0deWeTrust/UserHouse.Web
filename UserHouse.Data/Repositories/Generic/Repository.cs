using System;
using System.Collections.Generic;
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
        private DbSet<T> _dbSet;

        public Repository(UserHouseDbContext userHouseDbContext)
        {
            _userHouseDbContext = userHouseDbContext;
            _dbSet = _userHouseDbContext.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            Save();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            Save();
        }

        public void Save()
        {
            _userHouseDbContext.SaveChanges();
        }
    }
}
