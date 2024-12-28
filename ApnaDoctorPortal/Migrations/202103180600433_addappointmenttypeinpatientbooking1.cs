namespace ApnaDoctorPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addappointmenttypeinpatientbooking1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientBookings", "AppointmentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientBookings", "AppointmentType");
        }
    }
}
