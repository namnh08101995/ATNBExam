namespace ATNB.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AirPlanes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 7),
                        Model = c.String(nullable: false, maxLength: 40),
                        AirPlaneType = c.String(nullable: false, maxLength: 3),
                        CruiseSpeed = c.Double(),
                        EmptyWeight = c.Double(),
                        MaxTakeoffWeight = c.Double(),
                        MinNeededRunwaySize = c.Double(),
                        AirPortId = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AirPorts", t => t.AirPortId)
                .Index(t => t.AirPortId);
            
            CreateTable(
                "dbo.AirPorts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 7),
                        Name = c.String(nullable: false, maxLength: 255),
                        RunwaySize = c.Double(nullable: false),
                        MaxFWParkingPlace = c.Int(nullable: false),
                        MaxRWParkingPlace = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Helicopters",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 7),
                        Model = c.String(nullable: false, maxLength: 40),
                        CruiseSpeed = c.Double(),
                        EmptyWeight = c.Double(),
                        MaxTakeoffWeight = c.Double(),
                        Range = c.Double(),
                        AirPortId = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AirPorts", t => t.AirPortId)
                .Index(t => t.AirPortId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Helicopters", "AirPortId", "dbo.AirPorts");
            DropForeignKey("dbo.AirPlanes", "AirPortId", "dbo.AirPorts");
            DropIndex("dbo.Helicopters", new[] { "AirPortId" });
            DropIndex("dbo.AirPlanes", new[] { "AirPortId" });
            DropTable("dbo.Helicopters");
            DropTable("dbo.AirPorts");
            DropTable("dbo.AirPlanes");
        }
    }
}
