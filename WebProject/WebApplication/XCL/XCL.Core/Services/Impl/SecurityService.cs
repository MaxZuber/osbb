using System.Security.Principal;
using System.Web;
using XCL.Core.Services.Abstract;
using XCL.Repository.Repositories.Abstract;

namespace XCL.Core.Services.Impl
{
    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptService _cryptService;

        public SecurityService(IUserRepository userRepository, ICryptService cryptService)
        {
            _userRepository = userRepository;
            _cryptService = cryptService;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.Login(username, _cryptService.EncryptUserPassword(password));

            if (user != null)
            {
                HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(username), new string[] { });
                return true;
            }

            return false;
        }

        public void AuthenticateRequest()
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated == true)
            {
                IIdentity identity = HttpContext.Current.User.Identity;

                HttpContext.Current.User =  new GenericPrincipal(identity, new string[] { });
            }
            else
            {
                HttpContext.Current.User =  null;
            }
        }
    }
}
