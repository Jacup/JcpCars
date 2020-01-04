namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                {
                    CarId = c.Int(nullable: false, identity: true),
                    SerieId = c.Int(nullable: false),
                    CarModel = c.String(nullable: false),
                    CarBrand = c.String(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    PictureFileName = c.String(),
                    Description = c.String(),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    IsHidden = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.Series", t => t.SerieId, cascadeDelete: true)
                .Index(t => t.SerieId);

            CreateTable(
                "dbo.Series",
                c => new
                {
                    SerieId = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Description = c.String(),
                    IconFilename = c.String(),
                })
                .PrimaryKey(t => t.SerieId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "SerieId", "dbo.Series");
            DropIndex("dbo.Cars", new[] { "SerieId" });
            DropTable("dbo.Series");
            DropTable("dbo.Cars");
        }
    }
}