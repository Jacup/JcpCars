namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNewArrivals : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Series", "IsNewArrival");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "IsNewArrival", c => c.Boolean(nullable: false));
        }
    }
}
