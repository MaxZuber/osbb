using XCL.Common.Result;
using XCL.Models.DbModels;

namespace XCL.Core.Services.Abstract
{
    public interface IUserService
    {
        bool IsUserExist(string email);
        RequestResult<Account> RegisterUser(Account account);
    }
}
