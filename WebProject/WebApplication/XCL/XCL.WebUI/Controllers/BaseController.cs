using System.Web.Mvc;
using XCL.Common.Models;

namespace XCL.WebUI.Controllers
{
    public class BaseController: Controller
    {
        protected new IOsbbPrincipal User
        {
            get
            {
                return base.User as IOsbbPrincipal;
            }
        }
    }
}