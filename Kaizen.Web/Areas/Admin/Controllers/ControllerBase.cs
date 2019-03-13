using Kaizen.Model;
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
        public ControllerBase(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
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
            ViewBag.CurrentUser = currentUser.FullName;
            ViewBag.CurrentEmail = currentUser.Email;
            ViewBag.CurrentUserName = currentUser.UserName;

            base.OnActionExecuted(filterContext);
        }
    }
}