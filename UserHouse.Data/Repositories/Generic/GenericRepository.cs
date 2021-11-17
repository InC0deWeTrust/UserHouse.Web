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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UserHouseDbContext _userHouseDbContext;

        public GenericRepository(UserHouseDbContext userHouseDbContext)
        {
            _userHouseDbContext = userHouseDbContext;
        }

        public async Task Add(T entity)
        {
            await _userHouseDbContext.Set<T>().AddAsync(entity);
        }
        public async Task<T> Get(int id)
        {
            return await _userHouseDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _userHouseDbContext.Set<T>().ToListAsync();
        }
        public void Update(T entity)
        {
            _userHouseDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _userHouseDbContext.Set<T>().Remove(entity);
        }
    }
}
