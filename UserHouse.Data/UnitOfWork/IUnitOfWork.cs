using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserHouse.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
