namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminProfiles",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CNIC = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Role = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Consultations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConsultationDate = c.DateTime(nullable: false),
                        Complaint = c.String(),
                        Diagnosis = c.String(),
                        Prescription = c.String(),
                        Tests = c.String(),
                        Remarks = c.String(),
                        CallType = c.String(),
                        Relation = c.String(),
                        DoctorProfileId = c.String(maxLength: 128),
                        PatientProfileId = c.String(maxLength: 128),
                        ConsultationType = c.String(),
                        ConsultationCharges = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId)
                .Index(t => t.DoctorProfileId)
                .Index(t => t.PatientProfileId);
            
            CreateTable(
                "dbo.DoctorProfiles",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CNIC = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        ProfileMessage = c.String(),
                        Status = c.String(),
                        DoctorSpecialtyId = c.Int(nullable: false),
                        DoctorCategoryId = c.Int(nullable: false),
                        OnlineDoctor = c.Int(nullable: false),
                        imgLink = c.String(),
                        ForPortalStatus = c.String(),
                        ExtensionCode = c.String(),
                        Experience = c.String(),
                        DetailedInformation = c.String(),
                        Allqualifications = c.String(),
                        DoctorDutyTime = c.String(),
                        LastConsultationTime = c.DateTime(nullable: false),
                        PhysicalAppointmentPrice = c.String(),
                        VirtualAppointmentPrice = c.String(),
                        VideoAppointmentPrice = c.String(),
                        VirtualAvailable = c.Boolean(nullable: false),
                        PhysicalAvailable = c.Boolean(nullable: false),
                        InstantVideoAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.DoctorSpecialties", t => t.DoctorSpecialtyId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.DoctorSpecialtyId);
            
            CreateTable(
                "dbo.DoctorSpecialties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientProfiles",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        MobileNo = c.String(),
                        Address = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        CNIC = c.String(),
                        MartialStatus = c.String(),
                        PatientCompany = c.String(),
                        Age = c.String(),
                        Gender = c.String(),
                        Weight = c.String(),
                        Height = c.String(),
                        RegistrationDate = c.String(),
                    })
                .PrimaryKey(t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.DeviceTokens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PatientProfileId = c.String(maxLength: 128),
                        DeviceTokenColumn = c.String(),
                        DateTimeStamp = c.DateTime(nullable: false),
                        OS = c.String(),
                        Status = c.String(),
                        DeviceName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId)
                .Index(t => t.PatientProfileId);
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DoctorDeviceTokens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DoctorProfileId = c.String(maxLength: 128),
                        DeviceTokenColumn = c.String(),
                        DateTimeStamp = c.DateTime(nullable: false),
                        OS = c.String(),
                        Status = c.String(),
                        DeviceName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId)
                .Index(t => t.DoctorProfileId);
            
            CreateTable(
                "dbo.DoctorQualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorProfileId = c.String(maxLength: 128),
                        DegreeName = c.String(),
                        YearTo = c.String(),
                        YearFrom = c.String(),
                        Institution = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId)
                .Index(t => t.DoctorProfileId);
            
            CreateTable(
                "dbo.DoctorSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorProfileId = c.String(maxLength: 128),
                        DayFrom = c.String(),
                        DayTo = c.String(),
                        StartTime = c.String(),
                        EndTime = c.String(),
                        Week = c.String(),
                        SlotDuration = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId)
                .Index(t => t.DoctorProfileId);
            
            CreateTable(
                "dbo.DoctorSlotSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorScheduleId = c.Int(nullable: false),
                        SlotDate = c.DateTime(nullable: false),
                        Days = c.String(),
                        Slot = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorSchedules", t => t.DoctorScheduleId, cascadeDelete: true)
                .Index(t => t.DoctorScheduleId);
            
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientProfileId = c.String(maxLength: 128),
                        DoctorProfileId = c.String(maxLength: 128),
                        FeedbackText = c.String(),
                        RatingPoints = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId)
                .Index(t => t.PatientProfileId)
                .Index(t => t.DoctorProfileId);
            
            CreateTable(
                "dbo.InstantVideoPaymentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientProfileId = c.String(nullable: false, maxLength: 128),
                        DoctorProfileId = c.String(nullable: false, maxLength: 128),
                        DateTimeStamp = c.DateTime(nullable: false),
                        CallingTime = c.DateTime(nullable: false),
                        PaymentStatus = c.String(),
                        Status = c.String(),
                        OrderId = c.String(),
                        IsConsultationSaved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId, cascadeDelete: true)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId, cascadeDelete: true)
                .Index(t => t.PatientProfileId)
                .Index(t => t.DoctorProfileId);
            
            CreateTable(
                "dbo.MedicineNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicineName = c.String(),
                        Formula = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientProfileId = c.String(maxLength: 128),
                        DoctorProfileId = c.String(maxLength: 128),
                        DoctorSlotScheduleId = c.Int(nullable: false),
                        Status = c.String(),
                        AppointmentDate = c.DateTime(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        PaymentStatus = c.String(),
                        OrderId = c.String(),
                        AmountCharged = c.String(),
                        Responsepayment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId)
                .ForeignKey("dbo.DoctorSlotSchedules", t => t.DoctorSlotScheduleId, cascadeDelete: true)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId)
                .Index(t => t.PatientProfileId)
                .Index(t => t.DoctorProfileId)
                .Index(t => t.DoctorSlotScheduleId);
            
            CreateTable(
                "dbo.PatientHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientProfileId = c.String(nullable: false, maxLength: 128),
                        DoctorProfileId = c.String(nullable: false, maxLength: 128),
                        PastMedicalhistory = c.String(),
                        PastSurgicalhistory = c.String(),
                        Drughistory = c.String(),
                        GyneorOBShistory = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorProfiles", t => t.DoctorProfileId, cascadeDelete: true)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId, cascadeDelete: true)
                .Index(t => t.PatientProfileId)
                .Index(t => t.DoctorProfileId);
            
            CreateTable(
                "dbo.PatientPreviousDiseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientProfileId = c.String(maxLength: 128),
                        DiseaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diseases", t => t.DiseaseId, cascadeDelete: true)
                .ForeignKey("dbo.PatientProfiles", t => t.PatientProfileId)
                .Index(t => t.PatientProfileId)
                .Index(t => t.DiseaseId);
            
            CreateTable(
                "dbo.PrescribeDetials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConsultationId = c.Int(nullable: false),
                        Day = c.String(),
                        Night = c.String(),
                        Morning = c.String(),
                        Days = c.String(),
                        status = c.String(),
                        quantity = c.String(),
                        AdditionalNotes = c.String(),
                        MedicineNameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consultations", t => t.ConsultationId, cascadeDelete: true)
                .ForeignKey("dbo.MedicineNames", t => t.MedicineNameId, cascadeDelete: true)
                .Index(t => t.ConsultationId)
                .Index(t => t.MedicineNameId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Slots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlotDuration = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeekDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Days = c.String(),
                        StartTime = c.String(),
                        EndTime = c.String(),
                        SlotDuration = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PrescribeDetials", "MedicineNameId", "dbo.MedicineNames");
            DropForeignKey("dbo.PrescribeDetials", "ConsultationId", "dbo.Consultations");
            DropForeignKey("dbo.PatientPreviousDiseases", "PatientProfileId", "dbo.PatientProfiles");
            DropForeignKey("dbo.PatientPreviousDiseases", "DiseaseId", "dbo.Diseases");
            DropForeignKey("dbo.PatientHistories", "PatientProfileId", "dbo.PatientProfiles");
            DropForeignKey("dbo.PatientHistories", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.PatientBookings", "PatientProfileId", "dbo.PatientProfiles");
            DropForeignKey("dbo.PatientBookings", "DoctorSlotScheduleId", "dbo.DoctorSlotSchedules");
            DropForeignKey("dbo.PatientBookings", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.InstantVideoPaymentDetails", "PatientProfileId", "dbo.PatientProfiles");
            DropForeignKey("dbo.InstantVideoPaymentDetails", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.FeedBacks", "PatientProfileId", "dbo.PatientProfiles");
            DropForeignKey("dbo.FeedBacks", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.DoctorSlotSchedules", "DoctorScheduleId", "dbo.DoctorSchedules");
            DropForeignKey("dbo.DoctorSchedules", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.DoctorQualifications", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.DoctorDeviceTokens", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.DeviceTokens", "PatientProfileId", "dbo.PatientProfiles");
            DropForeignKey("dbo.Consultations", "PatientProfileId", "dbo.PatientProfiles");
            DropForeignKey("dbo.PatientProfiles", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Consultations", "DoctorProfileId", "dbo.DoctorProfiles");
            DropForeignKey("dbo.DoctorProfiles", "DoctorSpecialtyId", "dbo.DoctorSpecialties");
            DropForeignKey("dbo.DoctorProfiles", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AdminProfiles", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PrescribeDetials", new[] { "MedicineNameId" });
            DropIndex("dbo.PrescribeDetials", new[] { "ConsultationId" });
            DropIndex("dbo.PatientPreviousDiseases", new[] { "DiseaseId" });
            DropIndex("dbo.PatientPreviousDiseases", new[] { "PatientProfileId" });
            DropIndex("dbo.PatientHistories", new[] { "DoctorProfileId" });
            DropIndex("dbo.PatientHistories", new[] { "PatientProfileId" });
            DropIndex("dbo.PatientBookings", new[] { "DoctorSlotScheduleId" });
            DropIndex("dbo.PatientBookings", new[] { "DoctorProfileId" });
            DropIndex("dbo.PatientBookings", new[] { "PatientProfileId" });
            DropIndex("dbo.InstantVideoPaymentDetails", new[] { "DoctorProfileId" });
            DropIndex("dbo.InstantVideoPaymentDetails", new[] { "PatientProfileId" });
            DropIndex("dbo.FeedBacks", new[] { "DoctorProfileId" });
            DropIndex("dbo.FeedBacks", new[] { "PatientProfileId" });
            DropIndex("dbo.DoctorSlotSchedules", new[] { "DoctorScheduleId" });
            DropIndex("dbo.DoctorSchedules", new[] { "DoctorProfileId" });
            DropIndex("dbo.DoctorQualifications", new[] { "DoctorProfileId" });
            DropIndex("dbo.DoctorDeviceTokens", new[] { "DoctorProfileId" });
            DropIndex("dbo.DeviceTokens", new[] { "PatientProfileId" });
            DropIndex("dbo.PatientProfiles", new[] { "ApplicationUserId" });
            DropIndex("dbo.DoctorProfiles", new[] { "DoctorSpecialtyId" });
            DropIndex("dbo.DoctorProfiles", new[] { "ApplicationUserId" });
            DropIndex("dbo.Consultations", new[] { "PatientProfileId" });
            DropIndex("dbo.Consultations", new[] { "DoctorProfileId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AdminProfiles", new[] { "ApplicationUserId" });
            DropTable("dbo.WeekDays");
            DropTable("dbo.TestNames");
            DropTable("dbo.Slots");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PrescribeDetials");
            DropTable("dbo.PatientPreviousDiseases");
            DropTable("dbo.PatientHistories");
            DropTable("dbo.PatientBookings");
            DropTable("dbo.MedicineNames");
            DropTable("dbo.InstantVideoPaymentDetails");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.DoctorSlotSchedules");
            DropTable("dbo.DoctorSchedules");
            DropTable("dbo.DoctorQualifications");
            DropTable("dbo.DoctorDeviceTokens");
            DropTable("dbo.Diseases");
            DropTable("dbo.DeviceTokens");
            DropTable("dbo.PatientProfiles");
            DropTable("dbo.DoctorSpecialties");
            DropTable("dbo.DoctorProfiles");
            DropTable("dbo.Consultations");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AdminProfiles");
        }
    }
}
