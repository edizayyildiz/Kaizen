namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyIdToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "CompanyId", c => c.Guid());
            CreateIndex("dbo.Employees", "CompanyId");
            AddForeignKey("dbo.Employees", "CompanyId", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropColumn("dbo.Employees", "CompanyId");
        }
    }
}
