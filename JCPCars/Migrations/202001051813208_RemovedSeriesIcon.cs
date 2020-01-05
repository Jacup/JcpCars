namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSeriesIcon : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Series", "IconFilename");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "IconFilename", c => c.String());
        }
    }
}
