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

        public BranchController(IBranchService branchService, ApplicationUserManager userManager, IEmployeeService employeeService) : base(userManager, employeeService)
        {
            this.branchService = branchService;
        }
        // GET: Admin/Branch
        public ActionResult Index()
        {
            var branches = branchService.GetAll();
            var branchViewModels = Mapper.Map<BranchViewModel>(branches);
            return View(branchViewModels);
        }

        public ActionResult Create()
        {
            var branchViewModel = new BranchViewModel();
            return View(branchViewModel);
        }

        [HttpPost]
        public ActionResult Create(BranchViewModel branchViewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = Mapper.Map<Branch>(branchViewModel);
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
                RedirectToAction("Index");
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
            var branchViewModel = Mapper.Map<CompanyViewModel>(branch);
            return View(branchViewModel);
        }
    }
}