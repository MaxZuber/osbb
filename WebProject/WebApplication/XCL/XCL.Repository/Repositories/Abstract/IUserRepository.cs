using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCL.Models.DbModels;

namespace XCL.Repository.Repositories.Abstract
{
    public interface IUserRepository
    {
        Account GetUserByEmail(string email);
        Account SaveUser(Account account);
        Account Login(string username, string password);
    }
}
