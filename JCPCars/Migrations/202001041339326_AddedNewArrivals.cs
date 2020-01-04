namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewArrivals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Series", "IsNewArrival", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Series", "IsNewArrival");
        }
    }
}
