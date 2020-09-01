namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartv2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "Serie_SerieId", "dbo.Series");
            DropIndex("dbo.OrderItems", new[] { "Serie_SerieId" });
            AddColumn("dbo.OrderItems", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItems", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.OrderItems", "CarId");
            AddForeignKey("dbo.OrderItems", "CarId", "dbo.Cars", "CarId", cascadeDelete: true);
            DropColumn("dbo.OrderItems", "Serie_SerieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Serie_SerieId", c => c.Int());
            DropForeignKey("dbo.OrderItems", "CarId", "dbo.Cars");
            DropIndex("dbo.OrderItems", new[] { "CarId" });
            DropColumn("dbo.OrderItems", "UnitPrice");
            DropColumn("dbo.OrderItems", "Quantity");
            CreateIndex("dbo.OrderItems", "Serie_SerieId");
            AddForeignKey("dbo.OrderItems", "Serie_SerieId", "dbo.Series", "SerieId");
        }
    }
}
