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
    public class TickerController : Controller
    {
        private readonly StockRealTimeTicker _stockRealtime;
        public TickerController()
            : this(StockRealTimeTicker.Instance,new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public TickerController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            _stockRealtime = stockTicker;
            UserManager = userManager;
          
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        private const string ImageURLAvata = "/images/avatar/";
        private string AbsolutePathHostName = AppHelper.AbsolutePathHostName;
        
        public async Task<ViewResult> Index(string symbolName)
        {            
            //ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            ViewBag.AbsolutePathHostName = AbsolutePathHostName;
            #region danh muc co phieu dang follow
            var postNumber = await db.Posts.CountAsync(s => s.StockPrimary.Contains(symbolName)); // so luong bai viet cua cổ phiếu này
            var stockFollowNumber = await db.FollowStocks.CountAsync(sf => sf.StockFollowed == symbolName); // bao nhieu nguoi da theo doi co phieu nay
            ViewBag.PostNumber = postNumber;
            ViewBag.StockFollowNumber = stockFollowNumber;
            // function follow stock
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var countStockFollowr = await db.FollowStocks.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id && f.StockFollowed == symbolName);
                if (countStockFollowr == 1) // kiem tra user nay co follow ma nay khong
                {
                    ViewBag.CheckStockExist = "Y";
                }
                else
                {
                    ViewBag.CheckStockExist = "N";
                }
                // so luong tin nhan
                var numberMessegeNew = await db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).SumAsync(mn => mn.NumNoti);
                ViewBag.NewMessege = numberMessegeNew;
            }

            #endregion

            #region Thong tin menu ben trai           
            if (User.Identity.IsAuthenticated) // thong tin user dang nhap
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.UserExtentLogin.AvataImage;
                ViewBag.CureentUserId = currentUser.UserExtentLogin.Id;
                ViewBag.UserName = currentUser.UserName;
                ViewBag.CharacterLimit = currentUser.UserExtentLogin.CharacterLimit;
            }
            else
            {
                ViewBag.AvataEmage = ImageURLAvataDefault;
            }


            // End thong tin menu ben trai


            #endregion

            #region thong tin co phieu
            var company = new StockCode();
            company = await db.StockCodes.FirstOrDefaultAsync(m => m.Code.ToUpper() == symbolName.ToUpper());
            ViewBag.StockCode = company == null ? StatusSymbol.NF.ToString() : symbolName.ToUpper();
            ViewBag.StockName = company == null ? StatusSymbol.NF.ToString() : company.ShortName;
            ViewBag.LongName = company == null ? StatusSymbol.NF.ToString() : company.LongName;
            ViewBag.MarketName = company == null ? StatusSymbol.NF.ToString() : company.IndexName;
            ViewBag.ImgEx = ".png";
            #endregion

            #region gia co phieu            
            StockRealTime stockprice = new StockRealTime();
            stockprice = _stockRealtime.GetStocksByTicker(symbolName).Result;
            if (stockprice == null)
            {
                stockprice = new StockRealTime();
                stockprice.CompanyID = symbolName;
            }
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            
            #endregion
            #region danh muc co phieu nong
            var listHotStock = await AppHelper.GetListHotStock();
            ViewBag.ListStockHot = listHotStock;
            #endregion
            
            return View(stockprice);

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