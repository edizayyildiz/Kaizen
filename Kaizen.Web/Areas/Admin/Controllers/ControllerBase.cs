using Kaizen.Model;
using Kaizen.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kaizen.Web.Areas.Admin.Controllers
{
    public class ControllerBase : Controller
    {
        // GET: Admin/ControllerBase
        public ApplicationUserManager userManager;
        public IEmployeeService employeeService;
        public ControllerBase(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public ControllerBase(ApplicationUserManager userManager, IEmployeeService employeeService)
        {
            this.userManager = userManager;
            this.employeeService = employeeService;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        // bu metot tüm actionlardan sonra çalışır
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var userName = User.Identity.Name;
            var currentUser = userManager.FindByNameAsync(userName).Result; // result async i senkrona çeviriyor
            var employee = employeeService.GetAll().FirstOrDefault(f => f.Email == currentUser.Email);
            ViewBag.ProfilePhoto = employee.Photo;
            ViewBag.CurrentUser = currentUser.FullName;
            ViewBag.CurrentEmail = currentUser.Email;
            ViewBag.CurrentUserName = currentUser.UserName;

            base.OnActionExecuted(filterContext);
        }
    }
}