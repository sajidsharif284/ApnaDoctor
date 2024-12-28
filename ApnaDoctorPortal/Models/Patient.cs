using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApnaDoctor.Models
{
    public class PatientProfile
    {
        public ApplicationUser ApplicationUser { get; set; }
        [Key,ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string MobileNo { get; set; }

        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CNIC { get; set; }
        public string MartialStatus { get; set; }
        public string PatientCompany { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public string Weight { get; set; }

        public string Height { get; set; }

        public string RegistrationDate { get; set; }

    }
    public class PatientPreviousDisease
    {
        [Key]
        public int Id { get; set; }

        public PatientProfile PatientProfile { get; set; }
        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }

        public Disease Disease { get; set; }
        [ForeignKey("Disease")]
        public int DiseaseId { get; set; }
    }


    public class Disease
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PatientBookings
    {
        [Key]
        public int Id { get; set; }

        public PatientProfile PatientProfile { get; set; }
        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }

        public FamilyMember FamilyMember { get; set; }
        [ForeignKey("FamilyMember")]
        public int FamilyMemberId { get; set; }

        public DoctorProfile DoctorProfile { get; set; }
        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }

        public DoctorSlotSchedule DoctorSlotSchedule { get; set; }
        [ForeignKey("DoctorSlotSchedule")]
        public int DoctorSlotScheduleId { get; set; }

        public string Status { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string AppointmentType { get; set; }

        public DateTime BookingDate { get; set; }

        public string PaymentStatus { get; set; }
        public string OrderId { get; set; }
        public string AmountCharged { get; set; }
        public string Responsepayment { get; set; }
    }
    public class DeviceToken
    {
        [Required]
        [Key]
        public int ID { get; set; }

        public PatientProfile PatientProfile { get; set; }

        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }

        public string DeviceTokenColumn { get; set; }

        public DateTime DateTimeStamp { get; set; }

        public string OS { get; set; }

        public string Status { get; set; }

        public string DeviceName { get; set; }

    }
}