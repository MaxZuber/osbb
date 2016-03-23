using XCL.Core.Services.Abstract;
using XCL.Models.DbModels;

namespace XCL.Core.Services.Impl
{
    public class EmailService : IEmailService
    {
        public bool SendRegistrationConfirmEmail(Account account, string verificationToken)
        {
            return true;
        }
    }
}
