namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteappointmenttypeinpatientbooking : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PatientBookings", "AppointmentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientBookings", "AppointmentType", c => c.DateTime(nullable: false));
        }
    }
}
