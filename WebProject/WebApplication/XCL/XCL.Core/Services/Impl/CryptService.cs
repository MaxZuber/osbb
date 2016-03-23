using System;
using XCL.Core.Services.Abstract;

namespace XCL.Core.Services.Impl
{
    public class CryptService : ICryptService
    {
        public string EncryptUserPassword(string password)
        {
            return password;;
        }

        public string GenerateVerificationToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
