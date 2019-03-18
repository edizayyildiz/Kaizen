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
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;
        public CityController(ICityService cityService, ApplicationUserManager userManager, IEmployeeService employeeService) : base(userManager, employeeService)
        {
            this.cityService = cityService;
        }
        // GET: Admin/City
        public ActionResult Index()
        {
            var cities = cityService.GetAll();
            var cityViewModels = Mapper.Map<IEnumerable<CityViewModel>>(cities);
            return View(cityViewModels);
        }

        public ActionResult Create()
        {
            var cityViewModel = new CityViewModel();
            return View(cityViewModel);
        }

        [HttpPost]
        public ActionResult Create(CityViewModel cityViewModel)
        {
            if (ModelState.IsValid)
            {
                var city = Mapper.Map<City>(cityViewModel);
                cityService.Insert(city);
                RedirectToAction("Index");
            }
            return View(cityViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var city = cityService.Find(id);
            var cityViewModel = Mapper.Map<CityViewModel>(city);
            if (cityViewModel == null)
            {
                return HttpNotFound();
            }
            return View(cityViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CityViewModel cityViewModel)
        {
            if (ModelState.IsValid)
            {
                var city = Mapper.Map<City>(cityViewModel);
                cityService.Update(city);
                return RedirectToAction("Index");
            }
            return View(cityViewModel);
        }

        public ActionResult Delete(Guid id)
        {
            cityService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var city = cityService.Find(id);
            var cityViewModel = Mapper.Map<CityViewModel>(city);
            return View(cityViewModel);
        }
    }
}