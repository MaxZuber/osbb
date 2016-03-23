using XCL.Models.DbModels;

namespace XCL.Core.Services.Abstract
{
    public interface IEmailService
    {
        bool SendRegistrationConfirmEmail(Account account, string verificationToken);
    }
}
