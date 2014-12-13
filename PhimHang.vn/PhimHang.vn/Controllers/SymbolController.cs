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
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public SymbolController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            _stockRealtime = stockTicker;
            UserManager = userManager;
        }
       
        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "../img/avatar_default.jpg";
        private const string ImageURLCoverDefault = "../img/cover_default.jpg";
        private const string ImageURLAvata = "../images/avatar/";
        private const string ImageURLCover = "../images/cover/";
        public async Task<ViewResult> Index(string symbolName)
        {
            var company = new StockCode();
            ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            #region danh muc co phieu dang follow
            // danh muc co phieu dang follow

            Session["listStock"] = new List<string>();
            using (db = new testEntities())
            {
                if (User.Identity.IsAuthenticated)
                {
                    Session["listStock"] = (from s in db.FollowStocks
                                            where s.UserId == currentUser.UserExtentLogin.Id
                                            select s.StockFollowed).ToList();
                }

                company = await db.StockCodes.FirstOrDefaultAsync(m => m.Code == symbolName);



            #endregion

            #region Thong tin menu ben trai
                // Thong tin menu ben trai
                var post = await db.Posts.CountAsync(p => p.PostedBy == currentUser.UserExtentLogin.Id);
                var follow = await db.FollowUsers.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id);
                var follower = await db.FollowUsers.CountAsync(f => f.UserIdFollowed == currentUser.UserExtentLogin.Id);


                ViewBag.TotalPost = post;
                ViewBag.Follow = follow;
                ViewBag.Follower = follower;
                string path = "../";
                ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.AvataCover) == true ? path + ImageURLCoverDefault : path + ImageURLCover + currentUser.AvataCover;
                ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.AvataImage) == true ? path + ImageURLAvataDefault : path + ImageURLAvata + currentUser.AvataImage;

                // End thong tin menu ben trai

            }
            #endregion

            #region thong tin co phieu
            ViewBag.StockCode = company == null ? StatusSymbol.NF.ToString() : symbolName;
            ViewBag.StockName = company == null ? StatusSymbol.NF.ToString() : company.ShortName;
            #endregion

            #region danh muc co phieu vua moi xem duoc luu troong cookie
            //var cookie = new HttpCookie("cookiename");

            if (Request.Cookies["HotStockCookie"] == null || string.IsNullOrEmpty(Request.Cookies["HotStockCookie"].Value))
            {
                Response.Cookies["HotStockCookie"].Value = "|" + symbolName;
            }
            else
            {
                Response.Cookies["HotStockCookie"].Value = Request.Cookies["HotStockCookie"].Value.Replace("|" + symbolName, "") + "|" + symbolName;
            }
            var listHotStock = Response.Cookies["HotStockCookie"].Value.Split('|');
            if (listHotStock.Length >10)
            {
                //Response.Cookies["HotStockCookie"].Value.Substring("","");
            }
            #endregion
            //return View(_stockRealtime.GetAllStocksTestList((List<string>)Session["listStock"]).Result);
            return View(currentUser);
        }

        //[Authorize]
        

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
