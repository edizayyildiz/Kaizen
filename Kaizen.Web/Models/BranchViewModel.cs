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
        public CompanyViewModel Company { get; set; }
        public CountryViewModel Country { get; set; }
        public CityViewModel City { get; set; }
        public CountyViewModel County { get; set; }
        [Display(Name = "Şube Adresi")]
        public string Address { get; set; }
    }
}