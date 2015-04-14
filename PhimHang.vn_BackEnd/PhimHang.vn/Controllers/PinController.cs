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
    public class PinController : Controller
    {
        //
        // GET: /Pin/
        private db_cungphim_FrontEnd dbcungphim = new db_cungphim_FrontEnd();
        public async Task<ActionResult> Index(int? page, string stockName) // list user
        {
            ViewBag.linkAbsolutePath = Request.Url.PathAndQuery;
            if (string.IsNullOrWhiteSpace(stockName))
            {
                stockName = "ALL";
            }
            ViewBag.stockName = stockName;

            var pins = from p in dbcungphim.PinStocks
                       orderby p.CreatedDate ascending
                       where (p.StockCodePin.Contains(stockName) || "ALL" == stockName)
                       select new PinModel
                        {
                            CreateDate = p.CreatedDate,
                            Id = p.ID,
                            PostContain = p.Post.Message,
                            StockName = p.StockCodePin

                        };


            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(Task.FromResult(pins.ToPagedList(pageNumber, pageSize)).Result);
        }

        public async Task<ActionResult> Detail(long? pinid) // list user
        {
            var pin = await dbcungphim.PinStocks.FindAsync(pinid);
            return View(pin);
        }
        [HttpPost, ActionName("Detail")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Detail(long pinid, string stockCode) // list user
        {
            var pin = await dbcungphim.PinStocks.FindAsync(pinid);
            if (pin != null)
            {
                if (pin.StockCodePin.ToUpper() != stockCode.ToUpper())
                {
                    pin.StockCodePin = stockCode;
                    try
                    {
                        dbcungphim.Entry(pin).State = EntityState.Modified;
                        await dbcungphim.SaveChangesAsync();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            return RedirectToAction("");
        }
        public async Task<ActionResult> Delete(long? pinid)// list user
        {
            var pin = await dbcungphim.PinStocks.FindAsync(pinid);
            return View(pin);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long pinid) // list user
        {
            var pin = await dbcungphim.PinStocks.FindAsync(pinid);
            if (pin != null)
            {
                try
                {
                    dbcungphim.PinStocks.Remove(pin);
                    await dbcungphim.SaveChangesAsync();
                }
                catch (Exception)
                {

                    //throw;
                }

            }
            return RedirectToAction("");
        }


	}
    public class PinModel
    {
        public long Id { get; set; }

        public string StockName { get; set; }

        public string PostContain { get; set; }

        public DateTime CreateDate { get; set; }
    }
}