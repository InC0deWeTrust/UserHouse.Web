using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Data.Repositories.Users;
using UserHouse.Web.Dtos.Users;

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

        public void Create(CreateUserDto createUserDto)
        {
            var newUser = CustomMapper.mapper.Map<User>(createUserDto);

            newUser.DateOfBirth = DateTime.Now;

            _userRepository.Create(newUser);
        }

        public void Update(UserDto userDto)
        {
            var updatedUser = CustomMapper.mapper.Map<User>(userDto);

            _userRepository.Update(updatedUser);
        }

        public void Delete(int userId)
        {
            _userRepository.Delete(userId);
        }
    }
}
