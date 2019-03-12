using Kaizen.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Data.Builders
{
    public class BranchBuilder
    {
        public BranchBuilder(EntityTypeConfiguration<Branch> entity)
        {
            entity.HasKey(p => p.Id);

            entity.HasRequired(p=>p.Company).WithMany(w=>w.Branches).HasForeignKey(p=>p.CompanyId);
            entity.HasOptional(p=>p.Country).WithMany(w=>w.Branches).HasForeignKey(p=>p.CountryId);
            entity.HasOptional(p=>p.City).WithMany(w=>w.Branches).HasForeignKey(p=>p.CityId);
            entity.HasOptional(p=>p.County).WithMany(w=>w.Branches).HasForeignKey(p=>p.CountyId);
        }
    }
}
