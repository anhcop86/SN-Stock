using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.DAL;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {

            using (var db = new SchoolContext())
            {
                var djkf = db.Courses.ToList();
            }
            return View();
        }

        public ActionResult About()
        {
            var data = from students in db.Students
                       group students by students.EnrollmentDate into datagroup

                       select new EnrollmentDateGroup()
                       {
                           EnrollmentDate = datagroup.Key,
                           StudentCount = datagroup.Count()
                       };

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}