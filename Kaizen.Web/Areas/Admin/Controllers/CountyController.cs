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
        private readonly ICountyService countyService;
        public CountyController(ICountyService countyService, ApplicationUserManager userManager, IEmployeeService employeeService) : base(userManager, employeeService)
        {
            this.countyService = countyService;
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
                RedirectToAction("Index");
            }
            return View(countyViewModel);
        }

        public ActionResult Edit(Guid id)
        {
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