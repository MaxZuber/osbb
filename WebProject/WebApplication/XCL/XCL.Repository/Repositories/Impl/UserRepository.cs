using System;
using System.Linq;
using XCL.Models.DbModels;
using XCL.Repository.Repositories.Abstract;

namespace XCL.Repository.Repositories.Impl
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public Account GetUserByEmail(string email)
        {
            return _db.Accounts.SingleOrDefault(x => x.Email == email);
        }

        public Account SaveUser(Account account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
            return account;
        }

        public Account Login(string username, string password)
        {
            return _db.Accounts.SingleOrDefault(x => x.Email == username && x.Password == password);
        }
    }
}
