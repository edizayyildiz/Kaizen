using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class CountryViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Ülke Adı")]
        public string Name { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}