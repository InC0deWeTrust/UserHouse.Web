using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserHouse.Data.ContextDb;

namespace UserHouse.Infrastructure.UnitOfWork
{
    //UnitOfWork acts as a business transaction.
    //In other words it should merge all of the CRUD operations
    //of Repositories into a single transaction.
    //All changes will be committed only once.
    //Could be considered as lazy evaluation transaction
    //because it will not block any data table until it commits the changes.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserHouseDbContext _userHouseDbContext;

        public UnitOfWork(UserHouseDbContext userHouseDbContext)
        {
            _userHouseDbContext = userHouseDbContext;
        }

        public Task Complete()
        {
            return _userHouseDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userHouseDbContext.Dispose();
            }
        }
    }
}
