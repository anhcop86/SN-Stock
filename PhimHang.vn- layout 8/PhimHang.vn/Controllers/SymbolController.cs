//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using PhimHang.Models;
//using System.Data.Entity;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;

//namespace PhimHang.Controllers
//{

//    [Authorize]
//    public class SymbolController : Controller
//    {
//        //
//        // GET: /Symbol/
//        //private readonly StockRealTimeTicker _stockRealtime;
//        public SymbolController()
//            : this( new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
//        {
//        }
//        public SymbolController( UserManager<ApplicationUser> userManager)
//        {
//            //_stockRealtime = stockTicker;
//            UserManager = userManager;
//        }
       
//        public UserManager<ApplicationUser> UserManager { get; private set; }

//        private testEntities db = new testEntities();
//        private const string ImageURLAvataDefault = "img/avatar_default.jpg";
//        private const string ImageURLCoverDefault = "img/cover_default.jpg";
//        private const string ImageURLAvata = "images/avatar/";
//        private const string ImageURLCover = "images/cover/";

        
//        public async Task<ViewResult> Index(string symbolName)
//        {
//            using (db = new testEntities())
//            {
//                var company = new StockCode();
//                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

//                #region danh muc co phieu dang follow
               

//                #endregion

//                #region Thong tin menu ben trai
//                // Thong tin menu ben trai
//                var post = await db.Posts.CountAsync(p => p.PostedBy == currentUser.UserExtentLogin.Id);
//                var follow = await db.FollowUsers.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id);
//                var follower = await db.FollowUsers.CountAsync(f => f.UserIdFollowed == currentUser.UserExtentLogin.Id);
                
//                var countStockFollowr = await db.FollowStocks.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id && f.StockFollowed == symbolName);
//                if (countStockFollowr==1)
//                {
//                    ViewBag.CheckStockExist = "Y";
//                }
//                else
//                {
//                    ViewBag.CheckStockExist = "N";
//                }
//                ViewBag.TotalPost = post;
//                ViewBag.Follow = follow;
//                ViewBag.Follower = follower;

//                ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataCover) == true ? ImageURLCoverDefault : ImageURLCover + currentUser.UserExtentLogin.AvataCover;
//                ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.UserExtentLogin.AvataImage;

//                // End thong tin menu ben trai


//                #endregion

//                #region thong tin co phieu
//                company = await db.StockCodes.FirstOrDefaultAsync(m => m.Code.ToUpper() == symbolName.ToUpper());
//                ViewBag.StockCode = company == null ? StatusSymbol.NF.ToString() : symbolName.ToUpper();
//                ViewBag.StockName = company == null ? StatusSymbol.NF.ToString() : company.ShortName;
//                ViewBag.LongName = company == null ? StatusSymbol.NF.ToString() : company.LongName;
//                ViewBag.MarketName = company == null ? StatusSymbol.NF.ToString() : company.IndexName;
//                ViewBag.CureentUserId = currentUser.UserExtentLogin.Id;
//                ViewBag.UserName = currentUser.UserName;
//                ViewBag.AvataImageUrl = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.UserExtentLogin.AvataImage + "?width=46&height=46&mode=crop"; 
//                #endregion
                
//                #region danh muc co phieu vua moi xem duoc luu troong cookie
//                /*
//                //var cookie = new HttpCookie("cookiename");

//                if (Request.Cookies["HotStockCookie" + currentUser.Id] == null)
//                {
//                    Response.Cookies.Clear();
//                    Response.Cookies["HotStockCookie" + currentUser.Id].Value = "";
//                    Response.Cookies["HotStockCookie" + currentUser.Id].Expires = DateTime.Now.AddDays(1);
//                }
//                else
//                {
//                    Response.Cookies["HotStockCookie" + currentUser.Id].Value = Request.Cookies["HotStockCookie" + currentUser.Id].Value.Replace("|" + symbolName, "") + "|" + symbolName;
//                }

//                string[] listHotStock = Response.Cookies["HotStockCookie" + currentUser.Id].Value.Split('|');

//                if (listHotStock.Length > 11) // neu xem hon 10 co phieu thi chi hien thi 10 co phieu dau tien
//                {
//                    Response.Cookies["HotStockCookie" + currentUser.Id].Value = Request.Cookies["HotStockCookie" + currentUser.Id].Value.Remove(0, Request.Cookies["HotStockCookie" + currentUser.Id].Value.IndexOf("|", 1)); // hien thi 10 co phieu đau tien
//                    listHotStock = Request.Cookies["HotStockCookie" + currentUser.Id].Value.Split('|');
//                }
//                List<string> listHotStockToArray = new List<string>();
//                foreach (var item in listHotStock)
//                {
//                    listHotStockToArray.Add(item);
//                }
//                //var hotStockPrice = _stockRealtime.GetAllStocksTestList(listHotStockToArray).Result;

//                ViewBag.HotStockPriceList = listHotStockToArray;//hotStockPrice.Count() == 0 ? new List<StockRealTime>() : hotStockPrice;
//               */
//                 #endregion
                
//                #region danh muc dau tu
//                var followstocks = await db.FollowStocks.Where(f => f.UserId == currentUser.UserExtentLogin.Id).ToListAsync();
//                var listfollowstocksString = (from sf in followstocks
//                                              select sf.StockFollowed).ToList();
//                var DMDTShortName = (from fs in db.FollowStocks.ToList()
//                                     join s in db.StockCodes.ToList() on fs.StockFollowed equals s.Code
//                                     where listfollowstocksString.Contains(fs.StockFollowed)
//                                     select new StockDetail
//                                     {
//                                         Stock = fs.StockFollowed,
//                                         ShortName = s.ShortName
//                                     }).ToList();
//                ViewBag.HotStockDMDT = DMDTShortName;//_stockRealtime.GetAllStocksTestList(listfollowstocksString).Result;
//                #endregion

//                //return View(_stockRealtime.GetAllStocksTestList((List<string>)Session["listStock"]).Result);
//                return View(currentUser);
//            }
//        }

        
//        // GET: /Symbol/Details/5
//        //public async Task<ActionResult> Details(long postid, string stock)
//        //{            
//        //    using (db = new testEntities())
//        //    {
//        //        ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

//        //        var post = await db.Posts.Include(p => p.UserLogin).FirstOrDefaultAsync(p => p.PostId == postid);
//        //        ViewBag.AvataEmagebyPost = string.IsNullOrEmpty(post.UserLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + post.UserLogin.AvataImage;
//        //        ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.UserExtentLogin.AvataImage;


//        //        return View(post);
//        //    }
            
//        //}

//        //
//        // GET: /Symbol/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        //
//        // POST: /Symbol/Create
//        [HttpPost]
//        public ActionResult Create(FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        //
//        // GET: /Symbol/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        //
//        // POST: /Symbol/Edit/5
//        [HttpPost]
//        public ActionResult Edit(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add update logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        //
//        // GET: /Symbol/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        //
//        // POST: /Symbol/Delete/5
//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        #region Extent
//        public enum StatusSymbol
//        {
//            NF
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && UserManager != null)
//            {
//                UserManager.Dispose();
//                UserManager = null;
//            }
//            base.Dispose(disposing);
//        }

//        #endregion
//    }
//}
