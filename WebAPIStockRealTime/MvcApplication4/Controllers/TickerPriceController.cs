using PorfolioInvesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PorfolioInvesment.Controllers
{
    public class TickerPriceController : Controller
    {
        //
        // GET: /TickerPrice/

        private Target_Price_StockChart_Hieu db = new Target_Price_StockChart_Hieu();
        [HttpGet]        
        public JsonResult GetStockPriChart(string chart)
        {
            HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            var ret = (from sp in db.StockPrices
                       orderby sp.TradingDate
                       where sp.Code == chart
                       select new
                       {
                           t = sp.TradingDate,
                           o = sp.OpenPrice,
                           h = sp.HighPrice,
                           l = sp.LowPrice,
                           c = sp.ClosePrice,
                           s = sp.Totalshare
                       }).ToList();
            //var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            //return result;
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

    }
}
