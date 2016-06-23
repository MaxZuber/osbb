using XCL.Common.Result;
using XCL.Models.DbModels;

namespace XCL.Core.Services.Abstract
{
    public interface IUserService
    {
        Account GetUserAccountById(int id);
        bool IsUserExist(string email);
        RequestResult<Account> RegisterUser(Account account);
        RequestResult<Account> UpdateUserAccount(Account account);
    }
}
