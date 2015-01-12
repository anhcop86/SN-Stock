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

namespace PhimHang.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private KNDTLocalConnection db = new KNDTLocalConnection();

        public async Task<ActionResult> Index()
        {
            //return RedirectToAction("Login", "Account");
            
                LoadInit();
                var recommendstocks = db.RecommendStocks.Include(r => r.UserLogin);
                return View(await recommendstocks.ToListAsync());
            
            //return View();
        }

        private void LoadInit()
        {
            
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