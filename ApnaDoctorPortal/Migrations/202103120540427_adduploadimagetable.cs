namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduploadimagetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadImages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Images = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            DropColumn("dbo.ClientTables", "Images");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientTables", "Images", c => c.String());
            DropTable("dbo.UploadImages");
        }
    }
}
