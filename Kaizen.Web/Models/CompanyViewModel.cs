using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaizen.Web.Models
{
    public class CompanyViewModel
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
        [Display(Name = "Şirket Adı")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Sektör")]
        public string Sector { get; set; }
        [Required]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Çalışan Sayısı")]
        public int HeadCount { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
    }
}