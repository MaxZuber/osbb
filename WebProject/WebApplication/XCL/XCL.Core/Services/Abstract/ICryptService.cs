namespace XCL.Core.Services.Abstract
{
    public interface ICryptService
    {
        string EncryptUserPassword(string password);
        string GenerateVerificationToken();
    }
}
