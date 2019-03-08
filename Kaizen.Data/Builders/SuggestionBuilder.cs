using Kaizen.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Data.Builders
{
    public class SuggestionBuilder
    {
        public SuggestionBuilder(EntityTypeConfiguration<Suggestion> entity)
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Posation).IsRequired().HasMaxLength(200);
            entity.Property(p => p.CurrentStatus).IsRequired();
            entity.Property(p => p.SuggestedStatus).IsRequired();
            entity.Property(p => p.Assessment).IsRequired();

            entity.HasRequired(p => p.Employee).WithMany(w => w.Suggestions).HasForeignKey(p => p.EmployeeId);
        }
    }
}
