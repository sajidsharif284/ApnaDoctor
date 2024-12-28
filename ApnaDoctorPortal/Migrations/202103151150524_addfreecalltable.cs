namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfreecalltable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FreeCalls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientProfileId = c.String(nullable: false, maxLength: 128),
                        DoctorProfileId = c.String(nullable: false, maxLength: 128),
                        ConsultationId = c.Int(nullable: false),
                        TotalFreeCall = c.Int(nullable: false),
                        TotalUsedFreeCall = c.Int(nullable: false),
                        TotalAvailableFreeCall = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consultations", t => t.ConsultationId, cascadeDelete: true)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId, cascadeDelete: true)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId, cascadeDelete: true)
                .Index(t => t.PatientProfileId)
                .Index(t => t.DoctorProfileId)
                .Index(t => t.ConsultationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FreeCalls", "PatientProfileId", "dbo.PatientProfiles");
            DropForeignKey("dbo.FreeCalls", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.FreeCalls", "ConsultationId", "dbo.Consultations");
            DropIndex("dbo.FreeCalls", new[] { "ConsultationId" });
            DropIndex("dbo.FreeCalls", new[] { "DoctorProfileId" });
            DropIndex("dbo.FreeCalls", new[] { "PatientProfileId" });
            DropTable("dbo.FreeCalls");
        }
    }
}
