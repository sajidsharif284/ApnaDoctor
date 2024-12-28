namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumninclienttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientTables", "AmountPay", c => c.String());
            AddColumn("dbo.ClientTables", "Images", c => c.String());
            AddColumn("dbo.ClientTables", "VoipCallingCertificate", c => c.String());
            AddColumn("dbo.ClientTables", "AndroidServerKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientTables", "AndroidServerKey");
            DropColumn("dbo.ClientTables", "VoipCallingCertificate");
            DropColumn("dbo.ClientTables", "Images");
            DropColumn("dbo.ClientTables", "AmountPay");
        }
    }
}
