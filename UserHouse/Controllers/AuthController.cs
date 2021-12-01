using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserHouse.Application.Auth;
using UserHouse.Application.Dtos.Login;
using UserHouse.Application.Models;
using UserHouse.Application.Users;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Entities.Roles;
using UserHouse.Infrastructure.Entities.Users;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace UserHouse.Web.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userAppService;
        private readonly IOptions<AuthOptions> _options;

        public AuthController(
            IMapper mapper,
            IUserService userAppService,
            IOptions<AuthOptions> options)
        {
            _mapper = mapper;
            _userAppService = userAppService;
            _options = options;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = AuthenticateUser(loginDto.Email, loginDto.FirstName);

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

        private UserModel AuthenticateUser(string email, string firstName)
        {
            var user = _userAppService.GetAll()
                .Result
                .Where(x => x.Email == email && x.FirstName == firstName)
                .FirstOrDefault();

            return user;
        }

        private string GenerateJWTToken(UserModel userModel)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Birthdate, userModel.DateOfBirth.ToString())
            };

            //foreach (var role in user)
            //{

            //}

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
