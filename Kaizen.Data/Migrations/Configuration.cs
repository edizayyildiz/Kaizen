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

            new ApplicationDbContextSeed().Seed(context);
        }
    }
}
