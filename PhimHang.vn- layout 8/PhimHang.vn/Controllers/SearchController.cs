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
    public class SearchController : Controller
    {
        private readonly StockRealTimeTicker _stockRealtime;
        private testEntities db = new testEntities();
        public SearchController()
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public SearchController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            _stockRealtime = stockTicker;
            UserManager = userManager;
        }
       
        public UserManager<ApplicationUser> UserManager { get; private set; }
        //
        // GET: /Search/
        //private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        //private const string ImageURLAvata = "/images/avatar/";
        public async Task<ActionResult> Index(string q)
        {
            #region thong tin user dang nnhap
            ViewBag.SearchString = q;
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser currentUser = new ApplicationUser();
                currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + currentUser.UserExtentLogin.AvataImage;
                var numberMessegeNew = await db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).SumAsync(mn => mn.NumNoti);
                ViewBag.NewMessege = numberMessegeNew;
            }
            else
            {
                ViewBag.AvataEmage = AppHelper.ImageURLAvataDefault;
            }

            #endregion
            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion
            #region danh muc co phieu nong
            var listHotStock = await AppHelper.GetListHotStock();
            ViewBag.ListStockHot = listHotStock;
            #endregion

            if (string.IsNullOrEmpty(q))
            {
                ViewBag.listStockPriceFind = new List<StockRealTime>();
                ViewBag.UserFindList = new List<UserFollowView>();
                return View();
            }
            else
            {
                //var search = db.StockCodes.FirstOrDefault(s => s.Code.StartsWith(q.Replace("$", ""))).Code;
                var searstring = q.RemoveSpecialString();
                var searchStockList = await (from sc in db.StockCodes
                                             where sc.Code.Contains(searstring)
                                       && (sc.MarketType == 0 || sc.MarketType == 1 || sc.MarketType == 3)
                                             select sc.Code).ToListAsync();
                var searchUser = await (from us in db.UserLogins
                                        where us.UserNameCopy.Contains(searstring)
                                        select new UserFollowView
                                        {
                                            UserId = us.Id,
                                            UserName = us.UserNameCopy,
                                            Status = us.Status,
                                            Avata = string.IsNullOrEmpty(us.AvataImage) ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + us.AvataImage,
                                            BrkVip = us.BrokerVIP
                                        }).ToListAsync();

                //load gia co phieu tim duoc
                #region gia cổ phieu cua cac ma tim duoc
                ViewBag.listStockPriceFind = _stockRealtime.GetAllStocksList(searchStockList as List<string>).Result;
                #endregion
                // Nguoi dung tim dc                
                #region gia cổ phieu cua cac ma tim duoc
                ViewBag.UserFindList = searchUser;
                #endregion
                #region thong bao cho nguoi dung biet la khong tim thay

                ViewBag.NoStockFind = ": " + searchStockList.Count + " Cổ phiếu";

                ViewBag.NoUserFind = ": " + searchUser.Count + " Người dùng";

                #endregion
                return View();

            }

        }
        

        //[HttpGet]
        //public async Task<dynamic> GetStockPriChart(string chart)
        //{
        //    var ret = (from sp in db.StockPrices
        //               orderby sp.TradingDate
        //               where sp.Code == "HAG"
        //               select new
        //               {
        //                   t = sp.TradingDate,
        //                   o = sp.OpenPrice,
        //                   h = sp.HighPrice,
        //                   l = sp.LowPrice,
        //                   c = sp.ClosePrice,
        //                   s = sp.Totalshare
        //               }).ToList();
        //    //var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
        //    //return result;
        //    return Json(ret, JsonRequestBehavior.AllowGet);
            
        //}


	}
}