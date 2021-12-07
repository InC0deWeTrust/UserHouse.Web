using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserHouse.Application.Auth;
using UserHouse.Application.Dtos.Login;
using UserHouse.Application.Helpers;
using UserHouse.Application.Models;
using UserHouse.Application.Roles;
using UserHouse.Application.Users;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace UserHouse.Web.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userAppService;
        private readonly IOptions<AuthToken> _options;
        private readonly IRoleService _roleService;

        public AuthController(
            ILogger<AuthController> logger,
            IUserService userAppService,
            IOptions<AuthToken> options,
            IRoleService roleService)
        {
            _logger = logger;
            _userAppService = userAppService;
            _options = options;
            _roleService = roleService;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = AuthenticateUser(loginDto.Email, loginDto.Password);

            if (user != null)
            {
                var token = GenerateJWTToken(user);

                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        private UserModel AuthenticateUser(string email, string password)
        {
            //I believe that it's more correctly to address my services
            //Rather than my repositories
            //Because Host should be working only with Business layer
            var user = _userAppService
                .GetAll().Result
                .FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                _logger.LogError($"Can't find user with email: {email}");
                throw new CustomUserFriendlyException("Password or Email is incorrect! Try again with a different ones.");
            }

            var verified = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!verified)
            {
                _logger.LogError("Passwords are not the same");
                throw new CustomUserFriendlyException("Password or Email is incorrect! Try again with a different ones.");
            }

            return user;
        }

        private async Task<string> GenerateJWTToken(UserModel userModel)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Birthdate, userModel.DateOfBirth.ToString()),
                //I'm not sure if I need to add hashed password to Claims
                new Claim(JwtRegisteredClaimNames.AtHash, userModel.Password),
            };

            var userRoles = await _roleService.GetRolesOfUser(userModel.Id);

            //Include every role of selected user to Claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role.RoleName));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
