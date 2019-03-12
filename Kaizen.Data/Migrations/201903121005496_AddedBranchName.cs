namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBranchName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "Name");
        }
    }
}
