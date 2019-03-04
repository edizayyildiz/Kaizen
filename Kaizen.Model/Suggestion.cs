using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class Suggestion : BaseEntity
    {
        [Display(Name = "Görevi")]
        public string Posation { get; set; }
        [Display(Name = "Departman")]
        public int DepartmentId { get; set; }
        [Display(Name = "Mevcut Durum")]
        public string CurrentStatus { get; set; }
        [Display(Name = "Önerilen Durum")]
        public string SuggestedStatus { get; set; }
        [Display(Name = "Değerlendirme")]
        public Assessment Assessment { get; set; }

        [Display(Name = "Departman")]
        public virtual Department Department { get; set; }
    }
}
