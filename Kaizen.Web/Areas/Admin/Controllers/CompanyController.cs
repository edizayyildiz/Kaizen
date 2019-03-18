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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService, ApplicationUserManager userManager, IEmployeeService employeeService) : base(userManager, employeeService)
        {
            this.companyService = companyService;
        }
        // GET: Admin/Company
        public ActionResult Index()
        {
            var companies = companyService.GetAll();
            var companyViewModels = Mapper.Map<IEnumerable<CompanyViewModel>>(companies);
            return View(companyViewModels);
        }

        public ActionResult Create()
        {
            var companyViewModel = new CompanyViewModel();
            return View(companyViewModel);
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel companyViewModel)
        {
            if (ModelState.IsValid)
            {
                var company = Mapper.Map<Company>(companyViewModel);
                companyService.Insert(company);
                return RedirectToAction("Index");
            }
            return View(companyViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var company = companyService.Find(id);
            var companyViewModel = Mapper.Map<CompanyViewModel>(company);
            if (companyViewModel == null)
            {
                return HttpNotFound();
            }
            return View(companyViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CompanyViewModel companyViewModel)
        {
            if (ModelState.IsValid)
            {
                var company = Mapper.Map<Company>(companyViewModel);
                companyService.Update(company);
                return RedirectToAction("Index");
            }
            return View(companyViewModel);
        }

        public ActionResult Delete(Guid id)
        {
            companyService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var company = companyService.Find(id);
            var companyViewModel = Mapper.Map<CompanyViewModel>(company);
            return View(companyViewModel);
        }
    }
}