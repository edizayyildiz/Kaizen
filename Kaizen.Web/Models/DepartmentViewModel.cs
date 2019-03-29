using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class DepartmentViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Güncelleme Tarihi")]
        public DateTime UpdatedAt { get; set; }
        [Display(Name = "Oluşturan")]
        public string CreatedBy { get; set; }
        [Display(Name = "Güncelleyen")]
        public string UpdatedBy { get; set; }
        [Required]
        [Display(Name = "Departman Adı")]
        public string Name { get; set; }
        [Display(Name = "Şube")]
        public Guid BranchId { get; set; }
        public BranchViewModel Branch { get; set; }
        [Display(Name = "Çalışanlar")]
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
    }
}

// Department headcount alanı eklenebilir ???