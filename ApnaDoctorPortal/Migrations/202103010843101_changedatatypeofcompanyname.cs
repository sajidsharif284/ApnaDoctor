namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatatypeofcompanyname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientTables", "CompanyName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientTables", "CompanyName", c => c.Int(nullable: false));
        }
    }
}
