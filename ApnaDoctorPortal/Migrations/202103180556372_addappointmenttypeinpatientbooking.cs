namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addappointmenttypeinpatientbooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientBookings", "AppointmentType", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientBookings", "AppointmentType");
        }
    }
}
