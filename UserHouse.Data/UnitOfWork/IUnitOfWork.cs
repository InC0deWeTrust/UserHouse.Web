using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserHouse.Data.Repositories.Users;

namespace UserHouse.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Complete();
    }
}
