using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApnaDoctor.Models
{
    public class Consultation
    {
        public int Id { get; set; }

        public DateTime ConsultationDate { get; set; }

        //public InsuranceProduct InsuranceProduct { get; set; }

        //public int? InsuranceProductId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Complaint { get; set; }

        [DataType(DataType.MultilineText)]
        public string Diagnosis { get; set; }

        [DataType(DataType.MultilineText)]
        public string Prescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Tests { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }


        [DataType(DataType.MultilineText)]
        public string CallType { get; set; }

        [DataType(DataType.MultilineText)]
        public string Relation { get; set; }


        public DoctorProfile DoctorProfile { get; set; }

        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }

        public PatientProfile PatientProfile { get; set; }

        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }

        public string ConsultationType { get; set; }

        //public ConsultationPaymentMethod ConsultationPaymentMethod { get; set; }

        //public int ConsultationPaymentMethodId { get; set; }

        public double ConsultationCharges { get; set; }



        //public string PatientId { get; set; }
    }
    public class PrescribeDetials
    {
        [Key]
        public int Id { get; set; }
        public Consultation Consultation { get; set; }

        [ForeignKey("Consultation")]
        public int ConsultationId { get; set; }


        public string Day { get; set; }

        public string Night { get; set; }

        public string Morning { get; set; }

        public string Days { get; set; }

        public string status { get; set; }
        public string quantity { get; set; }

        public string AdditionalNotes { get; set; }

        public MedicineNames MedicineNames { get; set; }
        [ForeignKey("MedicineNames")]
        public int MedicineNameId { get; set; }
    }
    public class PatientHistory
    {
        [Key]
        public int Id { get; set; }

        public PatientProfile PatientProfile { get; set; }

        [Required]
        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }

        public DoctorProfile DoctorProfile { get; set; }

        [Required]
        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }
       
        public string PastMedicalhistory { get; set; }
        
        public string PastSurgicalhistory { get; set; }
       
        public string Drughistory { get; set; }
        
        public string GyneorOBShistory { get; set; }

    }
    public class NewConsultationViewModel
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public Consultation Consultation { get; set; }
        public IEnumerable<TestNames> TestNames { get; set; }
    }
    public class InstantVideoPaymentDetails
    {
        [Key]
        public int Id { get; set; }

        public PatientProfile PatientProfile { get; set; }

        [Required]
        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }

        public DoctorProfile DoctorProfile { get; set; }

        [Required]
        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }

        public DateTime DateTimeStamp { get; set; }

        public DateTime CallingTime { get; set; }

        public string PaymentStatus { get; set; }

        public string Status { get; set; }

        public string OrderId { get; set; }

        public bool IsConsultationSaved { get; set; }

    }
    public class FamilyMember
    {
        [Key]
        public int Id { get; set; }

        public PatientProfile PatientProfile { get; set; }

        [Required]
        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }
        
        public string Relation { get; set; }

        public string Name { get; set; }

    }
    public class FreeCall
    {
        [Key]
        public int Id { get; set; }

        public PatientProfile PatientProfile { get; set; }

        [Required]
        [ForeignKey("PatientProfile")]
        public string PatientProfileId { get; set; }

        public DoctorProfile DoctorProfile { get; set; }

        [Required]
        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }

        public Consultation Consultation { get; set; }

        [Required]
        [ForeignKey("Consultation")]
        public int ConsultationId { get; set; }

        public int TotalFreeCall { get; set; }

        public int TotalUsedFreeCall { get; set; }

        public int TotalAvailableFreeCall { get; set; }

        public DateTime DateTime { get; set; }

    }
}