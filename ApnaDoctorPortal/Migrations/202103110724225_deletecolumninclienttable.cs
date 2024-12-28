namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletecolumninclienttable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClientTables", "AuthSecret");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientTables", "AuthSecret", c => c.String());
        }
    }
}
