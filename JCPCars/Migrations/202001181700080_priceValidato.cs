namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priceValidato : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Phone", c => c.Int(nullable: false));
            DropColumn("dbo.Cars", "IsHidden");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "IsHidden", c => c.Boolean(nullable: false));
            DropColumn("dbo.Cars", "Phone");
        }
    }
}
