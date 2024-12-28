namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcertificateinclienttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientTables", "VoipCallingCertificateOfDoctor", c => c.String());
            AddColumn("dbo.ClientTables", "VoipCallingCertificateOfPatient", c => c.String());
            DropColumn("dbo.ClientTables", "VoipCallingCertificate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientTables", "VoipCallingCertificate", c => c.String());
            DropColumn("dbo.ClientTables", "VoipCallingCertificateOfPatient");
            DropColumn("dbo.ClientTables", "VoipCallingCertificateOfDoctor");
        }
    }
}
