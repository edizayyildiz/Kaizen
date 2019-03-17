namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitleAddedToSuggestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suggestions", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suggestions", "Title");
        }
    }
}
