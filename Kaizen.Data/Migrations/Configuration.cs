namespace Kaizen.Data.Migrations
{
    using Kaizen.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Kaizen.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Kaizen.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdateOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var reason = new Company()
            {
                Id = Guid.Parse("9854e4fd-6e1f-4c31-9f0d-65422dbd0bd9"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "Reason",
                Sector = "Yaz�l�m",
                Description = "Reason firmas� a��klamas�",
                HeadCount = 200
            };
            var microsoft = new Company()
            {
                Id = Guid.Parse("a53a3120-f00a-4f3f-9a6b-5ebf3a1ede32"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "Microsoft",
                Sector = "Yaz�l�m",
                Description = "Microsoft firmas� a��klamas�",
                HeadCount = 1000
            };


            var turkey = new Country()
            {
                Id = Guid.Parse("73756f28-9de2-4592-a7cc-4675d3bfedef"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "T�rkiye"
            };
            var usa = new Country()
            {
                Id = Guid.Parse("b780f09d-c2d1-4be0-8916-1ca1a6b5ecc6"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "USA"
            };


            var turkeyCity = new City()
            {
                Id = Guid.Parse("6dbe0db1-763d-4ed1-8709-e7d7ce8a77a0"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "�stanbul",
                CountryId = Guid.Parse("73756f28-9de2-4592-a7cc-4675d3bfedef")
            };
            var usaCity = new City()
            {
                Id = Guid.Parse("8540d2c6-b95f-4129-8fe0-869db732045e"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "New York",
                CountryId = Guid.Parse("b780f09d-c2d1-4be0-8916-1ca1a6b5ecc6")
            };


            var istCounty = new County()
            {
                Id = Guid.Parse("48800942-4888-409e-89a0-e51a60a2e7ba"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "Be�ikta�",
                CityId = Guid.Parse("6dbe0db1-763d-4ed1-8709-e7d7ce8a77a0")
            };
            var newYorkCounty = new County()
            {
                Id = Guid.Parse("6b04b845-0552-44ae-81b2-bbce7e3da273"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "Brooklyn",
                CityId = Guid.Parse("8540d2c6-b95f-4129-8fe0-869db732045e")
            };


            var reasonBranch = new Branch()
            {
                Id = Guid.Parse("b98c6a08-40ed-4a0f-a13a-9b7b772b2828"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "�stanbul �ubesi",
                CompanyId = Guid.Parse("9854e4fd-6e1f-4c31-9f0d-65422dbd0bd9"),
                CountryId = Guid.Parse("73756f28-9de2-4592-a7cc-4675d3bfedef"),
                CityId = Guid.Parse("6dbe0db1-763d-4ed1-8709-e7d7ce8a77a0"),
                CountyId = Guid.Parse("48800942-4888-409e-89a0-e51a60a2e7ba"),
                Address = "Reason Software Be�ikta�/�stanbul"
            };
            var reasonBranch2 = new Branch()
            {
                Id = Guid.Parse("9fcf4898-20c2-4e21-9948-62d954421955"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "New York �ubesi",
                CompanyId = Guid.Parse("9854e4fd-6e1f-4c31-9f0d-65422dbd0bd9"),
                CountryId = Guid.Parse("b780f09d-c2d1-4be0-8916-1ca1a6b5ecc6"),
                CityId = Guid.Parse("8540d2c6-b95f-4129-8fe0-869db732045e"),
                CountyId = Guid.Parse("6b04b845-0552-44ae-81b2-bbce7e3da273"),
                Address = "Reason Software Brooklyn/New York"
            };
            var microsoftBranch = new Branch()
            {
                Id = Guid.Parse("0e36930a-8756-4411-af32-5f087e0ecf27"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "Turkey Office",
                CompanyId = Guid.Parse("a53a3120-f00a-4f3f-9a6b-5ebf3a1ede32"),
                CountryId = Guid.Parse("73756f28-9de2-4592-a7cc-4675d3bfedef"),
                CityId = Guid.Parse("6dbe0db1-763d-4ed1-8709-e7d7ce8a77a0"),
                CountyId = Guid.Parse("48800942-4888-409e-89a0-e51a60a2e7ba"),
                Address = "Microsoft Be�ikta�/�stanbul",
            };


            var employee = new Employee()
            {
                Id = Guid.Parse("9f7ba10d-7e07-4f84-afb5-1071d05ba657"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "admin",
                Photo = null,
                Email = "admin@gmail.com",
                Position = "CEO",
                CompanyId = Guid.Parse("9854e4fd-6e1f-4c31-9f0d-65422dbd0bd9"),
                BranchId = Guid.Parse("b98c6a08-40ed-4a0f-a13a-9b7b772b2828"),
            };


            var reasonDepartment = new Department()
            {
                Id = Guid.Parse("623a567d-e087-4040-befe-b1533aaa9794"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "Software",
                BranchId = Guid.Parse("b98c6a08-40ed-4a0f-a13a-9b7b772b2828"),
            };
            var reasonDepartment2 = new Department()
            {
                Id = Guid.Parse("7f3026e7-1e18-43af-ade0-ae3c64086697"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "�nsan Kaynaklar�",
                BranchId = Guid.Parse("9fcf4898-20c2-4e21-9948-62d954421955"),
            };
            var microsoftDepartment = new Department()
            {
                Id = Guid.Parse("5f31a2ae-76c2-445a-859d-912f1b813ab6"),
                CreatedAt = DateTime.Now,
                CreatedBy = "username",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "username",
                Name = "Software",
                BranchId = Guid.Parse("0e36930a-8756-4411-af32-5f087e0ecf27"),
            };


            var user = new ApplicationUser()
            {
                Id = "0a5152bb-9563-4eec-bde0-501127bacfb9",
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "admin@gmail.com",
                EmailConfirmed = false,
                PasswordHash = "AILqh33d5oU3noI48vS/RMHp8DlNiYRTn78wTQyW0nmtVvtTxy0j36yqR8hDO5P4og==", // Parola: 123456Can!
                SecurityStamp = "69b2fb73-64be-4619-8125-a2b22dafff2a",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = null,
                AccessFailedCount = 0,
                UserName = "admin@gmail.com",
                Role = "SuperAdmin"
            };

            context.Companies.AddOrUpdate(reason);
            context.Companies.AddOrUpdate(microsoft);

            context.Countries.AddOrUpdate(turkey);
            context.Countries.AddOrUpdate(usa);

            context.Cities.AddOrUpdate(turkeyCity);
            context.Cities.AddOrUpdate(usaCity);

            context.Counties.AddOrUpdate(istCounty);
            context.Counties.AddOrUpdate(newYorkCounty);

            context.Branches.AddOrUpdate(reasonBranch);
            context.Branches.AddOrUpdate(reasonBranch2);
            context.Branches.AddOrUpdate(microsoftBranch);

            context.Employees.AddOrUpdate(employee);

            context.Departments.AddOrUpdate(reasonDepartment);
            context.Departments.AddOrUpdate(reasonDepartment2);
            context.Departments.AddOrUpdate(microsoftDepartment);

            context.Users.AddOrUpdate(user);

            base.Seed(context);
        }

    }
}
