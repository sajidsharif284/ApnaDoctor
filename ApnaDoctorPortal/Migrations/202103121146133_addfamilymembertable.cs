namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfamilymembertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FamilyMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientProfileId = c.String(nullable: false, maxLength: 128),
                        Relation = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId, cascadeDelete: true)
                .Index(t => t.PatientProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FamilyMembers", "PatientProfileId", "dbo.PatientProfiles");
            DropIndex("dbo.FamilyMembers", new[] { "PatientProfileId" });
            DropTable("dbo.FamilyMembers");
        }
    }
}
