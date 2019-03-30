using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class SearchViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "Tür")]
        public string Type { get; set; }
        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Güncelleme Tarihi")]
        public DateTime UpdatedAt { get; set; }
        [Display(Name = "Oluşturan Kişi")]
        public string CreatedBy { get; set; }
    }
}