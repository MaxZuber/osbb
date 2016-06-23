using System;
using System.Collections.Generic;
using System.Linq;
using XCL.Common.Models;

namespace XCL.Core.Services.Abstract
{
    public interface ISecurityService
    {
        bool Login(string username, string password);
        void AuthenticateRequest();

        bool AuthenticateUser(string userEmail);
        void SignOutUser();
        void UpdateUser(OsbbSerializeModel serializeModel);
        void PostAuthRequest();
    }
}
