using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserHouse.Data.ContextDb;
using UserHouse.Data.Entities;

namespace UserHouse.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserHouseDbContext _userHouseDbContext;

        public UserRepository()
        {
            _userHouseDbContext = new UserHouseDbContext();
        }

        public void Create(User user)
        {
            _userHouseDbContext.Users.Add(user);
        }

        public void Delete(int id)
        {
            var user = _userHouseDbContext.Users.Find(id);
            _userHouseDbContext.Users.Remove(user);
        }

        public List<User> GetAll()
        {
            return _userHouseDbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _userHouseDbContext.Users.Find(id);
        }

        public void Save()
        {
            _userHouseDbContext.SaveChanges();
        }

        public void Update(int id, User user)
        {
            User oldUser = _userHouseDbContext.Users.Find(id);
            oldUser = user;
            _userHouseDbContext.Users.Update(oldUser);
        }
    }
}
