using Kaizen.Data.Builders;
using Kaizen.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Suggestion> Suggestions { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CityBuilder(modelBuilder.Entity<City>());
            new CompanyBuilder(modelBuilder.Entity<Company>());
            new CountryBuilder(modelBuilder.Entity<Country>());
            new CountyBuilder(modelBuilder.Entity<County>());
            new DepartmentBuilder(modelBuilder.Entity<Department>());
            new EmployeeBuilder(modelBuilder.Entity<Employee>());
            new SuggestionBuilder(modelBuilder.Entity<Suggestion>());
            new BranchBuilder(modelBuilder.Entity<Branch>());

        }
    }
}
