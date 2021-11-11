using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Data.Repositories.Users;

namespace UserHouse.Application.Users
{
    public class UserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //TODO: AUTOMAPPER
        public List<UserModel> GetAll()
        {
            var users = _userRepository.GetAll();

            var userModels = users
                .Select(user => new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            })
                .ToList();

            return userModels;
        }

        public UserModel GetById(int userId)
        {
            User user = _userRepository.GetById(userId);

            UserModel userModel = new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };

            return userModel;
        }

        public void Create(UserModel userModel)
        {
            User newUser = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                DateOfBirth = userModel.DateOfBirth
            };

            _userRepository.Create(newUser);
        }

        public void Update(UserModel userModel, int userId)
        {
            User updatedUser = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                DateOfBirth = userModel.DateOfBirth
            };

            _userRepository.Update(userId, updatedUser);
        }

        public void Delete(int userId)
        {
            _userRepository.Delete(userId);
        }
    }
}
