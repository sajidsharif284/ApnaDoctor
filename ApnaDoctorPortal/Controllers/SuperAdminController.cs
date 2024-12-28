using ApnaDoctor.Models;
using ApnaDoctorPortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace ApnaDoctor.Controllers
{
    public class SuperAdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: SuperAdmin
        public ActionResult Index()
        {
            var totalclinic = db.ClientTables.ToList();
            return View(totalclinic);
        }
        // ===================  Add ,Edit and Details Of Clients========================================================ss
        public ActionResult AddClient()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddClient(ClientTable clienttable,FormCollection fc, HttpPostedFileBase file, HttpPostedFileBase file2, IEnumerable<HttpPostedFileBase> Images)
        {
            string directory1 = Path.Combine(Server.MapPath("~/VoipCertificate/" + clienttable.CompanyName));
            if (!Directory.Exists(directory1))
            {
                Directory.CreateDirectory(directory1);
            }
            string pathofimage1 = Path.Combine(Server.MapPath("~/VoipCertificate/" + clienttable.CompanyName), file.FileName);
            file.SaveAs(pathofimage1);
            string makeDirectory1 = "https://apnadoctor.webddocsystems.com/VoipCertificate/" + clienttable.CompanyName + "/" + file.FileName;

            string pathofimage2 = Path.Combine(Server.MapPath("~/VoipCertificate/" + clienttable.CompanyName), file2.FileName);
            file.SaveAs(pathofimage2);
            string makeDirectory2 = "https://apnadoctor.webddocsystems.com/VoipCertificate/" + clienttable.CompanyName + "/" + file2.FileName;
            
            clienttable.VoipCallingCertificateOfDoctor = makeDirectory1;
            clienttable.VoipCallingCertificateOfPatient = makeDirectory2;
            db.ClientTables.Add(clienttable);
            db.SaveChanges();

            UploadImage img = new UploadImage();
            string makeDirectory = Path.Combine(Server.MapPath("~/UploadImages/" + clienttable.CompanyName));
            if (!Directory.Exists(makeDirectory))
            {
                Directory.CreateDirectory(makeDirectory);
            }
            foreach (var image in Images)
            {

                string pathofimage = Path.Combine(Server.MapPath("~/UploadImages/" + clienttable.CompanyName), image.FileName);
                image.SaveAs(pathofimage);
                img.Images = "https://apnadoctor.webddocsystems.com/UploadImages/" + clienttable.CompanyName + "/" + image.FileName;
                img.CompanyName = clienttable.CompanyName;
                img.ClientTableId = clienttable.Id;
                db.UploadImages.Add(img);
                db.SaveChanges();

            }


            return RedirectToAction("Index");


        }

        public ActionResult DetailClients(string id)
        {
            var id1 = Int32.Parse(id);
            var totalclinic = db.ClientTables.Where(y => y.Id == id1).FirstOrDefault();
            return View(totalclinic);
        }
        public ActionResult EditClient(string id)
        {
            var id1 = Int32.Parse(id);
            var list = db.ClientTables.Where(y => y.Id == id1).FirstOrDefault();
            ViewBag.clientid = id;
            return View(list);
        }
        [HttpPost]
        public ActionResult EditClient(FormCollection fc, ClientTable client, HttpPostedFileBase file, HttpPostedFileBase file2)
        {
            string directory1 = Path.Combine(Server.MapPath("~/VoipCertificate/" + client.CompanyName));
            if (!Directory.Exists(directory1))
            {
                Directory.CreateDirectory(directory1);
            }

            string pathofimage1 = Path.Combine(Server.MapPath("~/VoipCertificate/" + client.CompanyName), file.FileName);
            file.SaveAs(pathofimage1);
            string makeDirectory1 = "https://apnadoctor.webddocsystems.com/VoipCertificate/" + client.CompanyName + "/" + file.FileName;

            string pathofimage2 = Path.Combine(Server.MapPath("~/VoipCertificate/" + client.CompanyName), file2.FileName);
            file.SaveAs(pathofimage2);
            string makeDirectory2 = "https://apnadoctor.webddocsystems.com/VoipCertificate/" + client.CompanyName + "/" + file2.FileName;


            var id = fc["Id"];
            var id1 = Int32.Parse(id);
            var list = db.ClientTables.Where(y => y.Id == id1).FirstOrDefault();
            list.CompanyName = client.CompanyName;
            list.Subdomain = client.Subdomain;
            list.PaymentStatus = client.PaymentStatus;
            list.PaymentCycle = client.PaymentCycle;
            list.Status = client.Status;
            list.POCEmail = client.POCEmail;
            list.POCName = client.POCName;
            list.POCPhoneNumber = client.POCPhoneNumber;
            list.Gmail = client.Gmail;
            list.GmailPassword = client.GmailPassword;
            list.PackageNameAndroid = client.PackageNameAndroid;
            list.PackageNameIOS = client.PackageNameIOS;
            list.BasePath = client.BasePath;
            list.AmountPay = client.AmountPay;
            list.AndroidServerKey = client.AndroidServerKey;
            list.VoipCallingCertificateOfDoctor = makeDirectory1;
            list.VoipCallingCertificateOfPatient = makeDirectory2;
            db.SaveChanges();
            return RedirectToAction("DetailClients", new { id = id });
        }

        // ===================  List Of All Clinics ========================================================ss
        public ActionResult ListClinics(string id)
        {
            var list = db.ClientTables.ToList();
            if (id != null)
            {
                var id1 = Int32.Parse(id);
                var totaldoctors = db.DoctorProfiles.Where(y => y.ApplicationUser.ClientTableId == id1).ToList();
                ViewBag.doctors = totaldoctors.Count();
                var totalpatient = db.PatientProfiles.Where(y => y.ApplicationUser.ClientTableId == id1).ToList();
                ViewBag.patient = totalpatient.Count();
                var totalamount = db.PatientBookings.Where(y => y.PaymentStatus == "success" && y.PatientProfile.ApplicationUser.ClientTableId==id1).ToList();
                int sum = 0;
                foreach (var item in totalamount)
                {
                    sum += Convert.ToInt32(item.AmountCharged);
                }
                ViewBag.amount = sum;
                var totalappointment = db.PatientBookings.Where(y=>y.PatientProfile.ApplicationUser.ClientTableId==id1 &&(y.Status == "Complete" || y.Status == "Missed")).ToList();
                ViewBag.appointment = totalappointment.Count();

                var Amount = db.ClientTables.Where(y =>y.Id==id1).FirstOrDefault();
                ViewBag.TotalAmountPay = Amount.AmountPay;
            }
            return View(list);
        }
        public ActionResult ListClinicsNew(string id)
        {
            var id1 = Int32.Parse(id);
            var totaldoctors = db.DoctorProfiles.Where(y => y.ApplicationUser.ClientTableId == id1).ToList();
            ViewBag.doctors = totaldoctors.Count();
            return RedirectToAction("ListClinics", new { id = id });
        }
        // ===================  Upload Images of Clinics ========================================================ss
       
        public ActionResult SuperAdminDashBoard()
        {
            var totalclinic = db.ClientTables.ToList();
            return View(totalclinic);
        }
        public ActionResult DetailOfClinic(string id,string clinicname)
        {
            ViewBag.ClinicName = clinicname;
            var id1 = Int32.Parse(id);
            var totaldoctors = db.DoctorProfiles.Where(y => y.ApplicationUser.ClientTableId == id1).ToList();
            ViewBag.doctors = totaldoctors.Count();
            var totalpatient = db.PatientProfiles.Where(y => y.ApplicationUser.ClientTableId == id1).ToList();
            ViewBag.patient = totalpatient.Count();
            var totalamount = db.PatientBookings.Where(y => y.PaymentStatus == "success" && y.PatientProfile.ApplicationUser.ClientTableId == id1).ToList();
            int sum = 0;
            foreach (var item in totalamount)
            {
                sum += Convert.ToInt32(item.AmountCharged);
            }
            ViewBag.amount = sum;
            var totalappointment = db.PatientBookings.Where(y => y.PatientProfile.ApplicationUser.ClientTableId == id1 && (y.Status == "Complete" || y.Status == "Missed")).ToList();
            ViewBag.appointment = totalappointment.Count();

            var Amount = db.ClientTables.Where(y => y.Id == id1).FirstOrDefault();
            ViewBag.TotalAmountPay = Amount.AmountPay;

            ViewBag.ClinicDetails = db.ClientTables.Where(y => y.Id == id1).ToList();
            ViewBag.ClinicImage = db.UploadImages.Where(y => y.ClientTableId== id1).ToList();
            return View();
        }

    }
}