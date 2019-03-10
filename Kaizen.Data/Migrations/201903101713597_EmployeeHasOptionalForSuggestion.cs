namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeHasOptionalForSuggestion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Suggestions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Suggestions", new[] { "EmployeeId" });
            AlterColumn("dbo.Suggestions", "EmployeeId", c => c.Guid());
            CreateIndex("dbo.Suggestions", "EmployeeId");
            AddForeignKey("dbo.Suggestions", "EmployeeId", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Suggestions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Suggestions", new[] { "EmployeeId" });
            AlterColumn("dbo.Suggestions", "EmployeeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Suggestions", "EmployeeId");
            AddForeignKey("dbo.Suggestions", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
