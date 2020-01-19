namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cars", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Phone", c => c.Int(nullable: false));
        }
    }
}
