using ApnaDoctor.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ApnaDoctor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = User.Identity.GetUserId();

            if (User.IsInRole(UserRole.Patient))
            {
                //var status = db.PatientProfiles.Where(y => y.ApplicationUserId == user).FirstOrDefault();
                return RedirectToAction("PatientDashboard", "Patient");
                
            }
            else if (User.IsInRole(UserRole.Doctor))
            {
                var list = db.DoctorProfiles.Where(y => y.ApplicationUserId == user).FirstOrDefault();
                if(list==null)
                {
                    return RedirectToAction("DoctorDashboard", "Doctor");
                }
                else if (list.Status == "Approved")
                {
                    return RedirectToAction("DoctorDashboard", "Doctor");
                }
            }
            else if (User.IsInRole(UserRole.Admin))
            {
                var list = db.AdminProfiles.Where(y => y.ApplicationUserId == user).FirstOrDefault();
                if(list==null)
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if(list.Status=="Approved")
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                    
                
            }
            else if (User.IsInRole(UserRole.SuperAdmin))
            {
                    return RedirectToAction("SuperAdminDashBoard", "SuperAdmin");
            }

            return RedirectToAction("Login","Account",new { temp=1});
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult View2()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}