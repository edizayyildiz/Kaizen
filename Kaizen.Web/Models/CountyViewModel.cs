using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class CountyViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "İlçe Adı")]
        public string Name { get; set; }
        public CityViewModel City { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
    }
}