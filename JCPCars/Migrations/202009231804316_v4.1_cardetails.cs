namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v41_cardetails : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "PictureFileName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "PictureFileName", c => c.String(nullable: false));
        }
    }
}
