using ApnaDoctor.Class;
using ApnaDoctor.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Security.Cryptography;


namespace ApnaDoctor.Controllers
{
    public class PatientController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Patient
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        // ===================  Patient Profile ========================================================ss
        public ActionResult CreatePatientProfile(string id)
        {
            ViewBag.ApplicationUserId = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreatePatientProfile(FormCollection fc)
        {
            Models.FamilyMember member = new Models.FamilyMember();
            Models.PatientProfile patient = new Models.PatientProfile();
            var id = fc["Id"].ToString();
            string FirstName = fc["FirstName"].ToString();
            string LastName = fc["LastName"].ToString();
            string DateOfBirth = fc["DateOfBirth"].ToString();
            string Address = fc["Address"].ToString();
            string Country = fc["Country"].ToString();
            string City = fc["City"].ToString();
            string CNIC = fc["CNIC"].ToString();
            string MobileNo = fc["MobileNo"].ToString();
            string MartialStatus = fc["MartialStatus"].ToString();
            string Age = fc["Age"].ToString();
            string Gender = fc["Gender"].ToString();
            string Weight = fc["Weight"].ToString();
            string Height = fc["Height"].ToString();
            patient.ApplicationUserId = id;
            patient.FirstName = FirstName;
            patient.LastName = LastName;
            patient.DateOfBirth = Convert.ToDateTime(DateOfBirth);
            patient.Address = Address;
            patient.Country = Country;
            patient.CNIC = CNIC;
            patient.MobileNo = MobileNo;
            patient.City = City;
            patient.MartialStatus = MartialStatus;
            patient.Age = Age;
            patient.Gender = Gender;
            patient.Weight = Weight;
            patient.Height = Height;
               
                db.PatientProfiles.Add(patient);
                db.SaveChanges();
            member.Name = patient.FirstName;
            member.Relation = "Self";
            member.PatientProfileId = patient.ApplicationUserId;
            db.FamilyMembers.Add(member);
            db.SaveChanges();

                return RedirectToAction("DetailPatientProfile");
        }
        public ActionResult DetailPatientProfile()
        {
            var userid = User.Identity.GetUserId();
            var patientprofile = db.PatientProfiles.Where(x => x.ApplicationUserId == userid).FirstOrDefault();
            ViewBag.disease = db.PatientPreviousDiseases.Where(x => x.PatientProfileId == userid).Include(y=>y.Disease).ToList();

            if (patientprofile==null)
            {
                return RedirectToAction("CreatePatientProfile",new { id=userid});
            }
            else
            { 
            return View(patientprofile);
            }
        }
        public ActionResult EditPatientProfile(string id)
        {
            var list = db.PatientProfiles.Where(x => x.ApplicationUserId == id).FirstOrDefault();
            var date = list.DateOfBirth.ToString();
            string[] arrayValues = date.Split(' ');
            ViewBag.date1 = arrayValues[0].ToString();
            ViewBag.ApplicationUserId = id;
            
            return View(list);
        }
        [HttpPost]
        public ActionResult EditPatientProfile(FormCollection fc, Models.PatientProfile patient)
        {
            var patientid = fc["Id"];
            var list = db.PatientProfiles.Where(x => x.ApplicationUserId == patientid).FirstOrDefault();
            list.FirstName = patient.FirstName;
            list.LastName = patient.LastName;
            list.DateOfBirth = patient.DateOfBirth;
            list.Address = patient.Address;
            list.Country = patient.Country;
            list.CNIC = patient.CNIC;
            list.MobileNo = patient.MobileNo;
            list.City = patient.City;
            list.MartialStatus = patient.MartialStatus;
            list.Age = patient.Age;
            list.Gender = patient.Gender;
            list.Weight = patient.Weight;
            list.Height = patient.Height;
            db.SaveChanges();
            return RedirectToAction("DetailPatientProfile");
        }
        // ===================  Patient Disease ========================================================ss
        public ActionResult CreatePatientPreviousDisease()
        {
            var list = db.Diseases.ToList();
            ViewBag.DiseaseId = new SelectList(list, "Id","Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreatePatientPreviousDisease(PatientPreviousDisease disease)
        {
            var list = db.Diseases.ToList();
            ViewBag.DiseaseId = new SelectList(list, "Id", "Name");
            disease.PatientProfileId = User.Identity.GetUserId();
            db.PatientPreviousDiseases.Add(disease);
            db.SaveChanges();
            return RedirectToAction("ListPreviousDisease");
        }
        public ActionResult ListPreviousDisease()
        {

            var userid = User.Identity.GetUserId();
            
            var list = db.PatientPreviousDiseases.Where(y=>y.PatientProfileId==userid).Include(y=>y.Disease).ToList();
            var patientprofile = db.PatientProfiles.Where(x => x.ApplicationUserId == userid).FirstOrDefault();
            var Patientdisease = db.PatientPreviousDiseases.Where(y=>y.PatientProfileId==userid).FirstOrDefault();
            if (patientprofile == null)
            {
                return RedirectToAction("CreatePatientProfile", "Patient");
            }
            else if (Patientdisease == null)
            {
                return RedirectToAction("CreatePatientPreviousDisease", "Patient");
            }
            return View(list);
        }
        public ActionResult EditPatientDisease(string id)
        {
            var id1 = Int32.Parse(id);
            var list2 = db.PatientPreviousDiseases.Where(x => x.Id == id1).FirstOrDefault();
            var list = db.Diseases.ToList();
            ViewBag.DiseaseId = new SelectList(list,"Id","Name");
            ViewBag.id = id;
            return View(list2);
        }
        [HttpPost]
        public ActionResult EditPatientDisease(PatientPreviousDisease disease, FormCollection fc)
        {
            var diseaseid = fc["Id"];
            var id1 = Int32.Parse(diseaseid);
            var list = db.Diseases.ToList();
            ViewBag.DiseaseId = new SelectList(list, "Id", "Name");
            var list2 = db.PatientPreviousDiseases.Where(x => x.Id == id1).FirstOrDefault();
            list2.DiseaseId = disease.DiseaseId;
            db.SaveChanges();
            return RedirectToAction("ListPreviousDisease");
        }
        public ActionResult DeletePatientDisease(string id)
        {
            var id1 = Int32.Parse(id);
            var user = db.PatientPreviousDiseases.Where(y => y.Id == id1).FirstOrDefault();
            db.PatientPreviousDiseases.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ListPreviousDisease");
        }
        // ===================  Book Appointment ========================================================ss
        public ActionResult ListDoctor(int? temp)
        {
            if(temp!=null)
            {

                ViewBag.Message = "Your Appointment Booked SuccessFully...";
            }
            var userid = User.Identity.GetUserId();
            var patientprofile = db.PatientProfiles.Where(y => y.ApplicationUserId == userid).FirstOrDefault();
            if (patientprofile == null)
            {
                return RedirectToAction("CreatePatientProfile", new { id = userid });
            }
            //ViewBag.DoctorAvailable = db.DoctorProfiles.Where(y => y.ApplicationUserId == userid).ToList();
            var list = db.DoctorProfiles.Where(y=>y.Status=="Approved" && y.ApplicationUser.ClientTableId==1).Include(y=>y.ApplicationUser).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult ListDoctorNew(string id)
        {
            
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int currentWeek = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var week = currentWeek.ToString();
            List<DoctorSlotsContract> dsc = new List<DoctorSlotsContract>();
            //var date = DateTime.Today;
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);

            var timeString = DateTime.Now.ToString("hh:mm tt");
            //var b = Convert.ToInt32(timeString);
            var schedule = db.DoctorSlotSchedules.Include(i => i.DoctorSchedule.DoctorProfile.ApplicationUser).Where(r => r.DoctorSchedule.DoctorProfileId == id && r.DoctorSchedule.Week == currentWeek.ToString() && r.Status == "NotBooked"&& DbFunctions.TruncateTime(r.SlotDate) >= date.Date ).ToList().GroupBy(r => r.Days);
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

        public string URLCalling()
        {
            
            HttpClient client1 = new HttpClient();
            string url = "https://webdocapiservice.webddocsystems.com/iOSApp.svc/GetTplRecord";
            client1.BaseAddress = new Uri("https://webdocapiservice.webddocsystems.com/iOSApp.svc/GetTplRecord");
            //string json = JsonConvert.SerializeObject(parms);
            client1.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");
            System.Net.Http.HttpResponseMessage response = client1.PostAsync(url, new StringContent("", null, "application/json")).Result;
            var FromURL = response.Content.ReadAsStringAsync().Result;
            List<GetTplRecordResult> dd = new List<GetTplRecordResult>();//convert response to model
            JavaScriptSerializer j = new JavaScriptSerializer();
            GetTplRecordResultMapping result = j.Deserialize<GetTplRecordResultMapping>(FromURL);

            return FromURL;
        }
        [HttpPost]
        public ActionResult BookSlot(FormCollection fc,PatientBookings slotbook)
        {
            //var orderId = DateTime.Now.ToString("yyyyMM");
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string orderId = x.ToString("000000");

            string familymembername = fc["FamilyMemberId"].ToString();
            var familymemberid = Convert.ToInt32(familymembername);
            string doctorid = fc["Doctorid"].ToString();
            string slotid = fc["slotid"].ToString();
            string Price = fc["Price"].ToString();
            int slotid1 = Int32.Parse(slotid);
            var patientid = User.Identity.GetUserId();
            var item = db.DoctorSlotSchedules.Where(Y => Y.Id==slotid1).FirstOrDefault();
            //item.Status = "Booked";
            var doctorprice = db.DoctorProfiles.Where(Y => Y.ApplicationUserId == doctorid).FirstOrDefault();
           
            slotbook.AppointmentDate = item.SlotDate;

            slotbook.BookingDate = DateTime.Now;
            slotbook.DoctorProfileId = doctorid;
            slotbook.DoctorSlotScheduleId =Int32.Parse(slotid);
            slotbook.PatientProfileId = patientid;
            slotbook.Status = "Pending";
            slotbook.PaymentStatus = "Pending";
            //slotbook.AmountCharged = price.PhysicalAppointmentPrice;
            slotbook.OrderId = orderId;

            if(Price== "PhysicalAppointmentPrice")
            {
                slotbook.AmountCharged = doctorprice.PhysicalAppointmentPrice;
                slotbook.AppointmentType = "Physical";
            }
            else if(Price== "VirtualAppointmentPrice")
            {
                slotbook.AmountCharged = doctorprice.VirtualAppointmentPrice;
                slotbook.AppointmentType = "Virtual";
            }
            else if(Price== "VideoAppointmentPrice")
            {
                slotbook.AmountCharged = doctorprice.VideoAppointmentPrice;
                slotbook.AppointmentType = "Video";
            }
            slotbook.FamilyMemberId = familymemberid;
            db.PatientBookings.Add(slotbook);

            db.SaveChanges();
            //return Redirect("https://portal.webdoc.com.pk/transection/A/cc.php?orderid=" + orderId+ "&amount="+ slotbook.AmountCharged);
            return RedirectToAction("PaymentMethod",new {orderid=orderId,amount=slotbook.AmountCharged, });
        //return RedirectToAction("ListDoctor",new { temp=1});
    }
        public ActionResult PaymentMethod(string orderid,string amount,int? temp)
        {
            
            if(temp!=null)
            {
                ViewBag.msg = "Sorry Payment Failed...";
                ViewBag.status = "2";
            }
            ViewBag.OrderId = orderid;
            ViewBag.Amount = amount;
            return View();
        }
        public string GetHash(String text, String key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        public ActionResult JazzCashPaymentApi(FormCollection fc)
        {
            var OrderId = fc["orderid"];
            var Amount = fc["amount"];
            var CNIC = fc["CNIC"];
            var PhoneNo = fc["PhoneNo"];
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string SecurityKey = "s52suwc12f";

            string pp_Amount = Amount+"00";
            string pp_BankID = "";
            string pp_BillReference = "billRef";
            string pp_CNIC = CNIC;
            string pp_Description = "this is transection";
            string pp_Language = "EN";
            string pp_MerchantID = "MC18403";
            string pp_MobileNumber = PhoneNo;
            string pp_Password = "4tx1430us8";
            string pp_ProductID = "";
            string pp_SubMerchantID = "";
            string pp_TxnCurrency = "PKR";
            string pp_TxnDateTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            string pp_TxnExpiryDateTime = DateTime.Now.AddDays(1).ToString("yyyyMMddhhmmss");
            string pp_TxnRefNo = DateTime.Now.ToString("T"+"yyyyMMddhhmmss");
            string pp_SecureHash = "";
            string ppmpf_1 = "";
            string ppmpf_2 = "";
            string ppmpf_3 = "";
            string ppmpf_4 = "";
            string ppmpf_5 = "";

       

           string Key = SecurityKey + "&" + pp_Amount + "&" + pp_BillReference + "&" + pp_CNIC + "&" + pp_Description
                        + "&" + pp_Language + "&" + pp_MerchantID + "&" + pp_MobileNumber + "&" + pp_Password
                        + "&" + pp_TxnCurrency + "&" + pp_TxnDateTime + "&" + pp_TxnExpiryDateTime + "&" + pp_TxnRefNo;


            var hashed = GetHash(Key,SecurityKey);

            JazzCashParam parms = new JazzCashParam()
            {
               pp_TxnRefNo = pp_TxnRefNo,
               pp_Password="4tx1430us8",
               pp_BillReference="billRef",
               pp_TxnExpiryDateTime= pp_TxnExpiryDateTime,
               pp_SecureHash= hashed,
               pp_ProductID="",
               pp_TxnCurrency= "PKR",
               pp_MerchantID="MC18403",
               pp_DiscountedAmount="",
               pp_Amount= pp_Amount,
               ppmpf_5="",
               ppmpf_4= "",
               pp_MobileNumber=PhoneNo,
               pp_Language="EN",
               pp_CNIC=CNIC,
               pp_BankID="",
               ppmpf_1="",
               pp_SubMerchantID="",
               pp_TxnDateTime= pp_TxnDateTime,
               ppmpf_3="",
               pp_Description="this is transection",
               ppmpf_2=""


    };
            HttpClient client1 = new HttpClient();
            string url = "https://sandbox.jazzcash.com.pk/ApplicationAPI/API/2.0/Purchase/DoMWalletTransaction";
            client1.BaseAddress = new Uri("https://sandbox.jazzcash.com.pk/ApplicationAPI/API/2.0/Purchase/DoMWalletTransaction");

            string json = JsonConvert.SerializeObject(parms);
            System.Net.Http.HttpResponseMessage response = client1.PostAsync(url, new StringContent(json, null, "application/json")).Result;
            var FromURL = response.Content.ReadAsStringAsync().Result;
            JavaScriptSerializer j = new JavaScriptSerializer();
            JazzCashResultMapping result = new JazzCashResultMapping();
            result = j.Deserialize<JazzCashResultMapping>(FromURL);
            if(result.pp_ResponseCode=="000")
            {
                return RedirectToAction("GetParemeter",new { O=OrderId});
            }
            return View();
        }
        public ActionResult EasyPaisaPaymentApi(FormCollection fc)
        {
            var OrderId = fc["orderid"];
            var Amount = fc["amount"];
            var PhoneNo = fc["PhoneNo"];
            var Email = fc["Email"];
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var orderId = DateTime.Now.ToString("yyyyMM");
            EasypaisaParam parms = new EasypaisaParam()
            {
                orderId = orderId,
                storeId = "4268",
                transactionAmount = "1.23",
                transactionType = "MA",
                mobileAccountNo = "03467286831",
                emailAddress = "testenmail@gmail.com"

            };
            HttpClient client1 = new HttpClient();
            string url = "https://easypaystg.easypaisa.com.pk/easypay-service/rest/v4/initiate-ma-transaction";
            client1.BaseAddress = new Uri("https://easypaystg.easypaisa.com.pk/easypay-service/rest/v4/initiate-ma-transaction");

            string json = JsonConvert.SerializeObject(parms);
            string EasypaisaCredentialKEy = "V0VCRE9DX25hYmVlbDo1Y2QyMDcyNWIyOTc3ZWZhYTZmOWRlNzdiM2Q1ODZkYg==";
            client1.DefaultRequestHeaders.Add("Credentials", EasypaisaCredentialKEy);
            System.Net.Http.HttpResponseMessage response = client1.PostAsync(url, new StringContent(json, null, "application/json")).Result;
            var FromURL = response.Content.ReadAsStringAsync().Result;
            JavaScriptSerializer j = new JavaScriptSerializer();
            EasypaisaResultMapping result = new EasypaisaResultMapping();
            result = j.Deserialize<EasypaisaResultMapping>(FromURL);
            if(result.responseCode=="0000")
            {
                return RedirectToAction("GetParemeter",new { O = OrderId });
            }
            else
            {
                return RedirectToAction("PaymentMethod", new { orderid = OrderId,amount=Amount,temp=1 });
            }

            return View();
        }
        public ActionResult JSPhoneNo(FormCollection fc,string orderid,string amount)
        {
            ViewBag.OrderId = orderid;
            ViewBag.Amount = amount;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            
            HttpClient client1 = new HttpClient();
            string url = "https://sandbox.jsbl.com/v2/oauth-blb?grant_type=client_credentials";
            client1.BaseAddress = new Uri("https://sandbox.jsbl.com/v2/oauth-blb?grant_type=client_credentials");
            
            //string EasypaisaCredentialKEy = "V0VCRE9DX25hYmVlbDo1Y2QyMDcyNWIyOTc3ZWZhYTZmOWRlNzdiM2Q1ODZkYg==";
            client1.DefaultRequestHeaders.Add("Authorization", "Basic REdFR0p5TEExeUxHcHFHYUxLSENNR3BFRGp5R1Y5SloKCgo6SmxKWnA4Mnp0ejB3ZnZaTQ==");
            client1.DefaultRequestHeaders.Add("Cookie", "__cfduid=d2f65acb28f210862611fb51d9d417f651616218798");
           // HttpResponseMessage response = httpClient.GetAsync("http://sandbox.jsbl.com/v2/oauth-blb?grant_type=client_credentials").Result;
            System.Net.Http.HttpResponseMessage response = client1.GetAsync("https://sandbox.jsbl.com/v2/oauth-blb?grant_type=client_credentials").Result;
            var FromURL = response.Content.ReadAsStringAsync().Result;
            JavaScriptSerializer j = new JavaScriptSerializer();
            JSBankResultMapping result = new JSBankResultMapping();
            result = j.Deserialize<JSBankResultMapping>(FromURL);
            ViewBag.AccessToken = result.access_token;

            return View();
        }

       
        public ActionResult JSBankOTP(FormCollection fc)
        {
            var token = fc["token"];
            ViewBag.orderid = fc["orderid"];
            ViewBag.amount = fc["amount"];
            ViewBag.Token = token;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string orderId = x.ToString("000000");
            var datetime= DateTime.Now.ToString("yyyyMMddhhmmss");
            JSBankAccountInquiryRequestParam parms = new JSBankAccountInquiryRequestParam()
            {

                AccountInquiryRequest = new AccountInquiryRequest() {
                    MerchantType = "0088",
                    TraceNo = orderId,
                    CompanyName = "Webdoc",
                    //DateTime = "20210111121213",
                    DateTime = datetime,
                TerminalId = "APIGEE",
                MobileNo = "03055488062",
                Amount = "20",
                TransactionType = "BEFILER PAYMENT",
                Reserved1 = "02"
                
}

        };
        HttpClient client1 = new HttpClient();
            string url = "https://sandbox.jsbl.com/v2/accountinquiry-blb";
            client1.BaseAddress = new Uri("https://sandbox.jsbl.com/v2/accountinquiry-blb");

            string json = JsonConvert.SerializeObject(parms);
            client1.DefaultRequestHeaders.Add("Authorization", "Bearer "+token);
            //client1.DefaultRequestHeaders.Add("Authorization", "Bearer ATnycVrvpo8PoGAJ6Oct1QSqOrz3");
            client1.DefaultRequestHeaders.Add("Cookie", "__cfduid=db6bea094df5220ddeee94b28bd541ceb1616659271");
            System.Net.Http.HttpResponseMessage response = client1.PostAsync(url, new StringContent(json, null, "application/json")).Result;
            var FromURL = response.Content.ReadAsStringAsync().Result;
            JavaScriptSerializer j = new JavaScriptSerializer();
            JSBankAccountInquiryResponse result = new JSBankAccountInquiryResponse();
            result = j.Deserialize<JSBankAccountInquiryResponse>(FromURL);



            return View();
        }
        public ActionResult JSBankPaymentApi(FormCollection fc)
        {
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string traceno = x.ToString("000000");
            string transectioncode = x.ToString("0000000000000");

            

            var otp = fc["OTP"];
            var publicKey = "<RSAKeyValue><Modulus>MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAiO1lWgkTZeDWQgXlDF8t92YLYZm/ENvCvKPJNuj9WZfGCF5RIUFaYolb/HAhoAHKxgYRUS81WFfHuMROT+B/d0cW+Ii/sqLzTfFjepExonCj1I8m4WLdBAdZCRlWLo+bdO39OpxfK14XaPmRMdb8+uTpZ0hZBhDzZDnXChCm4fgsn63ZT2VEHdHX8PgmKTViR4VXsvyZCkT60FiEix2JdLCuSGF+tPr9GQnlSDJK4vRCZl+/TD/IaIbeAFWcx0Y6kdLpUBBUHbxY8cXcsr/HfJ6/WMEBSlUCOvbZhrx41yC/182WMPppaqCDeDamDV2T+ufzrQkT1nU40gm9h7uoXwIDAQAB</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var testData = Encoding.UTF8.GetBytes(otp);

           var rsa = new RSACryptoServiceProvider(2048);                
           rsa.FromXmlString(publicKey.ToString());

            var encryptedData = rsa.Encrypt(testData, true);

            var base64Encrypted = Convert.ToBase64String(encryptedData);

            
            var orderid = fc["orderid"];
            var amount = fc["amount"];
            var token = fc["token"];
            var datetime = DateTime.Now.ToString("yyyyMMddhhmmss");
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
           

                        JSBankPaymentRequestParam parms = new JSBankPaymentRequestParam()
            {

                PaymentRequest = new PaymentRequest()
                {
             Amount="20",
            Reserved1="02",
            Charges="0.0",
            PaymentType="01",
            TerminalId="APIGEE",
            AccountNumber="03055488062",
            OtpPin= base64Encrypted,
            CompanyName ="Webdoc",
            DateTime= datetime,
            //DateTime="20210301151938",
            MerchantType ="0088",
            TraceNo=traceno,
            TransactionType="BEFILER PAYMENT",
            SettlementType="",
            TransactionCode= transectioncode

                }

            };
            HttpClient client1 = new HttpClient();
            string url = "https://sandbox.jsbl.com/v2/payment-blb";
            client1.BaseAddress = new Uri("https://sandbox.jsbl.com/v2/payment-blb");

            string json = JsonConvert.SerializeObject(parms);
            client1.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client1.DefaultRequestHeaders.Add("Authorization", "Bearer ATnycVrvpo8PoGAJ6Oct1QSqOrz3");
            client1.DefaultRequestHeaders.Add("Cookie", "__cfduid=db6bea094df5220ddeee94b28bd541ceb1616659271");
            System.Net.Http.HttpResponseMessage response = client1.PostAsync(url, new StringContent(json, null, "application/json")).Result;
            var FromURL = response.Content.ReadAsStringAsync().Result;
            JavaScriptSerializer j = new JavaScriptSerializer();
            PaymentResponse result = new PaymentResponse();
            result = j.Deserialize<PaymentResponse>(FromURL);
            if (result.ResponseCode == "00")
            {
                return RedirectToAction("GetParemeter", new { O = orderid });
            }
            else
            {
                return RedirectToAction("PaymentMethod", new { orderid = orderid, amount = amount, temp = 1 });
            }


            return View();
        }
        // ===================  Doctor Details ========================================================ss
        public ActionResult PatientUpcomingAppointments(int? temp)
        {
            if (temp != null)
            {
                ViewBag.msg = "Apointment Deleted Successfully....";
                ViewBag.Status = 1;
            }
            var id = User.Identity.GetUserId();
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
            var list = db.PatientBookings.Where(y => y.PatientProfileId== id && DbFunctions.TruncateTime(y.AppointmentDate)>= date.Date && y.Status=="Active").Include(y=>y.DoctorSlotSchedule).Include(y => y.DoctorProfile).ToList();
            
            return View(list);
        }
        [HttpPost]
        public ActionResult DeletePatientAppointment(FormCollection fc)
        {
            string bookingId = fc["Bookingid"];
            string doctorSlotScheduleid = fc["doctorslotscheduleid"];
            var bookingId1 = Int32.Parse(bookingId);
            var doctorSlotScheduleid1 = Int32.Parse(doctorSlotScheduleid);
            var id = db.PatientBookings.Where(y => y.Id == bookingId1).FirstOrDefault();
            id.Status = "Cancelled";

            var item = db.DoctorSlotSchedules.Where(y => y.Id == doctorSlotScheduleid1).FirstOrDefault();
            item.Status = "NotBooked";
            
            db.SaveChanges();
            return RedirectToAction("PatientUpcomingAppointments",new { temp=1});
        }
        public ActionResult PatientAppointmentHistory(FormCollection fc)
        {
            var patientId = User.Identity.GetUserId();
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
            var list = db.PatientBookings.Where(y =>DbFunctions.TruncateTime(y.AppointmentDate)<=date.Date && y.PatientProfileId== patientId && y.Status== "Complete").Include(y=>y.DoctorProfile).Include(y=>y.PatientProfile).Include(y=>y.DoctorSlotSchedule).ToList().OrderByDescending(y=>y.AppointmentDate);
            
            return View(list);
        }
        public ActionResult DoctorDetails(string id)
        {
            var patientId = User.Identity.GetUserId();
            var date = DateTime.Today;
            var list = db.DoctorProfiles.Where(y => y.ApplicationUserId==id).Include(y => y.DoctorSpecialty).Include(y=>y.ApplicationUser).ToList();

            return View(list);
        }
        public ActionResult DoctorProfiles(string id)
        {
            var userid = User.Identity.GetUserId();
            var patientprofile = db.PatientProfiles.Where(y => y.ApplicationUserId == userid).FirstOrDefault();
            if (patientprofile == null)
            {
                return RedirectToAction("CreatePatientProfile", new { id = userid });
            }
            var list = db.DoctorProfiles.Where(y=>y.ApplicationUser.ClientTableId==1).Include(y=>y.DoctorSpecialty).ToList();

            return View(list);
        }
       
        public ActionResult DoctorSchedule(string id)
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var list = db.DoctorSchedules.Where(y=>y.DoctorProfileId==id &&y.Week== weekNum.ToString() ).Include(y=>y.DoctorProfile).ToList();
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
        public ActionResult DoctorQualification(string id)
        {
            var list = db.DoctorQualifications.Where(y => y.DoctorProfileId==id).Include(y => y.DoctorProfile).ToList();

            return View(list);
        }

        // ===================  Consultation and prescribe Controllers ========================================================ss
        public ActionResult PatientDashboard()
        {
            var id = User.Identity.GetUserId();
            string zoneId = "Pakistan Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);

            var totalappointment = db.PatientBookings.Where(y => y.PatientProfileId == id && DbFunctions.TruncateTime(y.AppointmentDate) <= date.Date && (y.Status == "Completed" || y.Status == "Missed"||y.Status=="Active")).Include(y => y.PatientProfile).Include(y => y.DoctorProfile).Include(y => y.DoctorSlotSchedule).ToList().OrderByDescending(y => y.AppointmentDate);

            ViewBag.TotalAppointment = totalappointment.Count();
            var totalconsultation = db.Consultations.Where(y => y.PatientProfileId == id ).ToList();
            ViewBag.TotalConsultation = totalconsultation.Count();
            var paidamount = db.PatientBookings.Where(y => y.PaymentStatus == "success" && y.PatientProfileId == id).ToList();
            int sum = 0;
            foreach (var item in paidamount)
            {
                sum += Convert.ToInt32(item.AmountCharged);
            }
            ViewBag.paidamount = sum;
            var patientprofile = db.PatientProfiles.Where(y => y.ApplicationUserId == id).FirstOrDefault();
            if (patientprofile == null)
            {
                return RedirectToAction("CreatePatientProfile", new { id = id });
            }
            var list = db.PatientBookings.Where(x => DbFunctions.TruncateTime(x.AppointmentDate) == date.Date && x.Status == "Active" && x.PatientProfileId == id).Include(y => y.DoctorProfile).Include(y => y.PatientProfile).Include(y => y.DoctorSlotSchedule).ToList();
            ViewBag.UpcomingAppointment = db.PatientBookings.Where(y => y.PatientProfileId == id && DbFunctions.TruncateTime(y.AppointmentDate) > date.Date && y.Status == "Active").Include(y => y.PatientProfile).Include(y => y.DoctorProfile).Include(y => y.DoctorSlotSchedule).ToList().OrderBy(y => y.AppointmentDate);
            ViewBag.AppointmentHistory = totalappointment;
            ViewBag.Consultation= db.Consultations.Where(y => y.PatientProfileId == id).Include(y => y.PatientProfile).Include(y => y.DoctorProfile).ToList().OrderByDescending(y => y.ConsultationDate);

            return View(list);
        }
        public ActionResult PatientMedicine(string id)
        {
            var consultid = Int32.Parse(id); 
            var list = db.PrescribeDetials.Where(y=>y.ConsultationId==consultid).Include(y=>y.Consultation).Include(y=>y.MedicineNames).ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult GetParemeter(string RC, string RD, string TS,string O)
        {
            
            ViewBag.test = "Code=" + RC + "   Discription " + RD + "    transection status" + TS + "  Order id " + O;
            var patientbooking = db.PatientBookings.Where(y => y.OrderId == O).Include(y => y.DoctorSlotSchedule.DoctorSchedule).FirstOrDefault();
            if (patientbooking != null)
            {
                patientbooking.DoctorSlotSchedule.Status = "Booked";
                patientbooking.Status = "Active";
                patientbooking.PaymentStatus = "Success";
                patientbooking.Responsepayment = "Success";
                db.SaveChanges();
            }

            ViewBag.patientbookings = db.PatientBookings.Where(y => y.OrderId == O).Include(y => y.DoctorProfile).Include(y => y.DoctorSlotSchedule).ToList();
            var BookingDate = patientbooking.BookingDate;
            var AppointmentDate = patientbooking.AppointmentDate;
            ViewBag.appointmentdate = AppointmentDate.ToString("dd/MMMM/yyyy", new CultureInfo("en-CA"));
            ViewBag.bookingdate = BookingDate.ToString("dd/MMMM/yyyy hh:mm:ss tt", new CultureInfo("en-CA"));

            return View();
        }

        public ActionResult DoctorList(int? temp,string id)
        {
            var id1 = User.Identity.GetUserId();
            var familymember = db.FamilyMembers.Where(y=>y.PatientProfileId== id1).ToList();
            ViewBag.Id = new SelectList(familymember, "Id", "Name");

            if (id!=null)
            { 
            ViewBag.doctorid = id;
            var doctordata = db.DoctorProfiles.Where(y => y.ApplicationUserId == id).FirstOrDefault();
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
            var userid = User.Identity.GetUserId();
            var patientprofile = db.PatientProfiles.Where(y => y.ApplicationUserId == userid).FirstOrDefault();
            if (patientprofile == null)
            {
                return RedirectToAction("CreatePatientProfile", new { id = userid });
            }
            
            var list = db.DoctorProfiles.Where(y => y.Status == "Approved" && y.ApplicationUser.ClientTableId == 1).Include(y => y.ApplicationUser).ToList();
            return View(list);
        }
        
        public ActionResult DoctorListNew(string id)
        {
            

            return RedirectToAction("DoctorList",new { id=id});
        }
        public ActionResult GetValues(string id,string dateData)
        {
            List<DoctorSlotsContract> dsc = new List<DoctorSlotsContract>();
            DateTime date = Convert.ToDateTime(dateData);
            var schedule = db.DoctorSlotSchedules.Include(i => i.DoctorSchedule.DoctorProfile.ApplicationUser).Where(r => r.DoctorSchedule.DoctorProfileId == id &&  r.Status == "NotBooked" && DbFunctions.TruncateTime(r.SlotDate) == date).ToList().GroupBy(r => r.Days);
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
        // ===================  Family Member ========================================================ss
        public ActionResult AddFamilyMember()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddFamilyMember(Models.FamilyMember family)
        {
            family.PatientProfileId = User.Identity.GetUserId();
            db.FamilyMembers.Add(family);
            db.SaveChanges();
            return RedirectToAction("DetailPatientProfile");
        }
        public ActionResult ListFamilyMember()
        {
            var userid = User.Identity.GetUserId();

            var familymember = db.FamilyMembers.Where(y => y.PatientProfileId == userid).FirstOrDefault();
            var list = db.FamilyMembers.Where(y=>y.PatientProfileId==userid).Include(y=>y.PatientProfile).ToList();
           
            if (familymember == null)
            {
                return RedirectToAction("AddFamilyMember");
            }
            else
            {
                return View(list);
            }
        }
        public ActionResult EditFamilyMember(string id)
        {
            var id1 = Convert.ToInt32(id);
            var list = db.FamilyMembers.Where(y => y.Id == id1).FirstOrDefault();
            ViewBag.memberid = id;
            return View(list);
        }
        [HttpPost]
        public ActionResult EditFamilyMember(Models.FamilyMember family,FormCollection fc)
        {
            var id = fc["Id"];
            var id1 = Convert.ToInt32(id);
            family.PatientProfileId = User.Identity.GetUserId();
            var list = db.FamilyMembers.Where(y => y.Id == id1).FirstOrDefault();
            list.Name = family.Name;
            list.Relation = family.Relation;
            db.SaveChanges();
            return RedirectToAction("ListFamilyMember");
        }
        public ActionResult DeleteFamilyMember(string id)
        {
            var id1 = Convert.ToInt32(id);
            var list = db.FamilyMembers.Where(y => y.Id == id1).FirstOrDefault();
            db.FamilyMembers.Remove(list);
            db.SaveChanges();
            return RedirectToAction("ListFamilyMember");
        }
        public string URLCalling1()
        {
            var userid = User.Identity.GetUserId();
            var userdata = db.Users.Where(y => y.Id == userid).FirstOrDefault();
            //var a = userdata.Email;
            
            ParamInsurance parms = new ParamInsurance()
            {
                Email ="testpatient@apnadoctor.com.pk",
                Password="8484"
                
            };
            HttpClient client1 = new HttpClient();
            string url = "https://apnadoctorapi.webddocsystems.com/Service1.svc/Login";
            client1.BaseAddress = new Uri("https://apnadoctorapi.webddocsystems.com/Service1.svc/Login");

            string json = JsonConvert.SerializeObject(parms);

            client1.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");
            System.Net.Http.HttpResponseMessage response = client1.PostAsync(url, new StringContent(json, null, "application/json")).Result;
            var FromURL = response.Content.ReadAsStringAsync().Result;


            //List<LoginResult> dd = new List<LoginResult>();//convert response to model
            //JavaScriptSerializer j = new JavaScriptSerializer();
            //loginresultmapping result = new loginresultmapping();
            // result = j.Deserialize<loginresultmapping>(FromURL);

            return FromURL;
        }
        public ActionResult InsurancePolicy()
        {
            var response = URLCalling1();
            List<LoginResult> dd = new List<LoginResult>();//convert response to model
            JavaScriptSerializer j = new JavaScriptSerializer();
            loginresultmapping result = new loginresultmapping();
            result = j.Deserialize<loginresultmapping>(response);
            var GetPanelDetails = result.LoginResult.PanelDetails;
            ViewBag.PanelDeatil = GetPanelDetails;

            return View();
        }
        public ActionResult GetInsurancePolicyData()
        {
            var list = db.InsuranceCompanies.ToList();
            return View(list);
        }


    }
}