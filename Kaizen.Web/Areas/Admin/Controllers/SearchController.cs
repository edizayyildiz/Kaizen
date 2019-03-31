using Kaizen.Service;
using Kaizen.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaizen.Web.Areas.Admin.Controllers
{
    public class SearchController : ControllerBase
    {
        private readonly IBranchService branchService;
        private readonly IDepartmentService departmentService;
        private readonly ISuggestionService suggestionService;
        private readonly ICompanyService companyService;
        public SearchController(ApplicationUserManager userManager, IEmployeeService employeeService, ApplicationRoleManager roleManager, IBranchService branchService, IDepartmentService departmentService, ISuggestionService suggestionService, ICompanyService companyService) : base(userManager, employeeService, roleManager)
        {
            this.branchService = branchService;
            this.departmentService = departmentService;
            this.suggestionService = suggestionService;
            this.companyService = companyService;
            this.companyService = companyService;
        }
        // GET: Admin/Search
        public ActionResult Index(string searchContent)
        {
            var searchResults = new List<SearchViewModel>();

            var branches = this.branchService.Search(searchContent).Select(f => new SearchViewModel { Id = f.Id, Title = f.Name, Type = "Şube", CreatedAt = f.CreatedAt, UpdatedAt = f.UpdatedAt, CreatedBy = f.CreatedBy }).ToList();
            var departments = this.departmentService.Search(searchContent).Select(f => new SearchViewModel { Id = f.Id, Title = f.Name, Type = "Departman", CreatedAt = f.CreatedAt, UpdatedAt = f.UpdatedAt, CreatedBy = f.CreatedBy });
            var suggestions = this.suggestionService.Search(searchContent).Select(f => new SearchViewModel { Id = f.Id, Title = f.Subject, Type = "Öneri", CreatedAt = f.CreatedAt, UpdatedAt = f.UpdatedAt, CreatedBy = f.CreatedBy });
            var companies = this.companyService.Search(searchContent).Select(f => new SearchViewModel { Id = f.Id, Title = f.Name, Type = "Şirket", CreatedAt = f.CreatedAt, UpdatedAt = f.UpdatedAt, CreatedBy = f.CreatedBy });

            searchResults.AddRange(branches);
            searchResults.AddRange(departments);
            searchResults.AddRange(suggestions);
            searchResults.AddRange(companies);

            return View(searchResults);
        }
    }
}