namespace XCL.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameBuildintIdColumnToEntranceIdInSensorTbl : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Sensors", name: "BuildingId", newName: "EntranceId");
            RenameIndex(table: "dbo.Sensors", name: "IX_BuildingId", newName: "IX_EntranceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Sensors", name: "IX_EntranceId", newName: "IX_BuildingId");
            RenameColumn(table: "dbo.Sensors", name: "EntranceId", newName: "BuildingId");
        }
    }
}
