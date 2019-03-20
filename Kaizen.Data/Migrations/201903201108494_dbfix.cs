namespace Kaizen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Counties", "CityId", "dbo.Cities");
            DropIndex("dbo.Counties", new[] { "CityId" });
            AlterColumn("dbo.Counties", "CityId", c => c.Guid());
            CreateIndex("dbo.Counties", "CityId");
            AddForeignKey("dbo.Counties", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Counties", "CityId", "dbo.Cities");
            DropIndex("dbo.Counties", new[] { "CityId" });
            AlterColumn("dbo.Counties", "CityId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Counties", "CityId");
            AddForeignKey("dbo.Counties", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
