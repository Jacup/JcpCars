namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Content", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Content", c => c.String());
        }
    }
}
