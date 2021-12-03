using System;
using System.Collections.Generic;
using System.Text;

namespace UserHouse.Application.Security
{
    public interface ISecurityService
    {
        string EncryptPassword(string password);

        string DecryptPassword(string base64EncodedData);

        bool CheckPasswords();
    }
}
