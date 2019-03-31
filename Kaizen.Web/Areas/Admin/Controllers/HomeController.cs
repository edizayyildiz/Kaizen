using Kaizen.Service;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kaizen.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IEmployeeService employeeService, ApplicationRoleManager roleManager) : base(userManager, employeeService, roleManager)
        {

        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}