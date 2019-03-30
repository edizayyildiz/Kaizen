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
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Güncelleme Tarihi")]
        public DateTime UpdatedAt { get; set; }
        [Display(Name = "Oluşturan")]
        public string CreatedBy { get; set; }
        [Display(Name = "Güncelleyen")]
        public string UpdatedBy { get; set; }
        [Required]
        [Display(Name = "Konu")]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Mevcut Durum")]
        public string CurrentStatus { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Önerilen Durum")]
        public string SuggestedStatus { get; set; }
        [Display(Name = "Değerlendirme")]
        public Assessment Assessment { get; set; }
        public Guid? EmployeeId { get; set; }
        public EmployeeViewModel Employee { get; set; }
    }
}