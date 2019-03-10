namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssessmentTypeAssessmentForSuggestion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Suggestions", "Assessment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suggestions", "Assessment", c => c.String(nullable: false));
        }
    }
}
