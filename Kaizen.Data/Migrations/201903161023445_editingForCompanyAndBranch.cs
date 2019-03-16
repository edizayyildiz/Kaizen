namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editingForCompanyAndBranch : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Departments", new[] { "CompanyId" });
            AddColumn("dbo.Departments", "BranchId", c => c.Guid(nullable: false));
            AddColumn("dbo.Employees", "BranchId", c => c.Guid());
            CreateIndex("dbo.Employees", "BranchId");
            CreateIndex("dbo.Departments", "BranchId");
            AddForeignKey("dbo.Employees", "BranchId", "dbo.Branches", "Id");
            AddForeignKey("dbo.Departments", "BranchId", "dbo.Branches", "Id", cascadeDelete: true);
            DropColumn("dbo.Departments", "CompanyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "CompanyId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Departments", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Employees", "BranchId", "dbo.Branches");
            DropIndex("dbo.Departments", new[] { "BranchId" });
            DropIndex("dbo.Employees", new[] { "BranchId" });
            DropColumn("dbo.Employees", "BranchId");
            DropColumn("dbo.Departments", "BranchId");
            CreateIndex("dbo.Departments", "CompanyId");
            AddForeignKey("dbo.Departments", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
    }
}
