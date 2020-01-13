namespace JCPCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TESTCleanupOfManageAndAccount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserData_FirstName");
            DropColumn("dbo.AspNetUsers", "UserData_LastName");
            DropColumn("dbo.AspNetUsers", "UserData_Address");
            DropColumn("dbo.AspNetUsers", "UserData_CodeAndCity");
            DropColumn("dbo.AspNetUsers", "UserData_PhoneNumber");
            DropColumn("dbo.AspNetUsers", "UserData_Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserData_Email", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_CodeAndCity", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_FirstName", c => c.String());
        }
    }
}
