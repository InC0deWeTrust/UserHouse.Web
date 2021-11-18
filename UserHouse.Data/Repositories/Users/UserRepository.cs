using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserHouse.Data.ContextDb;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Repositories.Generic;

namespace UserHouse.Infrastructure.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserHouseDbContext userHouseDbContext)
            :base(userHouseDbContext)
        {
            
        }
    }
}
