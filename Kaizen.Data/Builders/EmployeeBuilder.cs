using Kaizen.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Data.Builders
{
    public class EmployeeBuilder
    {
        public EmployeeBuilder(EntityTypeConfiguration<Employee> entity)
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.UserName).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Posation).IsRequired().HasMaxLength(200);

            entity.HasMany(p => p.Departments).WithMany(w => w.Employees);
        }
    }
}
