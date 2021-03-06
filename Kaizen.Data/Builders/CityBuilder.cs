﻿using Kaizen.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Data.Builders
{
    public class CityBuilder
    {
        public CityBuilder(EntityTypeConfiguration<City> entity)
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);

            entity.HasRequired(p => p.Country).WithMany(w => w.Cities).HasForeignKey(p => p.CountryId);

        }

    }
}
