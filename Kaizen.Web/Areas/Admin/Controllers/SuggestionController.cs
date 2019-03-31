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
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionService suggestionService;
        public SuggestionController(ISuggestionService suggestionService, IEmployeeService employeeService, ApplicationUserManager userManager, ApplicationRoleManager roleManager) : base(userManager, employeeService, roleManager)
        {
            this.suggestionService = suggestionService;
        }
        // GET: Admin/Suggestion
        public ActionResult Index()
        {
            var suggestions = suggestionService.GetAll();
            var suggestionViewModels = Mapper.Map<IEnumerable<SuggestionViewModel>>(suggestions);
            return View(suggestionViewModels);
        }

        public ActionResult Create()
        {
            var suggestionViewModel = new SuggestionViewModel();
            return View(suggestionViewModel);
        }

        [HttpPost]
        public ActionResult Create(SuggestionViewModel suggestionViewModel)
        {
            if (ModelState.IsValid)
            {
                var suggestion = Mapper.Map<Suggestion>(suggestionViewModel);
                var userMail = User.Identity.Name;
                var employee = employeeService.GetAll(e => e.Email == userMail).FirstOrDefault().Id;
                suggestion.EmployeeId = employee;
                suggestionService.Insert(suggestion);
                return RedirectToAction("Index");
            }
            return View(suggestionViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var userMail = User.Identity.Name;
            ViewBag.CurrentEmployeeId = employeeService.GetAll(e => e.Email == userMail).FirstOrDefault().Id;
            ViewBag.AuthorEmployeeId = suggestionService.GetAll(f => f.Id == id).FirstOrDefault().EmployeeId;
            var suggestion = suggestionService.Find(id);
            
            var suggestionViewModel = Mapper.Map<SuggestionViewModel>(suggestion);
            if (suggestionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(suggestionViewModel);
        }

        [HttpPost]
        public ActionResult Edit(SuggestionViewModel suggestionViewModel)
        {
            if (ModelState.IsValid)
            {
                // Suggestion ı edit yaptıktan sonra FirstName ve LastName alanı boş geliyordu(employee null olarak update ediliyordu), onun için suggestion ı veren employee bulundu ve suggestionViewModel e update öncesi eklendi
                //suggestionViewModel'e EmployeeId eklenmezse FirstName LastName bulamıyor.
                var employeeId = suggestionService.GetAll(f => f.Id == suggestionViewModel.Id).FirstOrDefault().EmployeeId;
                //var employee = employeeService.GetAll(f => f.Id == employeeId).FirstOrDefault();
                //var employee = employeeService.GetAll().FirstOrDefault(f => f.Suggestions.FirstOrDefault().Id == suggestionViewModel.Id);
                //var employeeViewModel = Mapper.Map<EmployeeViewModel>(employee);
                suggestionViewModel.EmployeeId = employeeId;
                //suggestionViewModel.Employee = employeeViewModel;
                var suggestion = Mapper.Map<Suggestion>(suggestionViewModel);
                suggestionService.Update(suggestion);
                return RedirectToAction("Index");
            }
            return View(suggestionViewModel);
        }

        public ActionResult Delete(Guid id)
        {
            suggestionService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            var suggestion = suggestionService.Find(id);
            var suggestionViewModel = Mapper.Map<SuggestionViewModel>(suggestion);
            return View(suggestionViewModel);
        }
    }
}