using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Data.Repositories.Users;

namespace UserHouse.Application.Users
{
    public class UserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserModel> GetAll()
        {
            var users = _userRepository.GetAll();

            return CustomMapper.mapper.Map<List<UserModel>>(users);
        }

        public UserModel GetById(int userId)
        {
            User user = _userRepository.GetById(userId);

            return CustomMapper.mapper.Map<UserModel>(user);
        }

        public void Create(UserModel userModel)
        {
            var newUser = CustomMapper.mapper.Map<User>(userModel);

            _userRepository.Create(newUser);
        }

        public void Update(UserModel userModel, int userId)
        {
            var updatedUser = CustomMapper.mapper.Map<User>(userModel);

            _userRepository.Update(userId, updatedUser);
        }

        public void Delete(int userId)
        {
            _userRepository.Delete(userId);
        }
    }
}
