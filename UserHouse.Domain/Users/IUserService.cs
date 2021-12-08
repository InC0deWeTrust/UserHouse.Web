using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserHouse.Application.Models;

namespace UserHouse.Application.Users
{
    public interface IUserService
    {
        Task Create(UserModel createUserDto);

        Task<List<UserModel>> GetAll();

        Task<UserModel> GetById(int userId);

        void Update(UserModel userDto);

        void Delete(int userId);

        Task AddRole(int userId, int roleId);

        Task RemoveRole(int userId, int roleId);

        Task ChangePassword(UserModel userModel);
    }
}
