using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class BrokerController : Controller
    {
        private KNDTLocalConnection db = new KNDTLocalConnection();        
        //
        // GET: /Broker/
        public ActionResult Index(int? page, int? postBy)
        {
            if (postBy == null)
            {
                postBy = 0;
            }
            ViewBag.postBy = postBy;
            LoadInit();
            var recommendstocks = from r in db.RecommendStocks.Include(r => r.UserLogin)
                                  orderby r.CreatedDate descending
                                  where (r.PostBy == postBy || 0 == postBy)
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


            ViewBag.listUserId = new SelectList(db.UserLogins, "Id", "UserNameCopy");
      

        }
	}
}