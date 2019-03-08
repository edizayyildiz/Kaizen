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
            Employees = new HashSet<Employee>();
        }
        [Display(Name = "Departman Adı")]
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [Display(Name = "Çalışanlar")]
        public virtual ICollection<Employee> Employees { get; set; }
        
    }
}
