using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using XCL.Common.InversionofControl;
using XCL.Common.Models;
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

        public bool AuthenticateUser(string userEmail)
        {
            var authTicket = CreateAuthTicket(userEmail);

            if (authTicket != null)
            {
                var cookie = CreateAuthCookie(authTicket);

                HttpContext.Current.Response.Cookies.Add(cookie);


                HttpContext.Current.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);    //remove oldCookie
                HttpContext.Current.Request.Cookies.Add(cookie);                                    //and set new one

                //to have authenticated user context we imidiately set it
                PostAuthRequest();

                return true;
            }
            return false;
        }

        public void SignOutUser()
        {
            throw new NotImplementedException();
        }

        private FormsAuthenticationTicket CreateAuthTicket(string userEmail)
        {
            var service = Ioc.Get<IUserRepository>();
            var user = service.GetUserByEmail(userEmail);
            if (user != null)
            {
                var serializeModel = new OsbbSerializeModel();
                serializeModel.Id = user.Id;
                serializeModel.Username = user.Email;
                serializeModel.Roles = new string[] {};



                string userData = JsonConvert.SerializeObject(serializeModel);
                return new FormsAuthenticationTicket(
                    1,
                    user.Id.ToString(),
                    DateTime.Now,
                    DateTime.Now.AddDays(30),
                    false,
                    userData);
            }

            return null;
        }

        private HttpCookie CreateAuthCookie(FormsAuthenticationTicket authenticationTicket)
        {
            string encTicket = FormsAuthentication.Encrypt(authenticationTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            return faCookie;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.Login(username, _cryptService.EncryptUserPassword(password));

            if (user != null)
            {
                //HttpContext.Current.User = new OsbbPrincipal(new GenericIdentity(user.Email))
                //{
                //    Roles = new string[] {},
                //    Username = user.Email,
                //    Id = user.Id
                //};
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

        public void PostAuthRequest()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializeModel = JsonConvert.DeserializeObject<OsbbSerializeModel>(authTicket.UserData);
                UpdateUser(serializeModel);
            }
            else
            {
                HttpContext.Current.User = new OsbbPrincipal(new GenericIdentity("mc"));
            }
        }

        public void UpdateUser(OsbbSerializeModel serializeModel)
        {
            var newUser = new OsbbPrincipal(serializeModel);
            newUser.Roles = serializeModel.Roles;
            HttpContext.Current.User = newUser;
        }
    }
}
