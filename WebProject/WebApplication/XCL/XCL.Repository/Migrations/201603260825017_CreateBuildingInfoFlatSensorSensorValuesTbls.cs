namespace XCL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBuildingInfoFlatSensorSensorValuesTbls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                        EntranceNumber = c.Int(nullable: false),
                        FlatNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BuildingInfoes", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.BuildingInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetName = c.String(maxLength: 150),
                        StreetNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dimension = c.String(maxLength: 10),
                        Description = c.String(maxLength: 1000),
                        InstallationPosition = c.Int(nullable: false),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BuildingInfoes", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.SensorValues",
                c => new
                    {
                        DateTime = c.DateTime(nullable: false),
                        SensorId = c.Int(nullable: false),
                        Value = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.DateTime)
                .ForeignKey("dbo.Sensors", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId);
            
            AddColumn("dbo.Account", "EmailIsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Account", "PhoneNumberIsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Account", "AccountIsVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.Account", "SendEmailNotification", c => c.Boolean(nullable: false));
            AddColumn("dbo.Account", "SendPhoneNotification", c => c.Boolean(nullable: false));
            AddColumn("dbo.Account", "FlatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Account", "FlatId");
            AddForeignKey("dbo.Account", "FlatId", "dbo.Flats", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensors", "BuildingId", "dbo.BuildingInfoes");
            DropForeignKey("dbo.SensorValues", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.Flats", "BuildingId", "dbo.BuildingInfoes");
            DropForeignKey("dbo.Account", "FlatId", "dbo.Flats");
            DropIndex("dbo.SensorValues", new[] { "SensorId" });
            DropIndex("dbo.Sensors", new[] { "BuildingId" });
            DropIndex("dbo.Flats", new[] { "BuildingId" });
            DropIndex("dbo.Account", new[] { "FlatId" });
            DropColumn("dbo.Account", "FlatId");
            DropColumn("dbo.Account", "SendPhoneNotification");
            DropColumn("dbo.Account", "SendEmailNotification");
            DropColumn("dbo.Account", "AccountIsVerified");
            DropColumn("dbo.Account", "PhoneNumberIsConfirmed");
            DropColumn("dbo.Account", "EmailIsConfirmed");
            DropTable("dbo.SensorValues");
            DropTable("dbo.Sensors");
            DropTable("dbo.BuildingInfoes");
            DropTable("dbo.Flats");
        }
    }
}
