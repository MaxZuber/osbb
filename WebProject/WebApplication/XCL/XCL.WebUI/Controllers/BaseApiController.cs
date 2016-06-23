using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using XCL.Common.Models;

namespace XCL.WebUI.Controllers
{
    public class BaseApiController : ApiController
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