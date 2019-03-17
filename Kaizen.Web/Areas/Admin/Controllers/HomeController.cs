﻿using Kaizen.Service;
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
    public class HomeController : ControllerBase
    {
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IEmployeeService employeeService) : base(userManager, employeeService)
        {
            this.userManager = userManager;
            this.employeeService = employeeService;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}