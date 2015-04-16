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

         public async Task<ActionResult> Add() // Tạo mã nóng
         {
             LoadInit();
             return View();
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<ActionResult> Add(StockCodeModel stockcode) // Tạo mã nóng
         {
             if (ModelState.IsValid)
             {

                 if (dbcungphim.StockCodes.Any(t => t.Code == stockcode.Code))
                 {
                     ModelState.AddModelError("", "Đã tồn tại mã cổ phiếu này trong thệ thống");
                 }
                 else
                 {
                     dbcungphim.StockCodes.Add(new StockCode { Code = stockcode.Code.ToUpper(), IndexName});
                     await dbcungphim.SaveChangesAsync();
                     return RedirectToAction("Index");
                 }


             }
             return View(stockcode);
         }

         public async Task<ActionResult> Detail(int tickerid) // list user
         {
             var hotTicker = await dbcungphim.TickerHots.FindAsync(tickerid);
             return View(hotTicker);
         }
         [HttpPost, ActionName("Detail")]
         [ValidateAntiForgeryToken]
         public async Task<ActionResult> Detail(int tickerid, string stockCode) // list user
         {
             var hotTicker = await dbcungphim.TickerHots.FindAsync(tickerid);
             if (hotTicker != null)
             {
                 if (hotTicker.THName.ToUpper() != stockCode.ToUpper())
                 {
                     hotTicker.THName = stockCode;
                     try
                     {
                         dbcungphim.Entry(hotTicker).State = EntityState.Modified;
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

         public async Task<ActionResult> Delete(int? tickerid) // list user
         {
             var hotTicker = await dbcungphim.TickerHots.FindAsync(tickerid);
             return View(hotTicker);
         }
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<ActionResult> Delete(int tickerid) // list user
         {
             var hotTicker = await dbcungphim.TickerHots.FindAsync(tickerid);
             if (hotTicker != null)
             {
                 try
                 {
                     dbcungphim.TickerHots.Remove(hotTicker);
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
}