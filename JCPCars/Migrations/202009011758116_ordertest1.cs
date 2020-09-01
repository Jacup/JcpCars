namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordertest1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        Serie_SerieId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.Serie_SerieId)
                .Index(t => t.OrderId)
                .Index(t => t.Serie_SerieId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 150),
                        Address = c.String(nullable: false, maxLength: 150),
                        CodeAndCity = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        Comment = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        OrderState = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "Serie_SerieId", "dbo.Series");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderItems", new[] { "Serie_SerieId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
        }
    }
}
