using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class BranchViewModel
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
        [Display(Name = "Şube Adı")]
        public string Name { get; set; }
        [Display(Name = "Firma")]
        public CompanyViewModel Company { get; set; }
        [Display(Name = "Ülke")]
        public CountryViewModel Country { get; set; }
        [Display(Name = "İl")]
        public CityViewModel City { get; set; }
        [Display(Name = "İlçe")]
        public CountyViewModel County { get; set; }
        [Display(Name = "Şube Adresi")]
        public string Address { get; set; }

        [Display(Name = "Departmanlar")]
        public IEnumerable<DepartmentViewModel> Departments { get; set; }
        [Display(Name = "Çalışanlar")]
        public IEnumerable<EmployeeViewModel> Employees { get; set; } // bunu da kaldırabiliriz belki.
    }
}