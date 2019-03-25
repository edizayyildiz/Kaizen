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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;
        public CountryController(ICountryService countryService, ApplicationUserManager userManager, IEmployeeService employeeService, ApplicationRoleManager roleManager) : base(userManager, employeeService, roleManager)
        {
            this.countryService = countryService;
        }
        // GET: Admin/Country
        public ActionResult Index()
        {
            var countries = countryService.GetAll();
            var countryViewModels = Mapper.Map<IEnumerable<CountryViewModel>>(countries);
            return View(countryViewModels);
        }
        
        public ActionResult Create()
        {
            var countryViewModel = new CountryViewModel();
            return View(countryViewModel);
        }

        [HttpPost]
        public ActionResult Create(CountryViewModel countryViewModel)
        {
            if (ModelState.IsValid)
            {
                var country = Mapper.Map<Country>(countryViewModel);
                countryService.Insert(country);
                return RedirectToAction("Index");
            }
            return View(countryViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var country = countryService.Find(id);
            var countryViewModel = Mapper.Map<CountryViewModel>(country);
            if (countryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(countryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CountryViewModel countryViewModel)
        {
            if (ModelState.IsValid)
            {
                var country = Mapper.Map<Country>(countryViewModel);
                countryService.Update(country);
                return RedirectToAction("Index");
            }
            return View(countryViewModel);
        }

        public ActionResult Delete(Guid id)
        {
            countryService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var country = countryService.Find(id);
            var countryViewModel = Mapper.Map<CountryViewModel>(country);
            return View(countryViewModel);
        }
    }
}