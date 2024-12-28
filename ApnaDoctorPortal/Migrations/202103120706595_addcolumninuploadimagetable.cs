namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumninuploadimagetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UploadImages", "CompanyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UploadImages", "CompanyName");
        }
    }
}
