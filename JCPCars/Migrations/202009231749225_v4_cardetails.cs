namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4_cardetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Location", c => c.String());
            AddColumn("dbo.Cars", "Fuel", c => c.String());
            AddColumn("dbo.Cars", "Gearbox", c => c.String());
            AddColumn("dbo.Cars", "ProductionYear", c => c.String());
            AddColumn("dbo.Cars", "Power", c => c.String());
            AddColumn("dbo.Cars", "PictureFileName2", c => c.String());
            AddColumn("dbo.Cars", "PictureFileName3", c => c.String());
            AddColumn("dbo.Cars", "PricePer1h", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "PricePer24h", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "PricePer1m", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "IsHighlited", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cars", "PictureFileName", c => c.String(nullable: false));
            DropColumn("dbo.Cars", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "PictureFileName", c => c.String());
            DropColumn("dbo.Cars", "IsHighlited");
            DropColumn("dbo.Cars", "PricePer1m");
            DropColumn("dbo.Cars", "PricePer24h");
            DropColumn("dbo.Cars", "PricePer1h");
            DropColumn("dbo.Cars", "PictureFileName3");
            DropColumn("dbo.Cars", "PictureFileName2");
            DropColumn("dbo.Cars", "Power");
            DropColumn("dbo.Cars", "ProductionYear");
            DropColumn("dbo.Cars", "Gearbox");
            DropColumn("dbo.Cars", "Fuel");
            DropColumn("dbo.Cars", "Location");
        }
    }
}
