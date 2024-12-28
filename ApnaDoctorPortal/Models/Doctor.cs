using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApnaDoctor.Models
{
   
    public class DoctorProfile
    {

        public ApplicationUser ApplicationUser { get; set; }
        [Key, ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

       
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

       
        public string CNIC { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        public string ProfileMessage { get; set; }

        public string Status { get; set; }

        [Required]
        public DoctorSpecialty DoctorSpecialty { get; set; }
        [ForeignKey("DoctorSpecialty")]
        public int DoctorSpecialtyId { get; set; }

        public int DoctorCategoryId { get; set; }

        public int OnlineDoctor { get; set; }

        public string imgLink { get; set; }

        public string ForPortalStatus { get; set; }

        public string ExtensionCode { get; set; }

        public string Experience { get; set; }

        public string DetailedInformation { get; set; }

        public string Allqualifications { get; set; }

        public string DoctorDutyTime { get; set; }

        public DateTime LastConsultationTime { get; set; }


        public string PhysicalAppointmentPrice { get; set; }

        public string VirtualAppointmentPrice { get; set; }

        public string VideoAppointmentPrice { get; set; }

        public bool VirtualAvailable { get; set; }
        public bool PhysicalAvailable { get; set; }
        public bool InstantVideoAvailable { get; set; }


    }
    public class FeedBack
    {
        [Key]
        public int Id { get; set; }

        public PatientProfile PatientProfile { get; set; }

        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }


        public DoctorProfile DoctorProfile { get; set; }

        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }

        public string FeedbackText { get; set; }

        public float RatingPoints { get; set; }
    }
    public class DoctorQualification
    {
        [Key]
        public int Id { get; set; }
        public DoctorProfile DoctorProfile { get; set; }
        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }
        public string DegreeName { get; set; }
        public string YearTo { get; set; }
        public string YearFrom { get; set; }
        public string Institution { get; set; }

    }
    public class DoctorSpecialty
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class DoctorDeviceToken
    {
        [Required]
        [Key]
        public int ID { get; set; }

        public DoctorProfile DoctorProfile { get; set; }

        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }

        public string DeviceTokenColumn { get; set; }

        public DateTime DateTimeStamp { get; set; }

        public string OS { get; set; }

        public string Status { get; set; }

        public string DeviceName { get; set; }

    }
    public class MedicineNames
    {
        [Key]
        public int Id { get; set; }

        public string MedicineName { get; set; }

        public string Formula { get; set; }
    }
    public class TestNames
    {
        [Key]
        public int Id { get; set; }

        public string Price { get; set; }

        public string Name { get; set; }
    }
    
}