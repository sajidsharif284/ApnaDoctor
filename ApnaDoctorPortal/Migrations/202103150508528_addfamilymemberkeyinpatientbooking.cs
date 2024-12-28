namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfamilymemberkeyinpatientbooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientBookings", "FamilyMemberId", c => c.Int(nullable: true));
            CreateIndex("dbo.PatientBookings", "FamilyMemberId");
            AddForeignKey("dbo.PatientBookings", "FamilyMemberId", "dbo.FamilyMembers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientBookings", "FamilyMemberId", "dbo.FamilyMembers");
            DropIndex("dbo.PatientBookings", new[] { "FamilyMemberId" });
            DropColumn("dbo.PatientBookings", "FamilyMemberId");
        }
    }
}
