using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class Department : BaseEntity
    {
        public Department()
        {
            this.Suggestions = new HashSet<Suggestion>();
        }
        [Display(Name = "Departman Adı")]
        public string Name { get; set; }
        public int CompanyId { get; set; }

        [Display(Name = "Öneriler")]
        public virtual ICollection<Suggestion> Suggestions { get; set; }
        public virtual Company Company { get; set; }
    }
}
