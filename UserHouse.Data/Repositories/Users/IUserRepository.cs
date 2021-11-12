using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserHouse.Data.Entities;

namespace UserHouse.Data.Repositories.Users
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User GetById(int id);

        void Create(User user);

        void Delete(int id);

        void Update(User user);

        void Save();
    }
}
