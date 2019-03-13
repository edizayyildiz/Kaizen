using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            Departments = new HashSet<Department>();
            Suggestions = new HashSet<Suggestion>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid? CompanyId { get; set; }
        public string Position { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }
}
