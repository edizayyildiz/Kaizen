using Kaizen.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Data.Builders
{
    public class DepartmentBuilder
    {
        public DepartmentBuilder(EntityTypeConfiguration<Department> entity)
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);

            entity.HasRequired(p => p.Branch).WithMany(b => b.Departments).HasForeignKey(p=>p.BranchId);
    }
}
}
