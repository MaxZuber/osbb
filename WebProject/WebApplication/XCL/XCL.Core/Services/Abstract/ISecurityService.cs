using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using XCL.Models.DbModels;

namespace XCL.Core.Services.Abstract
{
    public interface ISecurityService
    {
        bool Login(string username, string password);
        void AuthenticateRequest();
    }
}
