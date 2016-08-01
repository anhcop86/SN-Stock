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
using System.Diagnostics;
using System.Web.Helpers;

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
        private testEntities db = new testEntities();
        //public List<string> _listStock { get; set; }
        public async Task<ActionResult> Index()
        {
            #region [Login User]
            if (User.Identity.IsAuthenticated)// neu dang nhap thi cho ve thẳng myprofile
            {
                return RedirectToAction("", "myprofile");
            }
            
            ViewBag.AbsolutePathHostName = AppHelper.AbsolutePathHostName;
            if (User.Identity.IsAuthenticated)
            {

                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                ViewBag.CureentUserId = currentUser.Id;
                ViewBag.UserName = currentUser.UserName;

                ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + currentUser.UserExtentLogin.AvataImage;
                ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataCover) == true ? AppHelper.ImageURLCoverDefault : AppHelper.ImageURLCover + currentUser.UserExtentLogin.AvataCover;

                // End thong tin menu ben trai
                //so luong tin cua User
                var numberMessegeNew = db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).Sum(mn => mn.NumNoti);
                ViewBag.NewMessege = numberMessegeNew;
            }
            else
            {
                ViewBag.AvataEmage = AppHelper.ImageURLAvataDefault;
            }
            #endregion

            #region Set Info of hot stock
            ViewBag.ListStockHot = AppHelper.GetListHotStock();
            #endregion

            #region random dan phim chuyen nghiep
            var DanPhimRandom = await (from u in db.UserLogins
                                       orderby Guid.NewGuid()
                                       where u.BrokerVIP == true
                                       select new UserRandom
                                       {
                                           Avata = string.IsNullOrEmpty(u.AvataImage) ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + u.AvataImage,
                                           UserName = u.UserNameCopy,
                                           FullName = u.FullName
                                       }).Take(5).ToListAsync();
            ViewBag.DanPhimRandom = DanPhimRandom;
            #endregion

            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion

            return View();
        }

        
        /// <summary>
        ///  Tra ve co phieu ngau nhien ben trai trang home bang ajax load tuan tu
        /// </summary>
        /// <returns></returns>
        public ActionResult RandomStockList()
        {
            var result = _stockRealtime.TrenStocksList(AppHelper.GetListHotStock());
            return PartialView("_Partial_Area_Left_Home2", result);
        }

        public ActionResult LoadFacebokSDK()
        {
            return PartialView("_Partial_Facebook_SDK");
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
        [HttpGet]
        public async Task<dynamic> GetMorePostsGlobal(int skipposition, string filter)
        {
            if (filter == "" || filter == "ALL")
            {
                var ret = await (from posts in db.Posts
                                 where posts.StockPrimary != ""
                                 orderby posts.Priority descending, posts.PostedDate descending
                                 select new
                                 {
                                     Message = posts.Message,
                                     Chart = posts.ChartImageURL,
                                     PostedByName = posts.UserLogin.UserNameCopy,
                                     PostedByAvatar = string.IsNullOrEmpty(posts.UserLogin.AvataImage) ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + posts.UserLogin.AvataImage,
                                     PostedDate = posts.PostedDate,
                                     PostId = posts.PostId,                                     
                                     Stm = posts.NhanDinh,
                                     ChartYN = posts.ChartYN,
                                     SumLike = posts.SumLike,
                                     SumReply = posts.SumReply,
                                     BrkVip = posts.UserLogin.BrokerVIP,
                                     Pri = posts.Priority
                                 }).Skip(skipposition).Take(10).ToArrayAsync();                         
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}