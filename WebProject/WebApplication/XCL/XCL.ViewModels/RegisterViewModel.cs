using System.Security.Cryptography;
using System.Security.Principal;
using XCL.Models.DbModels;

namespace XCL.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }

        public Account UpdateEntity(Account account)
        {
            account.Password = Password;
            account.Email = Email;
            return account;
        }
    }
}
