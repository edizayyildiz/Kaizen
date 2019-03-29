using AutoMapper;
using Kaizen.Model;
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
        private readonly ICompanyService companyService;
        private readonly IBranchService branchService;
        public DepartmentController(IDepartmentService departmentService, ICompanyService companyService, IBranchService branchService, ApplicationUserManager userManager, IEmployeeService employeeService, ApplicationRoleManager roleManager) : base(userManager, employeeService, roleManager)
        {
            this.departmentService = departmentService;
            this.companyService = companyService;
            this.branchService = branchService;
        }
        // GET: Admin/Department
        public ActionResult Index()
        {
            var departments = departmentService.GetAll();
            var departmentViewModels = Mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(departmentViewModels);
        }

        public ActionResult Create()
        {
            var userMail = User.Identity.Name;
            var currentEmployee = employeeService.GetAll(e => e.Email == userMail).FirstOrDefault();
            var currentCompanyId = companyService.GetAll(c => c.Id == currentEmployee.CompanyId).FirstOrDefault().Id;
            var branches = branchService.GetAll(b => b.CompanyId == currentCompanyId);
            ViewBag.BranchId = new SelectList(branches, "Id", "Name");
            var departmentViewModel = new DepartmentViewModel();
            return View(departmentViewModel);
        }

        [HttpPost]
        public ActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var departmant = Mapper.Map<Department>(departmentViewModel);
                departmentService.Insert(departmant);
                return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var userMail = User.Identity.Name;
            var currentEmployee = employeeService.GetAll(e => e.Email == userMail).FirstOrDefault();
            var currentCompanyId = companyService.GetAll(c => c.Id == currentEmployee.CompanyId).FirstOrDefault().Id;
            var branches = branchService.GetAll(b => b.CompanyId == currentCompanyId);
            var department = departmentService.Find(id);
            var departmentViewModel = Mapper.Map<DepartmentViewModel>(department);
            if (departmentViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = new SelectList(branches, "Id", "Name", department.BranchId );
            return View(departmentViewModel);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var department = Mapper.Map<Department>(departmentViewModel);
                departmentService.Update(department);
                return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }

        public ActionResult Delete(Guid id)
        {
            departmentService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var department = departmentService.Find(id);
            var departmentViewModel = Mapper.Map<DepartmentViewModel>(department);
            return View(departmentViewModel);
        }
    }
}