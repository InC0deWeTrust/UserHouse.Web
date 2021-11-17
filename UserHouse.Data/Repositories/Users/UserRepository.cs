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

namespace UserHouse.Data.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserHouseDbContext userHouseDbContext)
            :base(userHouseDbContext)
        {
            
        }
        //private readonly UserHouseDbContext _userHouseDbContext;

        //public UserRepository()
        //{
        //    _userHouseDbContext = new UserHouseDbContext();
        //}

        //public void Create(User user)
        //{
        //    _userHouseDbContext.Users.Add(user);

        //    Save();
        //}

        //public void Delete(int id)
        //{
        //    var user = _userHouseDbContext.Users.Find(id);

        //    _userHouseDbContext.Users.Remove(user);

        //    Save();
        //}

        //public List<User> GetAll()
        //{
        //    return _userHouseDbContext.Users.ToList();
        //}

        //public User GetById(int id)
        //{
        //    return _userHouseDbContext.Users.Find(id);
        //}

        //public void Save()
        //{
        //    _userHouseDbContext.SaveChanges();
        //}

        ////TODO: Think of better way to do
        //public void Update(User user)
        //{
        //    User oldUser = _userHouseDbContext.Users.Find(user.Id);

        //    oldUser.FirstName = user.FirstName;
        //    oldUser.LastName = user.LastName;
        //    oldUser.DateOfBirth = user.DateOfBirth;

        //    _userHouseDbContext.Users.Update(oldUser);

        //    Save();
        //}
    }
}
