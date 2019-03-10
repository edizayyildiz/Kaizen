namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PositionNameFixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Position", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Employees", "Posation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Posation", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Employees", "Position");
        }
    }
}
