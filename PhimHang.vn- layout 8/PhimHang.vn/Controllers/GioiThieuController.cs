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
using System.Text.RegularExpressions;

namespace PhimHang.Controllers
{
    public class GioiThieuController : Controller
    {
        //
        // GET: /GioiThieu/
        private readonly StockRealTimeTicker _stockRealtime;
         public GioiThieuController()
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
         public GioiThieuController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            _stockRealtime = stockTicker;
            UserManager = userManager;
        }
       
        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();        
        public async Task<ActionResult> Index()
        {
            using (db = new testEntities())
            {

                if (User.Identity.IsAuthenticated)
                {
                    ApplicationUser currentUser = new ApplicationUser();
                    currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    ViewBag.CureentUserId = currentUser.Id;
                    ViewBag.UserName = currentUser.UserName;
                    ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + currentUser.UserExtentLogin.AvataImage;
                    var numberMessegeNew = await db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).SumAsync(mn => mn.NumNoti);
                    ViewBag.NewMessege = numberMessegeNew;
                }
                else
                {
                    ViewBag.AvataEmage = AppHelper.ImageURLAvataDefault;
                }

                ViewBag.AbsolutePathHostName = AppHelper.AbsolutePathHostName;
                #region gia chi so index va hnxindex
                var listIndex = new List<string>();
                listIndex.Add("VNINDEX");
                listIndex.Add("HNXINDEX");
                ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
                #endregion

                #region Set Info of hot stock
                ViewBag.ListStockHot = AppHelper.GetListHotStock();
                #endregion

                // end
                return View();
            }
        }
	}
}