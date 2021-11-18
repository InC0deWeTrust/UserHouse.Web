using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using UserHouse.Application.Models;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Repositories.Users;
using UserHouse.Infrastructure.Repositories.Generic;

namespace UserHouse.Application.Users
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            ILogger<UserService> logger,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> GetAll()
        {
            var users = await _userRepository.GetAll();

            if (users == null)
            {
                _logger.LogInformation("List of users is empty");
                throw new Exception("Unable to get users!");
            }

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel> GetById(int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null)
            {
                _logger.LogInformation($"Unable to find user with Id: {userId}");
                throw new Exception("Unable to get specified user!");
            }

            return _mapper.Map<UserModel>(user);
        }

        public void Create(UserModel userModel)
        {
            if (userModel == null)
            {
                _logger.LogInformation("Unable to create a new user due to empty data in UserModel");
                throw new Exception("Unable to create a new user!");
            }

            var newUser = _mapper.Map<User>(userModel);

            newUser.DateOfBirth = DateTime.Now;

            _userRepository.Add(newUser);
        }

        public void Update(UserModel userModel)
        {
            if (userModel == null)
            {
                _logger.LogInformation("Unable to update user due to empty data in UserModel");
                throw new Exception("Unable to update specified user!");
            }

            var updatedUser = _mapper.Map<User>(userModel);

            _userRepository.Update(updatedUser);
        }

        public void Delete(int userId)
        {
            var user = _userRepository.Get(userId);

            if (user == null)
            {
                _logger.LogInformation($"Unable to find user with Id: {userId} and delete it");
                throw new Exception("Unable to delete specified user!");
            }

            _userRepository.Delete(user);
        }
    }
}
