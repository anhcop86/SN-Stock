using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhimHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PhimHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly FilterKeyworkSingleton _keyword;
        public HomeController()
            : this(FilterKeyworkSingleton.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public HomeController(FilterKeyworkSingleton KeyworkSing, UserManager<ApplicationUser> userManager)
        {
            _keyword = KeyworkSing;
            UserManager = userManager;
        }


        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        public async Task<ActionResult> Index()
        {
            //return RedirectToAction("", "MyProfile");
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var numberMessegeNew = db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).Sum(mn => mn.NumNoti);
                ViewBag.NewMessege = numberMessegeNew;
                
                
            }
            
            return View();
        }     

        public ActionResult About()
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