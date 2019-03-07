using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class BaseEntity
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
    }
}
