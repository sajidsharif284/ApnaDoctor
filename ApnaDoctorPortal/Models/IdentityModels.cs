using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ApnaDoctorPortal.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApnaDoctor.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; internal set; }
        
        public int ClientTableId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.DoctorProfile> DoctorProfiles { get; set; }
        public System.Data.Entity.DbSet<ApnaDoctor.Models.DoctorQualification> DoctorQualifications { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.PatientProfile> PatientProfiles { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.PatientPreviousDisease> PatientPreviousDiseases { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.Slots> Slots { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.WeekDays> WeekDays { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.DoctorSchedule> DoctorSchedules { get; set; }
        public System.Data.Entity.DbSet<ApnaDoctor.Models.Disease> Diseases { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.DoctorSlotSchedule> DoctorSlotSchedules { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.FeedBack> FeedBacks { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.DoctorSpecialty> DoctorSpecialties { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.PatientBookings> PatientBookings { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.DeviceToken> DeviceTokens { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.DoctorDeviceToken> DoctorDeviceTokens { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.MedicineNames> MedicineNames { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.TestNames> TestNames { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.Consultation> Consultations { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.PrescribeDetials> PrescribeDetials { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.PatientHistory> PatientHistories { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.AdminProfile> AdminProfiles { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.InstantVideoPaymentDetails> InstantVideoPaymentDetails { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctorPortal.Models.ClientTable> ClientTables { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctorPortal.Models.UploadImage> UploadImages { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.FamilyMember> FamilyMembers { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctor.Models.FreeCall> FreeCalls { get; set; }

        public System.Data.Entity.DbSet<ApnaDoctorPortal.Models.InsuranceCompany> InsuranceCompanies { get; set; }
    }
}