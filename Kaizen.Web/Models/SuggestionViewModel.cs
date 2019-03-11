using Kaizen.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class SuggestionViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Mevcut Durum")]
        public string CurrentStatus { get; set; }
        [Required]
        [Display(Name = "Önerilen Durum")]
        public string SuggestedStatus { get; set; }
        [Display(Name = "Değerlendirme")]
        public Assessment Assessment { get; set; }
        [Display(Name = "Çalışan")]
        public Guid? EmployeeId { get; set; }
        [Display(Name = "Çalışan")]
        public EmployeeViewModel Employee { get; set; }
    }
}