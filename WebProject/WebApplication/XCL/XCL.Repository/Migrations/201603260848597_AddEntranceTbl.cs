namespace XCL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntranceTbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flats", "BuildingId", "dbo.BuildingInfoes");
            DropForeignKey("dbo.Sensors", "BuildingId", "dbo.BuildingInfoes");
            DropIndex("dbo.Flats", new[] { "BuildingId" });
            CreateTable(
                "dbo.Entrances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                        EntranceNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BuildingInfoes", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            AddColumn("dbo.Flats", "EntranceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Flats", "EntranceId");
            AddForeignKey("dbo.Flats", "EntranceId", "dbo.Entrances", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sensors", "BuildingId", "dbo.Entrances", "Id", cascadeDelete: true);
            DropColumn("dbo.Flats", "BuildingId");
            DropColumn("dbo.Flats", "EntranceNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flats", "EntranceNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Flats", "BuildingId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sensors", "BuildingId", "dbo.Entrances");
            DropForeignKey("dbo.Flats", "EntranceId", "dbo.Entrances");
            DropForeignKey("dbo.Entrances", "BuildingId", "dbo.BuildingInfoes");
            DropIndex("dbo.Entrances", new[] { "BuildingId" });
            DropIndex("dbo.Flats", new[] { "EntranceId" });
            DropColumn("dbo.Flats", "EntranceId");
            DropTable("dbo.Entrances");
            CreateIndex("dbo.Flats", "BuildingId");
            AddForeignKey("dbo.Sensors", "BuildingId", "dbo.BuildingInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Flats", "BuildingId", "dbo.BuildingInfoes", "Id", cascadeDelete: true);
        }
    }
}
