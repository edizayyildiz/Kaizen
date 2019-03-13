using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class Company : BaseEntity
    {
        public Company()
        {
            Departments = new HashSet<Department>();
            Branches = new HashSet<Branch>();
        }
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Description { get; set; }
        public int HeadCount { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
