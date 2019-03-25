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
    public class CountyController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ICountyService countyService;
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        public CountyController(ICompanyService companyService, ICountyService countyService, ICityService cityService, ICountryService countryService, ApplicationUserManager userManager, IEmployeeService employeeService) : base(userManager, employeeService)
        {
            this.companyService = companyService;
            this.countyService = countyService;
            this.cityService = cityService;
            this.countryService = countryService;
        }
        // GET: Admin/County
        public ActionResult Index()
        {
            var counties = countyService.GetAll();
            var countyViewModels = Mapper.Map<IEnumerable<CountyViewModel>>(counties);
            return View(countyViewModels);
        }

        public ActionResult Create()
        {
            var userMail = User.Identity.Name;
            var employee = employeeService.GetAll(f => f.Email == userMail).FirstOrDefault();
            var companyEmployeeEmails = (companyService.GetAll(f => f.Id == employee.CompanyId).FirstOrDefault()).Employees.Select(s => s.Email).ToList(); // birden fazla yönetici ülke ekleyince gösteriyor mu ?
            var cities = new List<City>();
            foreach (var emailItem in companyEmployeeEmails)
            {
                var countries = countryService.GetAll(c => c.CreatedBy == emailItem).Select(s => s.Id);// Bu kısma online kullanıcının şirketindeki yöneticilerin ve kendisinin eklediği ülkelerin Id si.
                foreach (var countriesItem in countries)
                {
                    var citiesItem = cityService.GetAll(c => c.CountryId == countriesItem);
                    cities.AddRange(citiesItem);
                }
            }
            ViewBag.CityId = new SelectList(cities, "Id", "Name");
            var countyViewModel = new CountyViewModel();
            return View(countyViewModel);
        }

        [HttpPost]
        public ActionResult Create(CountyViewModel countyViewModel)
        {
            if (ModelState.IsValid)
            {
                var county = Mapper.Map<County>(countyViewModel);
                countyService.Insert(county);
                return RedirectToAction("Index");
            }
            return View(countyViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var userMail = User.Identity.Name;
            var employee = employeeService.GetAll(f => f.Email == userMail).FirstOrDefault();
            var companyEmployeeEmails = (companyService.GetAll(f => f.Id == employee.CompanyId).FirstOrDefault()).Employees.Select(s => s.Email).ToList(); // birden fazla yönetici ülke ekleyince gösteriyor mu ?
            var cities = new List<City>();
            foreach (var emailItem in companyEmployeeEmails)
            {
                var countries = countryService.GetAll(c => c.CreatedBy == emailItem).Select(s => s.Id);// Bu kısma online kullanıcının şirketindeki yöneticilerin ve kendisinin eklediği ülkelerin Id si.
                foreach (var countriesItem in countries)
                {
                    var citiesItem = cityService.GetAll(c => c.CountryId == countriesItem);
                    cities.AddRange(citiesItem);
                }
            }
            ViewBag.CityId = new SelectList(cities, "Id", "Name");
            var county = countyService.Find(id);
            var countyViewModel = Mapper.Map<CountyViewModel>(county);
            if (countyViewModel == null)
            {
                return HttpNotFound();
            }
            return View(countyViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CountyViewModel countyViewModel)
        {
            var userMail = User.Identity.Name;
            var employee = employeeService.GetAll(f => f.Email == userMail).FirstOrDefault();
            var companyEmployeeEmails = (companyService.GetAll(f => f.Id == employee.CompanyId).FirstOrDefault()).Employees.Select(s => s.Email).ToList(); // birden fazla yönetici ülke ekleyince gösteriyor mu ?
            var cities = new List<City>();
            foreach (var emailItem in companyEmployeeEmails)
            {
                var countries = countryService.GetAll(c => c.CreatedBy == emailItem).Select(s => s.Id);// Bu kısma online kullanıcının şirketindeki yöneticilerin ve kendisinin eklediği ülkelerin Id si.
                foreach (var countriesItem in countries)
                {
                    var citiesItem = cityService.GetAll(c => c.CountryId == countriesItem);
                    cities.AddRange(citiesItem);
                }
            }
            ViewBag.CityId = new SelectList(cities, "Id", "Name");
            if (ModelState.IsValid)
            {
                var county = Mapper.Map<County>(countyViewModel);
                countyService.Update(county);
                return RedirectToAction("Index");
            }
            return View(countyViewModel);
        }

        public ActionResult Delete(Guid id)
        {
            countyService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var county = countyService.Find(id);
            var countyViewModel = Mapper.Map<CountyViewModel>(county);
            return View(countyViewModel);
        }
    }
}