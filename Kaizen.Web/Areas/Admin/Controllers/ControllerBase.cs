using Kaizen.Model;
using Kaizen.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kaizen.Web.Areas.Admin.Controllers
{
    public class ControllerBase : Controller
    {
        // GET: Admin/ControllerBase
        public ApplicationUserManager userManager;
        public ApplicationRoleManager roleManager;
        public IEmployeeService employeeService;
        public ControllerBase(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public ControllerBase(ApplicationUserManager userManager, IEmployeeService employeeService, ApplicationRoleManager roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
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
            var currentUser = userManager.FindByNameAsync(userName).Result; // Result async i senkrona çeviriyor
            var employee = employeeService.GetAll().FirstOrDefault(f => f.Email == currentUser.Email);
            var userRoles = Roles.GetRolesForUser(userName); // Kullanıcının rollerini string dizi olarak döndürür
            ViewBag.ProfilePhoto = employee.Photo;
            ViewBag.CurrentUser = currentUser.FullName;
            ViewBag.CurrentEmail = currentUser.Email;
            ViewBag.CurrentUserName = currentUser.UserName;
            ViewBag.UserRoles = userRoles;

            base.OnActionExecuted(filterContext);
        }
    }
}