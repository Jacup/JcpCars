namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dates1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderItems", "ToDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "ToDate");
            DropColumn("dbo.OrderItems", "FromDate");
        }
    }
}
