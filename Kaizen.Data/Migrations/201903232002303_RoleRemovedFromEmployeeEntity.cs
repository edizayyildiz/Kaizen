namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleRemovedFromEmployeeEntity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Role", c => c.String());
        }
    }
}
