using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApnaDoctorPortal.Models
{
    public class ClientTable
    {
        [Key]
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Subdomain { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentCycle { get; set; }

        public string Status { get; set; }

        public string POCName { get; set; }

        public string POCEmail { get; set; }

        public string POCPhoneNumber { get; set; }

        public string Gmail { get; set; }

        public string GmailPassword { get; set; }

        public string BasePath { get; set; }

        public string PackageNameIOS { get; set; }

        public string PackageNameAndroid { get; set; }

        public string AmountPay { get; set; }

        public string VoipCallingCertificateOfDoctor { get; set; }

        public string VoipCallingCertificateOfPatient { get; set; }

        public string AndroidServerKey { get; set; }


    }
    public class UploadImage
    {
        [Key]
        public int id { get; set; }

        public ClientTable ClientTable { get; set; }
        [ForeignKey("ClientTable")]
        public int ClientTableId { get; set; }

        public string CompanyName { get; set; }
        public string Images { get; set; }
    }
    public class InsuranceCompany
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string PicURL { get; set; }
        public string Status { get; set; }
    }

}