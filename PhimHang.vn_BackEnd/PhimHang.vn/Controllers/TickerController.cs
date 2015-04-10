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
    public class TickerController : Controller
    {
         private KNDTLocalConnection db = new KNDTLocalConnection();        
        //
        // GET: /Ticker/
         public ActionResult Index(int? page, string stockCode, int? monthReport)
         {
             if (string.IsNullOrWhiteSpace(stockCode))
             {
                 stockCode = "ALL";
             }
             if (monthReport == null)
             {
                 monthReport = DateTime.Now.Month;
             }
             ViewBag.stockCode = stockCode;
             ViewBag.monthReportV = monthReport;
             LoadInit();
             var recommendstocks = from r in db.RecommendStocks.Include(r => r.UserLogin)
                                   orderby r.CreatedModify descending
                                   where  (r.StockCode.Contains(stockCode) || "ALL"  == stockCode)
                                   && (r.CreatedDate.Month == monthReport || 0 == monthReport)
                                   && (r.CreatedDate.Year == DateTime.Now.Year)
                                   select r;
             int pageSize = 10;
             int pageNumber = (page ?? 1);

             return View(recommendstocks.ToPagedList(pageNumber, pageSize));
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

             var monthReport = new List<dynamic>
             {
                   new { Id = "0",Name = string.Empty  },
                  new { Id = "1",Name = "1"  },
                        new { Id = "2",Name = "2" },
                        new { Id = "3",Name = "3"},
                        new { Id = "4" ,Name = "4"},
                        new { Id = "5" ,Name = "5"},
                        new { Id = "6" ,Name = "6"},
                        new { Id = "7" ,Name = "7"},
                        new { Id = "8",Name = "8"},
                        new { Id = "9" ,Name = "9"},
                        new { Id = "10" ,Name = "10"},
                        new { Id = "11" ,Name = "11"},
                        new { Id = "12" ,Name = "12"}
             };
             ViewBag.monthReport = new SelectList(monthReport, "Id", "Name");


         }
	}
}