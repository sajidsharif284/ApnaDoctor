using ApnaDoctor.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ApnaDoctor.Class;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;


namespace ApnaDoctor.Controllers
{
    public class DoctorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        // ===================  Doctor Profile ========================================================ss
        public ActionResult CreateDoctorProfile()
        {
            var list = db.DoctorSpecialties.ToList();
            ViewBag.DoctorSpecialtyId = new SelectList(list,"Id", "Description");
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoctorProfile( DoctorProfile doctor,FormCollection fc)
        {
            
            var list = db.DoctorSpecialties.ToList();
            ViewBag.DoctorSpecialtyId = new SelectList(list, "Id", "Description");
            doctor.ApplicationUserId = User.Identity.GetUserId();
            doctor.LastConsultationTime = Convert.ToDateTime("1900-01-01 00:00:00.000");
            doctor.Status = "Approved";
            db.DoctorProfiles.Add(doctor);
            db.SaveChanges();
           
            
            return RedirectToAction("Index");
           
            
        }
        public ActionResult EditDoctorProfile(string id)
        {
            var list = db.DoctorProfiles.Where(x => x.ApplicationUserId == id).FirstOrDefault();
            var list2 = db.DoctorSpecialties.ToList();
            ViewBag.DoctorSpecialtyId = new SelectList(list2, "Id", "Description");
            ViewBag.doctorid = id;
            return View(list);
        }
        [HttpPost]
        public ActionResult EditDoctorProfile(DoctorProfile doctor,FormCollection fc)
        {
            var doctorid = fc["Id"];
            var list2 = db.DoctorSpecialties.ToList();
            ViewBag.DoctorSpecialtyId = new SelectList(list2, "Id", "Description");
            var list = db.DoctorProfiles.Where(x => x.ApplicationUserId == doctorid).FirstOrDefault();
            list.FirstName = doctor.FirstName;
            list.LastName = doctor.LastName;
            list.CNIC = doctor.CNIC;
            list.DateOfBirth = doctor.DateOfBirth;
            list.Gender = doctor.Gender;
            list.Address = doctor.Address;
            list.Country = doctor.Country;
            list.City = doctor.City;
            list.MobileNumber = doctor.MobileNumber;
            list.DoctorSpecialtyId = doctor.DoctorSpecialtyId;
            list.PhysicalAppointmentPrice = doctor.PhysicalAppointmentPrice;
            list.VideoAppointmentPrice = doctor.VideoAppointmentPrice;
            list.VirtualAppointmentPrice = doctor.VirtualAppointmentPrice;
            list.VirtualAvailable = doctor.VirtualAvailable;
            list.PhysicalAvailable = doctor.PhysicalAvailable;
            list.InstantVideoAvailable = doctor.InstantVideoAvailable;
            //list.LastConsultationTime = Convert.ToDateTime("1900-01-01 00:00:00.000");
            list.Status = "Approved";
            list.DoctorCategoryId = 0;
            list.OnlineDoctor = 0;
            db.SaveChanges();
            return RedirectToAction("DetailDoctorProfile");
        }
        public ActionResult DetailDoctorProfile()
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            var userid = User.Identity.GetUserId();
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var doctorprofile = db.DoctorProfiles.Where(x => x.ApplicationUserId == userid).FirstOrDefault();
            ViewBag.qualification = db.DoctorQualifications.Where(x => x.DoctorProfileId == userid).ToList();
            var doctorschedule = db.DoctorSchedules.Where(x => x.DoctorProfileId == userid && x.Week==weekNum.ToString()).ToList();
            List<DoctorScheduleContract> dsc = new List<DoctorScheduleContract>();
            foreach (var t in doctorschedule)
            {
                dsc.Add(new DoctorScheduleContract
                {
                    FirstName = t.DoctorProfile.FirstName,
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
            var date = DateTime.Today;
            if (doctorprofile==null)
            {
                return RedirectToAction("CreateDoctorProfile","Doctor",new { id=userid});
            }
            else
            {
                return View(doctorprofile);
            }
           
        }
        // ===================  Doctor Qualification ========================================================ss
        public ActionResult CreateDoctorQualification()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.DoctorProfileId = userid;
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoctorQualification(FormCollection fc)
        {
            DoctorQualification qualification=new DoctorQualification();
            var doctorprofileid = fc["Id"].ToString();
            string DegreeName = fc["DegreeName"].ToString();
            string YearTo = fc["YearTo"].ToString();
            string YearFrom = fc["YearFrom"].ToString();
            string Institution = fc["Institution"].ToString();
            qualification.DoctorProfileId = doctorprofileid;
            qualification.DegreeName = DegreeName;
            qualification.YearTo = YearTo;
            qualification.YearFrom = YearFrom;
            qualification.Institution = Institution;
            db.DoctorQualifications.Add(qualification);
            db.SaveChanges();
            return RedirectToAction("ListDoctorQualification");
        }
        public ActionResult ListDoctorQualification()
        {
            var userid = User.Identity.GetUserId();
            var list = db.DoctorQualifications.Where(x=>x.DoctorProfileId==userid).ToList();
            var doctorprofile = db.DoctorProfiles.Where(x => x.ApplicationUserId == userid).FirstOrDefault();
            var doctorqualification = db.DoctorQualifications.Where(x => x.DoctorProfileId == userid).FirstOrDefault();
            if (doctorprofile == null)
            {
                return RedirectToAction("CreateDoctorProfile", "Doctor");
            }
            else if (doctorqualification == null)
            {
                return RedirectToAction("CreateDoctorQualification", "Doctor");
            }
            return View(list);
        }
        public ActionResult EditDoctorQualification(string id)
        {
            var id1 = Int32.Parse(id);
            var list = db.DoctorQualifications.Where(x => x.Id == id1).FirstOrDefault();
            ViewBag.DoctorQualificationId = id;
            return View(list);
        }
        [HttpPost]
        public ActionResult EditDoctorQualification(DoctorQualification qualification,FormCollection fc)
        {
            
            var qualificationid = fc["Id"];
            var id1 = Int32.Parse(qualificationid);
            var list = db.DoctorQualifications.Where(x => x.Id == id1).FirstOrDefault();
            list.DegreeName = qualification.DegreeName;
            list.YearTo = qualification.YearTo;
            list.YearFrom = qualification.YearFrom;
            list.Institution = qualification.Institution;
            db.SaveChanges();
            return RedirectToAction("ListDoctorQualification");
        }
        public ActionResult DeleteDoctorQualification(string id)
        {
            var id1 = Int32.Parse(id);
            var user = db.DoctorQualifications.Where(y => y.Id == id1).FirstOrDefault();
            db.DoctorQualifications.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ListDoctorQualification");
        }
        // ===================  Add Doctor Schedule ========================================================ss
        public ActionResult DoctorSchedule(int? temp)
        {
            var id = User.Identity.GetUserId();
            var doctorprofile = db.DoctorProfiles.Where(y => y.ApplicationUserId == id).FirstOrDefault();
            if (doctorprofile == null)
            {
                return RedirectToAction("CreateDoctorProfile", "Doctor");
            }
            if (temp!=null)
            {
                ViewBag.msg = "Added SuccessFully";
                ViewBag.status=1;
            }
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);

            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int currentWeek = ciCurr.Calendar.GetWeekOfYear(datetime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            ViewBag.week = currentWeek;

            var currentweek2 = Convert.ToString(currentWeek - 1);
            
            ViewBag.list = db.DoctorSchedules.Where(x => x.DoctorProfileId == id && x.Week == currentweek2).Include(y => y.DoctorProfile).ToList();
            var id1 = db.DoctorSchedules.Where(x => x.DoctorProfileId == id && x.Week == currentweek2).Include(y => y.DoctorProfile).FirstOrDefault();
            if(id1!=null)
            { ViewBag.list2 = id1.Id; }
            
            return View();
        }
        [HttpPost]
        public ActionResult DoctorSchedule(DoctorSchedule schedule,FormCollection fc,DoctorSlotSchedule slot)
        {
            int Year = 2021;
            int Weeks = Convert.ToInt32(schedule.Week);
            int DayOfWeek = 1;

            DateTime FecIni = new DateTime(Year, 1, 1);
            FecIni = FecIni.AddDays(7 * (Weeks));
            if ((int)FecIni.DayOfWeek > DayOfWeek)
            {
                while ((int)FecIni.DayOfWeek != DayOfWeek) FecIni = FecIni.AddDays(-1);
            }
            else
            {
                while ((int)FecIni.DayOfWeek != DayOfWeek) FecIni = FecIni.AddDays(1);
            }
            var doctorid = User.Identity.GetUserId();

            string DayFrom = fc["DayFrom"].ToString();
            string DayTo = fc["DayTo"].ToString();
            string StartTime = fc["StartTime"].ToString();
            string EndTime = fc["EndTime"].ToString();
            string SlotDuration = fc["SlotDuration"].ToString();
            string Week = fc["Week"].ToString();

            schedule.DoctorProfileId=doctorid;
            schedule.DayFrom = DayFrom;
            schedule.DayTo = DayTo;
            schedule.StartTime = StartTime;
            schedule.EndTime = EndTime;
            schedule.SlotDuration = SlotDuration;
           
            schedule.Week =Week ;
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

            //DateTime startAtMonday = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek);

            //DateTime startofweek = Convert.ToDateTime(startAtMonday).Date;

            //DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);


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
                            slot.SlotDate = FecIni.AddDays(p);
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
            return RedirectToAction("DoctorSchedule",new { temp=1});
        }
       
        public ActionResult DoctorDashboard()
        {
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
            var id = User.Identity.GetUserId();
            var doctorprofile = db.DoctorProfiles.Where(y => y.ApplicationUserId == id).FirstOrDefault();
            if (doctorprofile == null)
            {
                return RedirectToAction("CreateDoctorProfile", "Doctor");
            }
            //var date = DateTime.Today;
            var list = db.PatientBookings.Where(x=>DbFunctions.TruncateTime(x.AppointmentDate) == date.Date && x.Status == "Active"&& x.DoctorProfileId==id).Include(y => y.DoctorProfile).Include(y=>y.PatientProfile).Include(y=>y.DoctorSlotSchedule).ToList();
            // ===================  Today Appointments ========================================================ss
            var totalAppointmentToday = db.PatientBookings.Where(y => y.DoctorProfileId == id && DbFunctions.TruncateTime(y.AppointmentDate)== date.Date &&( y.Status=="Active" || y.Status=="Completed")).ToList();
            ViewBag.totalAppointmentToday1 = totalAppointmentToday.Count();
            var ConsultationToday = db.PatientBookings.Where(y => y.DoctorProfileId == id && DbFunctions.TruncateTime(y.AppointmentDate) == date.Date && y.Status=="Completed").ToList();
            ViewBag.ConsultationToday1 = ConsultationToday.Count();
            // ===================  doctor price ========================================================ss
            ViewBag.detailDocor = db.DoctorProfiles.Where(y => y.ApplicationUserId == id).ToList();
            var totalConsultation = db.PatientBookings.Where(y => y.DoctorProfileId == id && DbFunctions.TruncateTime(y.AppointmentDate) <= date.Date && y.Status == "Completed").Include(y=>y.DoctorProfile).Include(y=>y.PatientProfile).Include(y=>y.DoctorSlotSchedule).ToList().OrderByDescending(y=>y.AppointmentDate);
            ViewBag.totalConsultation = totalConsultation;
            ViewBag.totalConsultation1 = totalConsultation.Count();

            ViewBag.UpcomingAppointment = db.PatientBookings.Where(y => y.DoctorProfileId == id && DbFunctions.TruncateTime(y.AppointmentDate) > date.Date && y.Status=="Active").Include(y=>y.PatientProfile).Include(y => y.DoctorProfile).Include(y => y.DoctorSlotSchedule).ToList().OrderBy(y=>y.AppointmentDate);
            ViewBag.AppointmentHistory = db.PatientBookings.Where(y => y.DoctorProfileId == id && DbFunctions.TruncateTime(y.AppointmentDate) <= date.Date && (y.Status == "Completed" || y.Status=="Missed")).Include(y => y.PatientProfile).Include(y => y.DoctorProfile).Include(y => y.DoctorSlotSchedule).ToList().OrderByDescending(y => y.AppointmentDate);

            return View(list);
        }
        // ===================  Consultation and prescribe Controllers ========================================================ss
        public ActionResult NewConsultation(string id,string slotid)
        {
            
            var viewModel = new NewConsultationViewModel
            {
                TestNames = db.TestNames.ToList(),
                PatientId = id,
            };
            ViewBag.History =db.PatientHistories.FirstOrDefault(t => t.PatientProfileId == id);
            ViewBag.SlotId = slotid;
            ViewBag.MedicineName = db.MedicineNames.ToList();
            return View("ConsultationForm", viewModel);
        }
        [HttpPost]
        public ActionResult SaveConsultation(Consultation consultation, string patientId, FormCollection fc,string slotid)
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
            consultation.DoctorProfileId = User.Identity.GetUserId();
            consultation.ConsultationDate = PakistanTIme;
            consultation.Prescription = "-";
            consultation.PatientProfileId = patientId;
            consultation.ConsultationType = "Phone";


            db.Consultations.Add(consultation);
            string docid = User.Identity.GetUserId();
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

            return RedirectToAction("UpcomingDoctorAppointments");

        }
        // ===================  Add Patient History ========================================================ss
        public ActionResult Addhistory(string patientId)
        {
            ViewBag.patientId = patientId;
           
            return View();

        }

        [HttpPost]
        public ActionResult Addhistory(PatientHistory ph, FormCollection fc)
        {
            ph.DoctorProfileId = User.Identity.GetUserId();
            db.PatientHistories.Add(ph);
            db.SaveChanges();
            ph.PatientProfileId = fc["PatientProfileId"].ToString();
            

            return RedirectToAction("NewConsultation", new { id = ph.PatientProfileId });

        }
        public ActionResult Edithistory(string patientId, string id)
        {
            var temp = db.PatientHistories.Find(Convert.ToInt32(id));
            ViewBag.patientId = patientId;
            return View(temp);
        }

        [HttpPost]
        public ActionResult Edithistory(PatientHistory ph, FormCollection fc)
        {
            ph.DoctorProfileId = User.Identity.GetUserId();

            db.Entry(ph).State = EntityState.Modified;
            db.SaveChanges();
            ph.PatientProfileId = fc["PatientProfileId"].ToString();
           
            return RedirectToAction("NewConsultation", new { id = ph.PatientProfileId }); 

        }
        // ===================  Patient Disease ========================================================ss
        public ActionResult PatientPreviousDisease(string id)
        {
            var list = db.PatientPreviousDiseases.Where(y=>y.PatientProfileId==id).Include(y=>y.Disease).Include(y=>y.PatientProfile).ToList();
            return View(list);
        }
        public ActionResult CheckDoctorSchedule()
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            var id = User.Identity.GetUserId();
            var list = db.DoctorSchedules.Where(x => x.DoctorProfileId == id).Include(y => y.DoctorProfile).ToList().OrderByDescending(y => y.Week);
            List<DoctorScheduleContract> dsc = new List<DoctorScheduleContract>();
            foreach (var t in list)
            {
                dsc.Add(new DoctorScheduleContract
                {
                    FirstName = t.DoctorProfile.FirstName,
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
        // ===================  Cancel Appointments ========================================================ss
        public ActionResult CancelPatientAppointment(string slotid,string bookid)
        {
            var bookingid1 = Int32.Parse(bookid);
            var bookingid = db.PatientBookings.Where(y => y.Id == bookingid1).FirstOrDefault();
            bookingid.Status = "Cancelled";
            var slotscheduleid1 = Int32.Parse(slotid);
            var doctorSlotSchedule = db.DoctorSlotSchedules.Where(y => y.Id == slotscheduleid1).FirstOrDefault();
            doctorSlotSchedule.Status = "NotBooked";
            db.SaveChanges();
            return RedirectToAction("UpcomingDoctorAppointments");
        }
        // ===================  Add Medicines ========================================================ss
        //public ActionResult AddMedicine()
        //{
        //    ViewBag.MedicineName = db.MedicineNames.ToList();
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddMedicine(MedicineNames medi)
        //{

        //    medi.Formula = "-";
        //    db.MedicineNames.Add(medi);
        //    db.SaveChanges();
        //    return RedirectToAction("ViewMedicine", "Doctor");
        //}
    }
}