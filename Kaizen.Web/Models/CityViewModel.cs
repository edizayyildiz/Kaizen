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
        [Required]
        [Display(Name = "İl Adı")]
        public string Name { get; set; }
        public CountryViewModel Country { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<CountyViewModel> Counties { get; set; }
    }
}