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
    public class SuggestionController : Controller
    {
        private readonly ISuggestionService suggestionService;
        public SuggestionController(ISuggestionService suggestionService)
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
            //suggestionViewModel.Assessment = Enum.GetValues(typeof(Assessment));
            return View(suggestionViewModel);
        }

        [HttpPost]
        public ActionResult Create(SuggestionViewModel suggestionViewModel)
        {
            if (ModelState.IsValid)
            {
                var suggestion = Mapper.Map<Suggestion>(suggestionViewModel);
                suggestionService.Insert(suggestion);
                return RedirectToAction("index");
            }
            return View(suggestionViewModel);
        }

        public ActionResult Edit(Guid id)
        {
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
                var suggestion = Mapper.Map<Suggestion>(suggestionViewModel);
                suggestionService.Update(suggestion);
                return RedirectToAction("index");
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