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
        public CompanyController(ICompanyService companyService, ApplicationUserManager userManager):base(userManager)
        {
            this.companyService = companyService;
            //this.userManager = userManager;

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
    }
}