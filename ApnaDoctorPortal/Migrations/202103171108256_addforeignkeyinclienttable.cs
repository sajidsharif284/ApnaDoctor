namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyinclienttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UploadImages", "ClientTableId", c => c.Int(nullable: false));
            CreateIndex("dbo.UploadImages", "ClientTableId");
            AddForeignKey("dbo.UploadImages", "ClientTableId", "dbo.ClientTables", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UploadImages", "ClientTableId", "dbo.ClientTables");
            DropIndex("dbo.UploadImages", new[] { "ClientTableId" });
            DropColumn("dbo.UploadImages", "ClientTableId");
        }
    }
}
