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
    [Authorize]
    public class TickerController : Controller
    {
        public TickerController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public TickerController(UserManager<ApplicationUser> userManager)
        {
            //_stockRealtime = stockTicker;
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        private const string ImageURLAvata = "/images/avatar/";

        [AllowAnonymous]
        public async Task<ViewResult> Index(string symbolName)
        {
            var company = new StockCode();
            //ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            #region danh muc co phieu dang follow
            var postNumber = await db.StockRelates.CountAsync(s => s.StockCodeRelate == symbolName);
            var stockFollowNumber = await db.FollowStocks.CountAsync(sf => sf.StockFollowed == symbolName);
            ViewBag.PostNumber = postNumber;
            ViewBag.StockFollowNumber = stockFollowNumber;
            // function follow stock
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var countStockFollowr = await db.FollowStocks.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id && f.StockFollowed == symbolName);
                if (countStockFollowr == 1)
                {
                    ViewBag.CheckStockExist = "Y";
                }
                else
                {
                    ViewBag.CheckStockExist = "N";
                }
                // so luong tin nhan
                var numberMessegeNew = db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).Sum(mn => mn.NumNoti);
                ViewBag.NewMessege = numberMessegeNew;
            }

            #endregion

            #region Thong tin menu ben trai           
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.UserExtentLogin.AvataImage;
                ViewBag.CureentUserId = currentUser.UserExtentLogin.Id;
                ViewBag.UserName = currentUser.UserName;
                ViewBag.AvataImageUrl = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + currentUser.UserExtentLogin.AvataImage + "?width=50&height=50&mode=crop";
            }
            else
            {
                ViewBag.AvataEmage = ImageURLAvataDefault;
            }


            // End thong tin menu ben trai


            #endregion

            #region thong tin co phieu
            company = await db.StockCodes.FirstOrDefaultAsync(m => m.Code.ToUpper() == symbolName.ToUpper());
            ViewBag.StockCode = company == null ? StatusSymbol.NF.ToString() : symbolName.ToUpper();
            ViewBag.StockName = company == null ? StatusSymbol.NF.ToString() : company.ShortName;
            ViewBag.LongName = company == null ? StatusSymbol.NF.ToString() : company.LongName;
            ViewBag.MarketName = company == null ? StatusSymbol.NF.ToString() : company.IndexName;



            #endregion



            #region danh muc dau tu
            //var followstocks = await db.FollowStocks.Where(f => f.UserId == currentUser.UserExtentLogin.Id).ToListAsync();
            //var listfollowstocksString = (from sf in followstocks
            //                              select sf.StockFollowed).ToList();
            //var DMDTShortName = (from fs in db.FollowStocks.ToList()
            //                     join s in db.StockCodes.ToList() on fs.StockFollowed equals s.Code
            //                     where listfollowstocksString.Contains(fs.StockFollowed)
            //                     select new StockDetail
            //                     {
            //                         Stock = fs.StockFollowed,
            //                         ShortName = s.ShortName
            //                     }).ToList();
            //ViewBag.HotStockDMDT = DMDTShortName;//_stockRealtime.GetAllStocksTestList(listfollowstocksString).Result;
            #endregion

            ////return View(_stockRealtime.GetAllStocksTestList((List<string>)Session["listStock"]).Result);
            return View();

        }
        #region Extent
        public enum StatusSymbol
        {
            NF
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}