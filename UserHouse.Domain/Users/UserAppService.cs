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
using UserHouse.Infrastructure.Repositories.Generic;

namespace UserHouse.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserAppService(
            IGenericRepository<User> userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<UserModel> GetAll()
        {
            var users = _userRepository.GetAll();

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel> GetById(int userId)
        {
            var user = await _userRepository.Get(userId);

            return _mapper.Map<UserModel>(user);
        }

        public void Create(UserModel createUserDto)
        {
            var newUser = _mapper.Map<User>(createUserDto);

            newUser.DateOfBirth = DateTime.Now;

            _userRepository.Add(newUser);
        }

        public void Update(UserModel userDto)
        {
            var updatedUser = _mapper.Map<User>(userDto);

            _userRepository.Update(updatedUser);
        }

        public void Delete(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);

            _userRepository.Delete(user);
        }

        public void DoSomethingInAppService()
        {
            
        }
    }
}
