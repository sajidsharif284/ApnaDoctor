namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addserverkeyinclienttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientTables", "ServerKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientTables", "ServerKey");
        }
    }
}
