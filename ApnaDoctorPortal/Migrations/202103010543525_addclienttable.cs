namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclienttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.Int(nullable: false),
                        Subdomain = c.String(),
                        PaymentStatus = c.String(),
                        PaymentCycle = c.String(),
                        Status = c.String(),
                        POCName = c.String(),
                        POCEmail = c.String(),
                        POCPhoneNumber = c.String(),
                        Gmail = c.String(),
                        GmailPassword = c.String(),
                        AuthSecret = c.String(),
                        BasePath = c.String(),
                        PackageNameIOS = c.String(),
                        PackageNameAndroid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClientTables");
        }
    }
}
