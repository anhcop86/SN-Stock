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
         private db_cungphim_FrontEnd dbcungphim = new db_cungphim_FrontEnd();
        //
        // GET: /Ticker/
         public ActionResult Index(int? page, string stockCode, short? marketType)
         {
             ViewBag.linkAbsolutePath = Request.Url.PathAndQuery;
             if (string.IsNullOrWhiteSpace(stockCode))
             {
                 stockCode = "ALL";
             }            
             ViewBag.stockCode = stockCode;
             if (marketType == null)
             {
                 marketType = -1;
             }
             ViewBag.marketType = marketType;
             LoadInit();
             var tickers = from r in dbcungphim.StockCodes
                                   orderby r.Code ascending
                                   where  (r.Code.Contains(stockCode) || "ALL"  == stockCode)
                                   && (r.MarketType == marketType || -1 == marketType)
                                   select r;
             int pageSize = 20;
             int pageNumber = (page ?? 1);

             return View(Task.FromResult(tickers.ToPagedList(pageNumber, pageSize)).Result); 
         }
         private async Task LoadInit()
         {

             var marketType = new List<dynamic>
             {
                  new { Id = "-1",Name = string.Empty  },
                  new { Id = "0",Name = "VNI"  },
                  new { Id = "1",Name = "HASTC Index" },
                  new { Id = "2",Name = "OTC Index"},
                  new { Id = "3" ,Name = "UPCOM Index"},                   
             };
             ViewBag.marketTypeList = new SelectList(marketType, "Id", "Name");

         }
	}
}