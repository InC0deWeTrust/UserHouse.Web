using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserHouse.Data.Entities;
using UserHouse.Infrastructure.Repositories.Generic;

namespace UserHouse.Application.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SecurityService> _logger;
        private readonly IRepository<User> _userRepository;

        private readonly string secretKeyPassword;
        public SecurityService(
            IConfiguration configuration,
            ILogger<SecurityService> logger,
            IRepository<User> userRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _userRepository = userRepository;

            secretKeyPassword = _configuration["SECRET_KEY_PASSWORD"];
        }

        public bool CheckPasswords()
        {
            throw new NotImplementedException();
        }

        public string DecryptPassword(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);

            var encodedResult = Encoding.UTF8.GetString(base64EncodedBytes);

            encodedResult = encodedResult.Substring(0, encodedResult.Length - secretKeyPassword.Length);

            return encodedResult;
        }

        public string EncryptPassword(string password)
        {
            password += secretKeyPassword;

            var passwordBytes = Encoding.UTF8.GetBytes(password);

            return Convert.ToBase64String(passwordBytes);
        }
    }
}
