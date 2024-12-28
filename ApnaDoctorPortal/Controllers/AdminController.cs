using ApnaDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Globalization;
using ApnaDoctor.Class;
using ApnaDoctorPortal.Models;
using System.IO;

namespace ApnaDoctor.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        // ===================  Admin Profile ========================================================ss
        public ActionResult CreateAdminProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAdminProfile(AdminProfile admin, FormCollection fc)
        {
            admin.ApplicationUserId = User.Identity.GetUserId();
            db.AdminProfiles.Add(admin);
            db.SaveChanges();


            return RedirectToAction("Index");


        }
        public ActionResult EditAdminProfile(string id)
        {
            var list = db.AdminProfiles.Where(y => y.ApplicationUserId == id).FirstOrDefault();
            return View(list);
        }
        [HttpPost]
        public ActionResult EditAdminProfile(AdminProfile admin, FormCollection fc)
        {
            db.Entry(admin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DetailAdminProfile()
        {
            var userid = User.Identity.GetUserId();
            var adminprofile = db.AdminProfiles.Where(x => x.ApplicationUserId == userid).FirstOrDefault();
           
            if (adminprofile == null)
            {
                return RedirectToAction("CreateAdminProfile", "Admin", new { id = userid });
            }
            else
            {
                return View(adminprofile);
            }

        }
        // ===================  Other Details ========================================================ss
        public ActionResult ListConsultation()
        {
            var list = db.Consultations.Include(y=>y.PatientProfile).Include(y=>y.DoctorProfile).Where(y=>y.DoctorProfile.ApplicationUser.ClientTableId==1).ToList().OrderByDescending(y=>y.ConsultationDate);
            return View(list);
        }
        public ActionResult AdminDashboard()
        {
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
            //var date = DateTime.Today;
            var list = db.PatientBookings.Where(y=>DbFunctions.TruncateTime(y.AppointmentDate)==date.Date && y.Status=="Active" && y.DoctorProfile.ApplicationUser.ClientTableId==1).Include(y=>y.DoctorProfile).Include(y=>y.PatientProfile).Include(y=>y.DoctorSlotSchedule).ToList();

            ViewBag.UpcomingAppointment = db.PatientBookings.Where(y=> DbFunctions.TruncateTime(y.AppointmentDate) > date.Date && y.Status == "Active" && y.DoctorProfile.ApplicationUser.ClientTableId == 1).Include(y => y.PatientProfile).Include(y => y.DoctorProfile).Include(y => y.DoctorSlotSchedule).ToList().OrderBy(y => y.AppointmentDate);
            var totalConsultation = db.PatientBookings.Where(y => DbFunctions.TruncateTime(y.AppointmentDate) <= date.Date && (y.Status == "Completed" || y.Status=="Missed") && y.DoctorProfile.ApplicationUser.ClientTableId == 1).Include(y => y.DoctorProfile).Include(y => y.PatientProfile).Include(y => y.DoctorSlotSchedule).ToList().OrderByDescending(y => y.AppointmentDate);
            ViewBag.totalConsultation = totalConsultation;


            var totaldoctors = db.DoctorProfiles.Where(y => y.ApplicationUser.ClientTableId == 1).ToList();
            ViewBag.doctors = totaldoctors.Count();
            var totalpatient = db.PatientProfiles.Where(y => y.ApplicationUser.ClientTableId == 1).ToList();
            ViewBag.patient = totalpatient.Count();
            var totalamount = db.PatientBookings.Where(y => y.PaymentStatus == "success" && y.PatientProfile.ApplicationUser.ClientTableId == 1).ToList();
            int sum = 0;
            foreach (var item in totalamount)
            {
                sum += Convert.ToInt32(item.AmountCharged);
            }
            ViewBag.amount = sum;
            var totalappointment = db.PatientBookings.Where(y => y.PatientProfile.ApplicationUser.ClientTableId == 1 && (y.Status == "Completed" || y.Status == "Missed")).ToList();
            ViewBag.appointment = totalappointment.Count();

            var Amount = db.ClientTables.Where(y => y.Id == 1).FirstOrDefault();
            ViewBag.TotalAmountPay = Amount.AmountPay;
            return View(list);
        }
        public ActionResult DetailOfConsultation(string id)
        {
            var id1 = Int32.Parse(id);
            var list = db.Consultations.Where(y=>y.Id==id1).Include(y=>y.PatientProfile).Include(y=>y.DoctorProfile).FirstOrDefault();
            return View(list);
        }
        
        public ActionResult ListDoctor()
        {
            var list = db.DoctorProfiles.Where(y=>y.ApplicationUser.ClientTableId==1).ToList();
            return View(list);
        }
        public ActionResult ListPatient()
        {
            var list = db.PatientProfiles.Where(y=>y.ApplicationUser.ClientTableId==1).ToList();
            return View(list);
        }
        public ActionResult ListAdmin()
        {
            var list = db.AdminProfiles.Where(y=>y.ApplicationUser.ClientTableId==1).ToList();
            return View(list);
        }
        // ===================  Doctor Profile ========================================================ss
        public ActionResult EditDoctorProfile(string id)
        {
            var list = db.DoctorProfiles.Where(x => x.ApplicationUserId == id).FirstOrDefault();
            var list2 = db.DoctorSpecialties.ToList();
            ViewBag.DoctorSpecialtyId = new SelectList(list2, "Id", "Description");
            return View(list);
        }
        [HttpPost]
        public ActionResult EditDoctorProfile(DoctorProfile doctor, FormCollection fc)
        {
            var list2 = db.DoctorSpecialties.ToList();
            ViewBag.DoctorSpecialtyId = new SelectList(list2, "Id", "Description");
            doctor.LastConsultationTime = Convert.ToDateTime("1900-01-01 00:00:00.000");
            db.Entry(doctor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListDoctor");
        }
        public ActionResult DetailDoctorProfile(string id)
        {
            var doctorprofile = db.DoctorProfiles.Where(x => x.ApplicationUserId == id).FirstOrDefault();
            return View(doctorprofile);
        }
        // =================== Add Doctor Schedule ========================================================ss
        public ActionResult AddDoctorSchedule(int? temp,string id)
        {

            if (temp != null)
            {
                ViewBag.msg = "Added SuccessFully";
                ViewBag.status = 1;
            }
            ViewBag.doctorid = id;
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);

            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int currentWeek = ciCurr.Calendar.GetWeekOfYear(datetime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            ViewBag.week = currentWeek;
            return View();
        }
        [HttpPost]
        public ActionResult AddDoctorSchedule(DoctorSchedule schedule, FormCollection fc, DoctorSlotSchedule slot)
        {
            string doctorid = fc["Id"].ToString();
            string DayFrom = fc["DayFrom"].ToString();
            string DayTo = fc["DayTo"].ToString();
            string StartTime = fc["StartTime"].ToString();
            string EndTime = fc["EndTime"].ToString();
            string SlotDuration = fc["SlotDuration"].ToString();
            string Week = fc["Week"].ToString();
            schedule.DoctorProfileId = doctorid;
            schedule.DayFrom = DayFrom;
            schedule.DayTo = DayTo;
            schedule.StartTime = StartTime;
            schedule.EndTime = EndTime;
            schedule.SlotDuration = SlotDuration;

            schedule.Week = Week;
            db.DoctorSchedules.Add(schedule);
            db.SaveChanges();
            var starttime = schedule.StartTime;
            var endTime = schedule.EndTime;
            int duration = Convert.ToInt32(schedule.SlotDuration);

            List<TimeList> timelist = new List<TimeList>();
            DateTime startdatetime = Convert.ToDateTime(starttime);
            DateTime enddatetime = Convert.ToDateTime(endTime);
            TimeSpan timeinterval = enddatetime.Subtract(startdatetime);
            int totalminute = Convert.ToInt32(timeinterval.TotalMinutes - duration);
            int noofslots = totalminute / duration;

            DateTime startAtMonday = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek);

            DateTime startofweek = Convert.ToDateTime(startAtMonday).Date;

            DateTime startOfWeek = DateTime.Today.AddDays(
      (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
      (int)DateTime.Today.DayOfWeek);


            if (DayFrom == schedule.DayFrom && DayTo == schedule.DayTo)
            {
                int startday = Int32.Parse(DayFrom);
                int endday = Int32.Parse(DayTo);
                string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

                for (int p = startday; p < endday + 1; p++)
                {
                    var starttime2 = schedule.StartTime;
                    DateTime startdatetime2 = Convert.ToDateTime(starttime2);
                    for (int i = 0; i < noofslots; i++)
                    {

                        if (i == 0)
                        {
                            var start = startdatetime.ToString("hh:mm tt");
                            slot.Days = days[p];
                            slot.Slot = start;
                            slot.DoctorScheduleId = schedule.Id;
                            slot.Status = "NotBooked";
                            slot.SlotDate = startofweek.AddDays(p);
                            db.DoctorSlotSchedules.Add(slot);
                            db.SaveChanges();
                        }

                        startdatetime2 = startdatetime2.AddMinutes(duration);
                        var strtime = startdatetime2.ToString("hh:mm tt");
                        slot.Slot = strtime;
                        db.DoctorSlotSchedules.Add(slot);
                        db.SaveChanges();

                    }
                }
            }
            return RedirectToAction("CheckDoctorSchedule", new { id=doctorid });
        }
        public ActionResult CheckDoctorSchedule(string id)
        {
            string[] days = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
          var schedule = db.DoctorSchedules.Where(x => x.DoctorProfileId == id ).Include(y => y.DoctorProfile).FirstOrDefault();
            if (schedule == null)
            {
                return RedirectToAction("AddDoctorSchedule", new { id = id });
            }
            
            var list = db.DoctorSchedules.Where(x => x.DoctorProfileId == id ).Include(y => y.DoctorProfile).ToList().OrderByDescending(y=>y.Week);
            List<DoctorScheduleContract> dsc = new List<DoctorScheduleContract>();
            foreach (var t in list)
            {
                dsc.Add(new DoctorScheduleContract
                {
                    FirstName=t.DoctorProfile.FirstName,
                    Id = t.Id,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Week = t.Week,
                    DayFrom = days[Convert.ToInt32(t.DayFrom)],
                    DayTo = days[Convert.ToInt32(t.DayTo)],
                    DoctorProfileId = t.DoctorProfileId,
                    SlotDuration = t.SlotDuration,
                });
            }
            ViewBag.schedule = dsc;
            return View();
        }


        // ===================  Book Appointment ========================================================ss
        public ActionResult ListDoctorForAppointment(int? temp)
        {
            if (temp != null)
            {

                ViewBag.Message = "Your Appointment Booked SuccessFully...";
            }
            var list = db.DoctorProfiles.Where(y=>y.Status=="Approved" && y.ApplicationUser.ClientTableId==1).Include(y => y.ApplicationUser).ToList();
            //ViewBag.DoctorAvailable = db.DoctorProfiles.Where(y => y.ApplicationUserId == "51951624-e145-4e77-8793-e0795692d795").ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult ListDoctorNew(string id)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int currentWeek = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var week = currentWeek.ToString();
            List<DoctorSlotsContract> dsc = new List<DoctorSlotsContract>();
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
            //var date = DateTime.Today;
            var timeString = DateTime.Now.ToString("hh:mm tt");
            var schedule = db.DoctorSlotSchedules.Include(i => i.DoctorSchedule.DoctorProfile.ApplicationUser).Where(r => r.DoctorSchedule.DoctorProfileId == id && r.DoctorSchedule.Week == currentWeek.ToString() && r.Status == "NotBooked" && DbFunctions.TruncateTime(r.SlotDate) >= DbFunctions.TruncateTime(date)).ToList().GroupBy(r => r.Days);
            foreach (var temp in schedule)
            {
                List<Class.Slots> sr = new List<Class.Slots>();
                foreach (var temp2 in temp.ToList())
                {
                    sr.Add(new Class.Slots
                    {
                        Id = temp2.Id,
                        Time = temp2.Slot
                    });
                }
                dsc.Add(new DoctorSlotsContract
                {
                    Day = temp.Key,
                    Slots = sr
                });
            }


            return Json(dsc, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BookSlot(FormCollection fc, PatientBookings slotbook)
        {
            var orderId = DateTime.Now.ToString("yyyyMMddHHmmss");
            string doctorid = fc["Doctorid"].ToString();
            string slotid = fc["slotid"].ToString();
            string familymembername = fc["familymemberid"].ToString();
            var familymemberid = Convert.ToInt32(familymembername);
            string patientid = fc["patientid"].ToString();
            int slotid1 = Int32.Parse(slotid);
            var item = db.DoctorSlotSchedules.Where(Y => Y.Id == slotid1).FirstOrDefault();

            var doctorprice = db.DoctorProfiles.Where(y => y.ApplicationUserId == doctorid).FirstOrDefault();
            string price = fc["Price"].ToString();
            if (price == "PhysicalAppointmentPrice")
            {
                slotbook.AmountCharged = doctorprice.PhysicalAppointmentPrice;
            }
            else if (price == "VirtualAppointmentPrice")
            {
                slotbook.AmountCharged = doctorprice.VirtualAppointmentPrice;
            }
            else if (price == "VideoAppointmentPrice")
            {
                slotbook.AmountCharged = doctorprice.VideoAppointmentPrice;
            }

            //item.Status = "Booked";
            slotbook.AppointmentDate = item.SlotDate;


            slotbook.BookingDate = DateTime.Now;
            slotbook.DoctorProfileId = doctorid;
            slotbook.DoctorSlotScheduleId = Int32.Parse(slotid);
            slotbook.PatientProfileId = patientid;
            slotbook.Status = "Pending";
            slotbook.PaymentStatus = "Pending";
            slotbook.OrderId = orderId;
            slotbook.FamilyMemberId = familymemberid;
            db.PatientBookings.Add(slotbook);
            db.SaveChanges();
            return Redirect("https://portal.webdoc.com.pk/transection/A/cc.php?orderid=" + orderId + "&amount=" + slotbook.AmountCharged);

            //return RedirectToAction("ListDoctorForAppointment", new { temp = 1 });
        }


        // ===================  Consultation and prescribe Controllers ========================================================ss

        public ActionResult NewConsultation(string id,string doctorid,string slotid)
        {

            var viewModel = new NewConsultationViewModel
            {
                TestNames = db.TestNames.ToList(),
                PatientId = id,
                DoctorId=doctorid,
            };
            ViewBag.History = db.PatientHistories.FirstOrDefault(t => t.PatientProfileId == id);
            ViewBag.SlotId = slotid;
            ViewBag.MedicineName = db.MedicineNames.ToList();
            return View("ConsultationForm", viewModel);
        }
        [HttpPost]
        public ActionResult SaveConsultation(Consultation consultation, string patientId, string doctorid, FormCollection fc,string slotid)
        {
            var slotid1 = Int32.Parse(slotid);
            var item = db.PatientBookings.Where(y => y.DoctorSlotScheduleId == slotid1).FirstOrDefault();
            item.Status = "Completed";
            List<string> mv;
            List<string> medicineName;
            List<string> days;
            List<string> morning;
            List<string> afternoon;
            List<string> night;
            List<string> addtionalnotes;
            //if (!fc.AllKeys.Contains("Test1"))
            //{
            //    return RedirectToAction("NewConsultation", "Doctor", new { id = patientId });
            //}
            if (!fc.AllKeys.Contains("medicinevalue[]"))
            {
                mv = new List<string>();
                medicineName = new List<string>();
                days = new List<string>();
                morning = new List<string>();
                afternoon = new List<string>();
                night = new List<string>();
                addtionalnotes = new List<string>();
            }
            else
            {
                mv = (fc["medicinevalue[]"].ToString()).Split(',').ToList();
                medicineName = (fc["medicineName[]"].ToString()).Split(',').ToList();
                days = (fc["days[]"].ToString()).Split(',').ToList();
                morning = (fc["morning[]"].ToString()).Split(',').ToList();
                afternoon = (fc["afternoon[]"].ToString()).Split(',').ToList();
                night = (fc["night[]"].ToString()).Split(',').ToList();
                addtionalnotes = (fc["addtionalnotes[]"].ToString()).Split(',').ToList();
            }
            var testlist = db.TestNames.ToList();
            string testnames = "";

            string test = fc["Test1"].ToString();
            if (test == "")
            {
                consultation.Tests = "-";
            }
            else
            {
                string[] testIds = test.Split(',');
                foreach (var id in testIds)
                {
                    int f = Convert.ToInt32(id);
                    testnames += testlist.Where(y => y.Id == f).Select(t => t.Name).FirstOrDefault() + ",";
                }
                consultation.Tests = testnames;
            }


            //string PhysicalAppointment = "";

            // PhysicalAppointment = fc["PhysicalAppointment"].ToString();
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime PakistanTIme = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);


            consultation.ConsultationCharges = 0;
            consultation.DoctorProfileId = doctorid;
            consultation.ConsultationDate = PakistanTIme;
            consultation.Prescription = "-";
            consultation.PatientProfileId = patientId;
            consultation.ConsultationType = "Phone";


            db.Consultations.Add(consultation);
            string docid =doctorid;
            db.SaveChanges();
            int consultId = db.Consultations.Max(y => y.Id);
            string medicine = "";
            string medicinedata = "";
            List<PrescribeDetials> pd1 = new List<PrescribeDetials>();
            for (int index = 0; index < mv.Count; index++)
            {
                PrescribeDetials pd = new PrescribeDetials();
                pd.AdditionalNotes = addtionalnotes[index];

                pd.MedicineNameId = Convert.ToInt32(mv[index]);
                pd.Days = days[index];
                if (afternoon[index] != "")
                {
                    pd.Day = afternoon[index];
                }
                else
                {
                    pd.Day = "0";
                }
                if (morning[index] != "")
                {
                    pd.Morning = morning[index];
                }
                else
                {
                    pd.Morning = "0";
                }
                if (night[index] != "")
                {
                    pd.Night = night[index];
                }
                else
                {
                    pd.Night = "0";
                }
                pd.ConsultationId = consultId;
                pd.status = "Pending";
                db.PrescribeDetials.Add(pd);
                pd1.Add(pd);
                db.SaveChanges();
                medicine += medicineName[index] + addtionalnotes[index];
            }

            return RedirectToAction("UpcomingAppointments");

        }
        // ===================  Patient History ========================================================ss
        public ActionResult Addhistory(string patientId, string doctorid)
        {
            ViewBag.patientId = patientId;
            ViewBag.doctorId = doctorid;
            return View();

        }

        [HttpPost]
        public ActionResult Addhistory(PatientHistory ph, FormCollection fc, string doctorid)
        {
            ph.DoctorProfileId = fc["DoctorProfileId"].ToString();
            db.PatientHistories.Add(ph);
            db.SaveChanges();
            ph.PatientProfileId = fc["PatientProfileId"].ToString();


            return RedirectToAction("NewConsultation", new { id = ph.PatientProfileId });

        }
        public ActionResult Edithistory(string patientId, string id,string doctorid)
        {
            var temp = db.PatientHistories.Find(Convert.ToInt32(id));
            ViewBag.patientId = patientId;
            ViewBag.doctorId = doctorid;
            return View(temp);
        }

        [HttpPost]
        public ActionResult Edithistory(PatientHistory ph, FormCollection fc, string doctorid)
        {
            ph.DoctorProfileId = fc["DoctorProfileId"].ToString();

            db.Entry(ph).State = EntityState.Modified;
            db.SaveChanges();
            ph.PatientProfileId = fc["PatientProfileId"].ToString();

            return RedirectToAction("NewConsultation", new { id = ph.PatientProfileId });

        }

        // ===================  Patient PreviousDisease ========================================================ss
        public ActionResult PatientPreviousDisease(string id)
        {
            var list = db.PatientPreviousDiseases.Where(y => y.PatientProfileId == id).Include(y => y.Disease).Include(y => y.PatientProfile).ToList();
            return View(list);
        }
        // ===================  Cancel Appointment ========================================================ss
        public ActionResult CancelPatientAppointment(string slotid,string bookid)
        {
            var bookingid1 = Int32.Parse(bookid);
            var bookingid = db.PatientBookings.Where(y => y.Id == bookingid1).FirstOrDefault();
            bookingid.Status = "Cancelled";
            var slotscheduleid1 = Int32.Parse(slotid);
            var slotScheduleId = db.DoctorSlotSchedules.Where(y => y.Id == slotscheduleid1).FirstOrDefault();
            slotScheduleId.Status = "NotBooked";
            db.SaveChanges();
           return RedirectToAction("UpcomingAppointments");
        }

        public ActionResult DoctorListForAppointment(int? temp,string id,string doctorid)
        {
            var familymember = db.FamilyMembers.Where(y=>y.PatientProfileId==id).ToList();
            ViewBag.Id = new SelectList(familymember, "Id", "Name");
            if (doctorid!=null)
            {
                ViewBag.doctorid = doctorid;
                var doctordata = db.DoctorProfiles.Where(y => y.ApplicationUserId == doctorid).FirstOrDefault();
                ViewBag.name = doctordata.FirstName;
                ViewBag.Virtual = doctordata.VirtualAppointmentPrice;
                ViewBag.physical = doctordata.PhysicalAppointmentPrice;
                ViewBag.Video = doctordata.VideoAppointmentPrice;
                ViewBag.VirtualAvailable = doctordata.VirtualAvailable;
                ViewBag.PhysicalAvailable = doctordata.PhysicalAvailable;
                ViewBag.InstantVideoavilable = doctordata.InstantVideoAvailable;
            }
            if (temp != null)
            {

                ViewBag.Message = "Your Appointment Booked SuccessFully...";
            }
            ViewBag.PatientId = id;
            var list = db.DoctorProfiles.Where(y => y.Status == "Approved" && y.ApplicationUser.ClientTableId == 1).Include(y => y.ApplicationUser).ToList();
            //ViewBag.DoctorAvailable = db.DoctorProfiles.Where(y => y.ApplicationUserId == "51951624-e145-4e77-8793-e0795692d795").ToList();
            return View(list);
        }
       
        public ActionResult DoctorListNew(string doctorid,string patientid)
        {


            return RedirectToAction("DoctorListForAppointment",new { doctorid=doctorid,id=patientid});
        }
        public ActionResult DoctorSlotJson(string id, string dateData)
        {
            List<DoctorSlotsContract> dsc = new List<DoctorSlotsContract>();
            DateTime date = Convert.ToDateTime(dateData);
            var schedule = db.DoctorSlotSchedules.Include(i => i.DoctorSchedule.DoctorProfile.ApplicationUser).Where(r => r.DoctorSchedule.DoctorProfileId == id && r.Status == "NotBooked" && DbFunctions.TruncateTime(r.SlotDate) == date).ToList().GroupBy(r => r.Days);
            foreach (var temp in schedule)
            {
                List<Class.Slots> sr = new List<Class.Slots>();
                foreach (var temp2 in temp.ToList())
                {
                    sr.Add(new Class.Slots
                    {
                        Id = temp2.Id,
                        Time = temp2.Slot
                    });
                }
                dsc.Add(new DoctorSlotsContract
                {
                    Day = temp.Key,
                    Slots = sr
                });
            }


            return Json(dsc, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddInsuranceCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddInsuranceCompany(InsuranceCompany insurance, HttpPostedFileBase file)
        {
            string makeDirectory = Path.Combine(Server.MapPath("~/InsuranceCompany/" + insurance.Name));
            if (!Directory.Exists(makeDirectory))
            {
                Directory.CreateDirectory(makeDirectory);
            }
            string pathofimage = Path.Combine(Server.MapPath("~/InsuranceCompany/" + insurance.Name), file.FileName);
            file.SaveAs(pathofimage);
            insurance.PicURL = "https://apnadoctor.webddocsystems.com/InsuranceCompany/" + insurance.Name + "/" + file.FileName;
            db.InsuranceCompanies.Add(insurance);
            db.SaveChanges();


            return RedirectToAction("Index");


        }
    }
}