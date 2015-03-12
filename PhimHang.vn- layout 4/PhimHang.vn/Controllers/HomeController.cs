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
    //[Authorize] // xoa khi public
    public class HomeController : Controller
    {
        //private readonly FilterKeyworkSingleton _keyword;
        private readonly StockRealTimeTicker _stockRealtime;
        public HomeController()
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public HomeController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            //_keyword = KeyworkSing;
            _stockRealtime = stockTicker;
            UserManager = userManager;
        }


        public UserManager<ApplicationUser> UserManager { get; private set; }
        private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        private const string ImageURLAvata = "/images/avatar/";
        private testEntities db = new testEntities();
        public async Task<ActionResult> Index()
        {
            //return RedirectToAction("", "MyProfile");
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var numberMessegeNew = db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).Sum(mn => mn.NumNoti);
                ViewBag.NewMessege = numberMessegeNew;
                ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.UserExtentLogin.AvataImage;

            }
            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VnIndex");
            listIndex.Add("HNXIndex");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion
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