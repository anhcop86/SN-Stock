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
using System.ComponentModel.DataAnnotations;

namespace PhimHang.Controllers
{
    [Authorize]
    public class TickerHotController : Controller
    {
        //
        // GET: /TickerHot/
        private db_cungphim_FrontEnd dbcungphim = new db_cungphim_FrontEnd();
        public async Task<ActionResult> Index()
        {
            var hotTickers = await dbcungphim.TickerHots.ToListAsync();
            return View(hotTickers);
        }

        public async Task<ActionResult> Add() // Tạo mã nóng
        {     
       
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(TickerHotModel tickerHot) // Tạo mã nóng
        {
            if (ModelState.IsValid)
            {
                using (dbcungphim = new db_cungphim_FrontEnd())
                {
                    if (dbcungphim.TickerHots.Any(t => t.THName.ToUpper() == tickerHot.THName.ToUpper()))
                    {
                        ModelState.AddModelError("", "Đã tồn tại mã cổ phiếu này trong thệ thống");
                    }
                    else
                    {
                        dbcungphim.TickerHots.Add(new TickerHot { THName = tickerHot.THName.ToUpper() });
                        await dbcungphim.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                

            }
            return View(tickerHot);
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
            using (dbcungphim = new db_cungphim_FrontEnd())
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
            using (dbcungphim = new db_cungphim_FrontEnd())
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
            }
            return RedirectToAction("");
        }
    }
    
}