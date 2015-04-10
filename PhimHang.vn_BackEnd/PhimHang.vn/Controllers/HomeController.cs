using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhimHang.Models;
using PagedList;
using System.Data.Entity;

namespace PhimHang.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private KNDTLocalConnection db = new KNDTLocalConnection();        

        public async Task<ActionResult> Index(int? page, int? postBy, string recommentType, string stockCode, string dateFilter)
        {

            if (postBy == null)
            {                
                postBy = 0;
            }
            if (string.IsNullOrWhiteSpace(recommentType))
            {
                recommentType = "ALL";
            }

            if (string.IsNullOrWhiteSpace(stockCode))
            {
                stockCode = "ALL";
            }
            var datetimeFilter = new DateTime();
            if (string.IsNullOrWhiteSpace(dateFilter) || dateFilter == "ALL")
            {
                dateFilter = "ALL";
                
            }
            else
            {
                datetimeFilter = new DateTime(int.Parse(dateFilter.Substring(6, 4)), int.Parse(dateFilter.Substring(3, 2)), int.Parse(dateFilter.Substring(0, 2)));
            }
            

                
            ViewBag.postBy = postBy;
            ViewBag.recommentType = recommentType;
            ViewBag.stockCode = stockCode;
            ViewBag.datefilter = dateFilter;
            LoadInit();

            var recommendstocks = from r in db.RecommendStocks.Include(r => r.UserLogin)
                                  orderby r.CreatedModify descending
                                  where (r.PostBy == postBy || 0 == postBy)
                                  && (r.TYPERecommend == recommentType || "ALL" == recommentType)
                                  && (r.StockCode.Contains(stockCode) || "ALL"  == stockCode)
                                  && (r.CreatedDate == datetimeFilter || new DateTime() == datetimeFilter)
                                  select r;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(recommendstocks.ToPagedList(pageNumber, pageSize));

            //return View();
        }
        public async Task<ActionResult> ModifyRecommend(int? page, int? postBy, string recommentType, string stockCode, string dateFilter)
        {
            if (User.Identity.Name != "long.thai")
            {                
                return RedirectToAction("", "");
            }
            if (postBy == null)
            {
                postBy = 0;
            }
            if (string.IsNullOrWhiteSpace(recommentType))
            {
                recommentType = "ALL";
            }

            if (string.IsNullOrWhiteSpace(stockCode))
            {
                stockCode = "ALL";
            }
            var datetimeFilter = new DateTime();
            if (string.IsNullOrWhiteSpace(dateFilter) || dateFilter == "ALL")
            {
                dateFilter = "ALL";

            }
            else
            {
                datetimeFilter = new DateTime(int.Parse(dateFilter.Substring(6, 4)), int.Parse(dateFilter.Substring(3, 2)), int.Parse(dateFilter.Substring(0, 2)));
            }



            ViewBag.postBy = postBy;
            ViewBag.recommentType = recommentType;
            ViewBag.stockCode = stockCode;
            ViewBag.datefilter = dateFilter;
            LoadInit();

            var recommendstocks = from r in db.RecommendStocks.Include(r => r.UserLogin)
                                  orderby r.CreatedModify descending
                                  where (r.PostBy == postBy || 0 == postBy)
                                  && (r.TYPERecommend == recommentType || "ALL" == recommentType)
                                  && (r.StockCode.Contains(stockCode) || "ALL" == stockCode)
                                  && (r.CreatedDate == datetimeFilter || new DateTime() == datetimeFilter)
                                  select r;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(recommendstocks.ToPagedList(pageNumber, pageSize));

            //return View();
        }

        private async Task LoadInit()
        {
            //var listStock = (from s in dbstox.stox_tb_Company.ToList()
            //                 orderby s.Ticker
            //                 where s.ExchangeID == 0
            //                 select new
            //                 {
            //                     Ticker = s.Ticker
            //                 }).ToList();

            //ViewBag.listStock = new SelectList(listStock, "Ticker", "Ticker");


            ViewBag.listUserId = new SelectList(db.UserLogins, "Id", "UserNameCopy");

            var listTypeRecomendation = new List<dynamic>
                    { 
                        new { Id = "MUA", Name = "MUA" },
                        new { Id = "BAN", Name = "BÁN" } 
                    }.ToList();

            ViewBag.listTypeRecomendation = new SelectList(listTypeRecomendation, "Id", "Name");

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