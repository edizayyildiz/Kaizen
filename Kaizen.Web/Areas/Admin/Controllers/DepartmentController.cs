using AutoMapper;
using Kaizen.Service;
using Kaizen.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaizen.Web.Areas.Admin.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        public DepartmentController(IDepartmentService departmentService, ApplicationUserManager userManager, IEmployeeService employeeService, ApplicationRoleManager roleManager) : base(userManager, employeeService, roleManager)
        {
            this.departmentService = departmentService;
        }
        // GET: Admin/Department
        public ActionResult Index()
        {
            var departments = departmentService.GetAll();
            var departmentViewModels = Mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(departmentViewModels);
        }

    }
}