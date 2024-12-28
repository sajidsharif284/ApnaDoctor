namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteserverkeyinclienttable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClientTables", "ServerKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientTables", "ServerKey", c => c.String());
        }
    }
}
