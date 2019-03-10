using Kaizen.Model;
using Kaizen.Service;
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
            var suggestion = suggestionService.GetAll();
            return View(suggestion);
        }

        public ActionResult Create()
        {
            var suggestion = new Suggestion();
            suggestion.Assessment = Assessment.NoAssessment;
            suggestion.Employee.FirstName = "Name";
            return View(suggestion);
        }

        [HttpPost]
        public ActionResult Create(Suggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                suggestionService.Insert(suggestion);
                return RedirectToAction("index");
            }
            return View(suggestion);
        }
    }
}