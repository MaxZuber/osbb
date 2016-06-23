using System;
using System.Runtime.InteropServices;
using XCL.Common.Result;
using XCL.Core.Services.Abstract;
using XCL.Models.DbModels;
using XCL.Repository.Repositories.Abstract;

namespace XCL.Core.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly ICryptService _cryptService;

        public UserService(IUserRepository userRepository, IEmailService emailService, ICryptService cryptService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _cryptService = cryptService;
        }

        public Account GetUserAccountById(int  id)
        {
            return _userRepository.GetUserById(id);
        }

        public bool IsUserExist(string email)
        {
            return _userRepository.GetUserByEmail(email) != null;
        }

        public RequestResult<Account> RegisterUser(Account account)
        {
            var result = new RequestResult<Account>();

            if (string.IsNullOrEmpty(account.Password) || string.IsNullOrEmpty(account.Email))
            {
                result.Status = RequestStatus.Error;
                result.Message = "Помилка при реєстрації";
            }
            else if (IsUserExist(account.Email))
            {
                result.Status = RequestStatus.Error;
                result.Message = "Користувач з таким емейлом вже існує";
            }
            else
            {
                try
                {
                    account.Password = _cryptService.EncryptUserPassword(account.Password);
                    result.Obj = _userRepository.SaveUser(account);
                    _emailService.SendRegistrationConfirmEmail(account, _cryptService.GenerateVerificationToken());
                    result.Status = RequestStatus.Success;
                }
                catch (Exception exception)
                {
                    result.Status = RequestStatus.Error;
                    result.Message = "Критична помилка";
                }
            }

            return result;
        }

        public RequestResult<Account> UpdateUserAccount(Account account)
        {
            var result = new RequestResult<Account>();

            if (string.IsNullOrEmpty(account.Email)
                || string.IsNullOrEmpty(account.FirstName)
                || string.IsNullOrEmpty(account.LastName)
                || account.FlatId <= 0)
            {
                result.Status = RequestStatus.Error;
                result.Message = "Відсутні обовязкові поля";
            }
            else
            {
                try
                {
                    result.Obj = _userRepository.UpdateAccount(account);
                    result.Status =  RequestStatus.Success;
                }
                catch (Exception ex)
                {
                    result.Status = RequestStatus.Error;
                    result.Message = "Помилка під час збереження";
                }
            }

            return result;
        }
    }
}
