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


namespace PhimHang.Controllers
{
    public class ThiTruongController : Controller
    {
        private readonly StockRealTimeTicker _stockRealtime;
        private testEntities db = new testEntities();
        public ThiTruongController()
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public ThiTruongController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            _stockRealtime = stockTicker;
            UserManager = userManager;
        }
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public async Task<ActionResult> Index(string g)
        {
            ViewBag.AbsolutePathHostName = AppHelper.AbsolutePathHostName;
            #region thong tin user dang nnhap
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
            //Top Menu
            #region danh muc co phieu nong
            var listHotStock = await AppHelper.GetListHotStock();
            ViewBag.ListStockHot = listHotStock;
            #endregion

            #region seach ma co phieu

            if (string.IsNullOrEmpty(g))
            {
                ViewBag.listStockGroup = new List<StockCodeColunn>();
            }
            ViewBag.GroupName = g.ToUpper();
            switch (g)
            {
                case "UPCOM":
                    await GetUPCOMList();
                    break;
                case "HOTSTOCK":
                    await GetHOTSTOCKList(listHotStock);
                    break;
                case "INDEX":
                    GetINDEXList();
                    break;
                default:
                    await GetNormalStockList(g);
                    break;
            }
         
            return View();
            #endregion
        }

        private void GetINDEXList()
        {
            var searchStockList = _stockRealtime.GetIndexList();
            ViewBag.listStockGroup = AssignList(searchStockList.Result);
        }
        /// <summary>
        /// Assign add value from soure list to desc list
        /// </summary>
        /// <param name="source"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        private List<StockCodeColunn> AssignList(IEnumerable<StockRealTime> source)
        {
            var result = new List<StockCodeColunn>();
            //foreach (var item in source)
            //{
            //   var descItem = desc.FirstOrDefault(de => de.CompanyName == item.CompanyID);
            //   if (descItem!=null)
            //   {
            //       descItem.Diff = item.Diff;
            //       descItem.DiffRate = item.DiffRate;
            //       descItem.FinishPrice = item.FinishPrice;
            //   }              
            //}
            foreach (var item in source)
            {
                result.Add(new StockCodeColunn
                {
                    CompanyName = item.CompanyID,
                    Diff = item.Diff,
                    DiffRate = item.DiffRate,
                    FinishPrice = item.FinishPrice,
                    MarketName = string.Empty,
                    StockCode = item.CompanyID

                });
            }

            return result;
        }
        private async Task GetNormalStockList(string g)
        {
            var searchStockList = await (from sc in db.StockCodes
                                         orderby sc.Code
                                         where sc.Code.StartsWith(g)
                                   && (sc.MarketType == 0 || sc.MarketType == 1)
                                         select new StockCodeColunn
                                         {
                                             CompanyName = sc.LongName,
                                             MarketName = sc.IndexName,
                                             StockCode = sc.Code,

                                         }).ToListAsync();

            foreach (var item in searchStockList)
            {
                StockRealTime stp = _stockRealtime.GetStocksByTicker(item.StockCode).Result;
                if (stp != null)
                {
                    item.FinishPrice = stp.FinishPrice;
                    item.Diff = stp.Diff;
                    item.DiffRate = stp.DiffRate;
                }
                else
                {
                    item.FinishPrice = 0;
                    item.Diff = 0;
                    item.DiffRate = 0;
                }

            }


            ViewBag.listStockGroup = searchStockList;
        }

        private async Task GetHOTSTOCKList(List<string> listHotStock)
        {
            var searchStockList = await (from sc in db.StockCodes
                                         orderby sc.Code
                                         where listHotStock.Contains(sc.Code)
                                   && (sc.MarketType == 0 || sc.MarketType == 1)
                                         select new StockCodeColunn
                                         {
                                             CompanyName = sc.LongName,
                                             MarketName = sc.IndexName,
                                             StockCode = sc.Code,

                                         }).ToListAsync();

            foreach (var item in searchStockList)
            {
                StockRealTime stp = _stockRealtime.GetStocksByTicker(item.StockCode).Result;
                if (stp != null)
                {
                    item.FinishPrice = stp.FinishPrice;
                    item.Diff = stp.Diff;
                    item.DiffRate = stp.DiffRate;
                }
                else
                {
                    item.FinishPrice = 0;
                    item.Diff = 0;
                    item.DiffRate = 0;
                }

            }


            ViewBag.listStockGroup = searchStockList;
        }

        private async Task GetUPCOMList()
        {
            var searchStockList = await (from sc in db.StockCodes
                                         orderby sc.Code
                                         where sc.MarketType == 3
                                         select new StockCodeColunn
                                         {
                                             CompanyName = sc.LongName,
                                             MarketName = sc.IndexName,
                                             StockCode = sc.Code,
                                         }).ToListAsync();
            foreach (var item in searchStockList)
            {
                StockRealTime stp = _stockRealtime.GetStocksByTicker(item.StockCode).Result;
                if (stp != null)
                {
                    item.FinishPrice = stp.FinishPrice;
                    item.Diff = stp.Diff;
                    item.DiffRate = stp.DiffRate;
                }
                else
                {
                    item.FinishPrice = 0;
                    item.Diff = 0;
                    item.DiffRate = 0;
                }

            }

            ViewBag.listStockGroup = searchStockList;
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
    }
    public class StockCodeColunn
    {
        public string StockCode { get; set; }
        public string CompanyName { get; set; }
        public string MarketName { get; set; }

        public decimal FinishPrice { get; set; }
        public decimal Diff { get; set; }
        public decimal DiffRate { get; set; }
    }
}