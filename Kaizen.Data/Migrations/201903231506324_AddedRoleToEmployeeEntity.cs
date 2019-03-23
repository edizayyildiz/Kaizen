namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRoleToEmployeeEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Role", c => c.String());
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Role");
            DropColumn("dbo.Employees", "Role");
        }
    }
}
