using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using XCL.Common.Result;
using XCL.ViewModels;
using XCL.Core.Services.Abstract;
using XCL.Models.DbModels;

namespace XCL.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ISecurityService _uSecurityService;

        public AccountController(IUserService userService, ISecurityService uSecurityService)
        {
            _userService = userService;
            _uSecurityService = uSecurityService;
        }

        public ActionResult Login()
        {
            var vm = new LoginViewModel()
            {
                InValidLogin = null
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (_uSecurityService.Login(loginViewModel.Email, loginViewModel.Password))
            {
                _uSecurityService.AuthenticateUser(loginViewModel.Email);

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                loginViewModel.InValidLogin = true;
            }


            return View(loginViewModel);
        }

        public ActionResult Register()
        {
            var vm = new RegisterViewModel();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.RegisterUser(registerViewModel.UpdateEntity(new Account()));

                if (result.Status == RequestStatus.Success)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    registerViewModel.Status = false;
                    ModelState.AddModelError("", result.Message);
                }
            }

            return View(registerViewModel);
        }
    }
}