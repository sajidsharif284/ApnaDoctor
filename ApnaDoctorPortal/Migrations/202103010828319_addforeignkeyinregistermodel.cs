namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyinregistermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ClientTableId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ClientTableId");
        }
    }
}
