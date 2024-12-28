namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinsurancecompanytable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InsuranceCompanies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PicURL = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InsuranceCompanies");
        }
    }
}
