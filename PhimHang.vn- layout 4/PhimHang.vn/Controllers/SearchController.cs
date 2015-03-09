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
        private testEntities db = new testEntities();
        //
        // GET: /Search/
        public async Task<ActionResult> Index(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return RedirectToAction("Helper");
            }
            else
            {
                //var search = db.StockCodes.FirstOrDefault(s => s.Code.StartsWith(q.Replace("$", ""))).Code;
                var searstring = q.Replace("$", "").Replace("@", "").Trim();
                var searchStockList = await (from sc in db.StockCodes
                                             where sc.Code.Contains(searstring)
                                       && (sc.MarketType == 0 || sc.MarketType == 1)
                                       select sc.Code).ToListAsync();
                var searchUser = await  (from us in db.UserLogins
                                         where us.UserNameCopy.Contains(searstring) 
                                  select us.UserNameCopy).ToListAsync();
                if (searchStockList.Count == 1) // đủ 3 ký tự và tìm thấy
                {
                    return RedirectToAction("/" + searchStockList[0], "Ticker");
                }
                else if (searchUser.Count == 1) // 
                {
                    return RedirectToAction("/" + searchUser[0] + "/tab/1", "user");
                }
                else
                {
                    return RedirectToAction("Helper");
                }


            }

        }
        public ActionResult Helper()
        {
            return View();
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