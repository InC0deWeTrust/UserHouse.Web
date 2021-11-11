using System;
using System.Collections.Generic;
using System.Text;

namespace UserHouse.Data.ContextDb
{
    public class UserHouseDbFactory : IDisposable
    {
        private bool _disposed;
        private Func<UserHouseDbContext> _instanceFunc;
        private UserHouseDbContext _userHouseDbContext;

        public UserHouseDbFactory(Func<UserHouseDbContext> userHouseDbFactory)
        {
            _instanceFunc = userHouseDbFactory;
        }

        public UserHouseDbContext UserHouseDbContext()
        {
            return _userHouseDbContext ??= _instanceFunc.Invoke();
        }

        public void Dispose()
        {
            if (!_disposed && _userHouseDbContext != null)
            {
                _disposed = true;
                _userHouseDbContext.Dispose();
            }
        }
    }
}
