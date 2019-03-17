namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SuggestionTitleChangedToSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suggestions", "Subject", c => c.String());
            DropColumn("dbo.Suggestions", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Suggestions", "Title", c => c.String());
            DropColumn("dbo.Suggestions", "Subject");
        }
    }
}
