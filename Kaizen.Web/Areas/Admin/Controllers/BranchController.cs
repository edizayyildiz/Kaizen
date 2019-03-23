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
    public class BranchController : ControllerBase
    {
        private readonly IBranchService branchService;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        private readonly ICountyService countyService;
        private readonly ICompanyService companyService;

        public BranchController(IBranchService branchService, ICountryService countryService, ICityService cityService, ICountyService countyService, ICompanyService companyService, ApplicationUserManager userManager, IEmployeeService employeeService) : base(userManager, employeeService)
        {
            this.branchService = branchService;
            this.countryService = countryService;
            this.cityService = cityService;
            this.countyService = countyService;
            this.companyService = companyService;
        }
        // GET: Admin/Branch
        public ActionResult Index()
        {
            var branches = branchService.GetAll();
            var branchViewModels = Mapper.Map<IEnumerable<BranchViewModel>>(branches);
            return View(branchViewModels);
        }

        public ActionResult Create()
        {
            var userMail = User.Identity.Name;
            var employee = employeeService.GetAll(f => f.Email == userMail).FirstOrDefault();
            var companyEmployeeEmails = (companyService.GetAll(f => f.Id == employee.CompanyId).FirstOrDefault()).Employees.Select(s => s.Email).ToList(); // birden fazla yönetici ülke ekleyince gösteriyor mu ?
            foreach (var item in companyEmployeeEmails)
            {
                var countries = countryService.GetAll(c => c.CreatedBy == item);   // Bu kısma hoca ile bir bakılmalı.
                ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            }
            ViewBag.CityId = new SelectList("", "Id", "Name");
            ViewBag.CountyId = new SelectList("", "Id", "Name");
            var branchViewModel = new BranchViewModel();
            return View(branchViewModel);
        }

        [HttpPost]
        public ActionResult Create(BranchViewModel branchViewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = Mapper.Map<Branch>(branchViewModel);
                var userMail = User.Identity.Name;
                var authorizedEmployee = employeeService.GetAll(w => w.Email == userMail).FirstOrDefault();
                branch.CompanyId = authorizedEmployee.CompanyId;
                branchService.Insert(branch);
                return RedirectToAction("Index");
            }
            return View(branchViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var branch = branchService.Find(id);
            var branchViewModel = Mapper.Map<BranchViewModel>(branch);
            if (branchViewModel == null)
            {
                return HttpNotFound();
            }
            return View(branchViewModel);
        }

        [HttpPost]
        public ActionResult Edit(BranchViewModel branchViewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = Mapper.Map<Branch>(branchViewModel);
                branchService.Update(branch);
                return RedirectToAction("Index");
            }
            return View(branchViewModel);
        }

        public ActionResult Delete(Guid id)
        {
            branchService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var branch = branchService.Find(id);
            var branchViewModel = Mapper.Map<BranchViewModel>(branch);
            return View(branchViewModel);
        }
    }
}