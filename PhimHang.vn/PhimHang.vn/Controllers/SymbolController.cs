using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhimHang.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhimHang.Controllers
{

    
    public class SymbolController : Controller
    {
        //
        // GET: /Symbol/
        private readonly StockRealTimeTicker _stockRealtime;
        public SymbolController()
            : this(StockRealTimeTicker.Instance)
        {
        }
        public SymbolController(StockRealTimeTicker stockTicker)
        {
            _stockRealtime = stockTicker;
        }
        public SymbolController(string name)
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {

        }
        public SymbolController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db;
        public async Task<ActionResult> Index(string symbolName)
        {
            var company = new StockCode();
            using (db = new testEntities())
            {
                company = await db.StockCodes.FirstOrDefaultAsync(m => m.Code == symbolName);    
            }
            
            if(company !=null)
            {
                ViewBag.StockCode = symbolName;
                ViewBag.StockName = company.ShortName;
            }
            else
            {
                ViewBag.StockCode = StatusSymbol.NF;
                ViewBag.StockName = StatusSymbol.NF;
            }
            
            return View();
        }

        //[Authorize]
        public async Task<List<StockRealTime>> GetListStockPriceFollowForUser(List<string> stock)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "Account");
            }
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var listStock = new List<string>();
            using (db = new testEntities())
            {
                listStock = (from s in db.FollowStocks
                                where s.UserId == currentUser.UserExtentLogin.Id
                                select s.StockFollowed).ToList();
            }
            //stock.Add("KLS");
            //stock.Add("HAG");
            return await _stockRealtime.GetAllStocksTestList(listStock);
        }

        //
        // GET: /Symbol/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Symbol/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Symbol/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Symbol/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Symbol/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Symbol/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Symbol/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Extent
        public enum StatusSymbol
        {
            NF
        }

        
        #endregion
    }
}
