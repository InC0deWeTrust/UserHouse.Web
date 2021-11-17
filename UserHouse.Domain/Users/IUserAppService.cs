using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserHouse.Application.Models;

namespace UserHouse.Application.Users
{
    public interface IUserAppService
    {
        List<UserModel> GetAll();

        Task<UserModel> GetById(int userId);

        void Update(UserModel userDto);

        void Create(UserModel createUserDto);

        void Delete(int userId);
    }
}
