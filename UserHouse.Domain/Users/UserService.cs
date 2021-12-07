using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using UserHouse.Application.Helpers;
using UserHouse.Application.Models;
using UserHouse.Application.Roles;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Entities.Roles;
using UserHouse.Infrastructure.Entities.Users;
using UserHouse.Infrastructure.Repositories.Generic;

namespace UserHouse.Application.Users
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UserService(
            ILogger<UserService> logger,
            IRepository<User> userRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole> userRoleRepository,
            IMapper mapper,
            IRoleService roleService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<List<UserModel>> GetAll()
        {
            var users = await _userRepository
                .GetAll()
                .ToListAsync();

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel> GetById(int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null)
            {
                _logger.LogError($"Unable to find user with Id: {userId}");
                throw new CustomUserFriendlyException("Unable to get specified user!");
            }

            return _mapper.Map<UserModel>(user);
        }

        public async Task Create(UserModel userModel)
        {
            //Check for existing email
            var existingEmail = await _userRepository
                .GetAll()
                .AnyAsync(x => x.Email == userModel.Email);

            if (existingEmail)
            {
                throw new CustomUserFriendlyException("This email already exist!");
            }

            var newUser = _mapper.Map<User>(userModel);

            newUser.DateOfBirth = DateTime.Now;

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            await _userRepository.Add(newUser);

            //Set basic role for him
            await _roleService.SetBasicRole(newUser.Id);
        }

        public void Update(UserModel userModel)
        {
            var currentUser = _userRepository.Get(userModel.Id);

            if (currentUser == null)
            {
                _logger.LogError($"Unable to find user with Id: {userModel.Id} and update him!");
                throw new CustomUserFriendlyException("Unable to get specified user and update him!");
            }

            currentUser.FirstName = userModel.FirstName;
            currentUser.LastName = userModel.LastName;
            currentUser.DateOfBirth = userModel.DateOfBirth;
            currentUser.Email = userModel.Email;

            _userRepository.Update(currentUser);
        }

        public void Delete(int userId)
        {
            var user = _userRepository.Get(userId);

            if (user == null)
            {
                _logger.LogError($"Unable to find user with Id: {userId} and delete it");
                throw new CustomUserFriendlyException("Unable to find and delete specified user!");
            }

            _userRepository.Delete(user);
        }

        public async Task AddRole(int userId, int roleId)
        {
            await CheckExistingUserRole(userId, roleId);

            await _roleService.AddRoleForUser(userId, roleId);
        }

        public async Task RemoveRole(int userId, int roleId)
        {
            await CheckExistingUserRole(userId, roleId);

            await _roleService.RemoveRoleFromUser(userId, roleId);
        }

        private async Task CheckExistingUserRole(int userId, int roleId)
        {
            var existingUser = await _userRepository.GetAsync(userId);

            var existingRole = await _roleRepository.GetAsync(roleId);

            if (existingUser == null || existingRole == null)
            {
                _logger.LogError($"User with Id: {userId} and role with Id: {roleId} don't exist!");
                throw new CustomUserFriendlyException($"Specified user or role don't exist!");
            }

            var existingUserRole = await _userRoleRepository
                .GetAll()
                .AnyAsync(x => x.UserId == userId && x.RoleId == roleId);

            if (existingUserRole)
            {
                _logger.LogError($"User with Id: {userId} and role with Id: {roleId} already set!");
                throw new CustomUserFriendlyException($"User already have this role!");
            }
        }

        public async Task ChangePassword(UserModel userModel)
        {
            var user = await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Email == userModel.Email);

            if (user == null)
            {
                _logger.LogError($"Can't find user with email: {userModel.Email}");
                throw new CustomUserFriendlyException("Unable to change password. Try again with another email!");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

            Update(_mapper.Map<UserModel>(user));
        }
    }
}
