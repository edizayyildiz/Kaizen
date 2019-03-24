using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class CityViewModel
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
        [Display(Name = "İl Adı")]
        public string Name { get; set; }
        [Display(Name = "Ülke")]
        public Guid CountryId { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<CountyViewModel> Counties { get; set; }
    }
}