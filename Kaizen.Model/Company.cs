﻿using System;
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
            Branches = new HashSet<Branch>();
            Employees = new HashSet<Employee>();
        }
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Description { get; set; }
        public int HeadCount { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
