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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;
        public CompanyController(ICompanyService companyService, ApplicationUserManager userManager):base(userManager)
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
    }
}