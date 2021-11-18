using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserHouse.Infrastructure.Repositories.Generic
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        T Get(int id);

        Task<IEnumerable<T>> GetAll();

        Task Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        void Save();
    }
}
