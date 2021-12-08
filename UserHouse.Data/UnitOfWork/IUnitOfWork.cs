using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserHouse.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Complete();
    }
}
