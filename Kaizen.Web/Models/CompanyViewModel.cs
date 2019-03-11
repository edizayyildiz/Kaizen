using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class CompanyViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Şirket Adı")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Sektör")]
        public string Sector { get; set; }
        [Required]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Çalışsan Sayısı")]
        public int HeadCount { get; set; }
    }
}