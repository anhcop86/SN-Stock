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
    //[Authorize] // xoa khi public
    public class UserController : Controller
    {
        //
        // GET: /User/
        private readonly StockRealTimeTicker _stockRealtime;
        public UserController()
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public UserController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            _stockRealtime = stockTicker;
            UserManager = userManager;
        }
       
        public UserManager<ApplicationUser> UserManager { get; private set; }
        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "/img/avatar2.jpg";        
        private const string ImageURLAvata = "/images/avatar/";
        private const string ImageURLCoverDefault = "/img/cover_default.jpg";
        private const string ImageURLCover = "/images/cover/";
        private string AbsolutePathHostName = AppHelper.AbsolutePathHostName;
        public async Task<ActionResult> Index(string username)
        {
            ViewBag.AbsolutePathHostName = AbsolutePathHostName;
            var currentUser = await db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username); //db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username);

            if (currentUser == null || string.IsNullOrEmpty(username))
            {
                return RedirectToAction("", "Search");
            }
            #region thong tin user
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser userLogin = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                ViewBag.AvataEmage = string.IsNullOrEmpty(userLogin.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + userLogin.UserExtentLogin.AvataImage;
                ViewBag.CureentUserId = userLogin.Id;
                ViewBag.UserName = userLogin.UserName;
                ViewBag.AvataImageUrl = string.IsNullOrEmpty(userLogin.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + userLogin.UserExtentLogin.AvataImage;

                #region follow user
                if (userLogin.UserExtentLogin.Id == currentUser.Id)
                {
                    ViewBag.CheckUserExist = "E";
                }
                else
                {
                    var checkUser = await db.FollowUsers.CountAsync(f => f.UserId == userLogin.UserExtentLogin.Id && f.UserIdFollowed == currentUser.Id);
                    if (checkUser == 1)
                    {
                        ViewBag.CheckUserExist = "Y";
                    }
                    else
                    {
                        ViewBag.CheckUserExist = "N";
                    }

                }
                // so luong tin nhan
                var numberMessegeNew = await db.NotificationMesseges.Where(nm => nm.UserReciver == userLogin.UserExtentLogin.Id && nm.NumNoti > 0).SumAsync(mn => mn.NumNoti);
                ViewBag.NewMessege = numberMessegeNew;
                #endregion
            }
            else
            {
                ViewBag.AvataEmage = ImageURLAvataDefault;
            }
            ViewBag.CurrentPositionImage = currentUser.CoverPosition;
            ViewBag.UserName = username;
            ViewBag.AvataImageUrlCurrent = string.IsNullOrEmpty(currentUser.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.AvataImage;
            ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.AvataCover) == true ? ImageURLCoverDefault : ImageURLCover + currentUser.AvataCover;
            ViewBag.UserId = currentUser.Id;
            var post = await db.Posts.CountAsync(p => p.PostedBy == currentUser.Id);
            var follow = await db.FollowUsers.CountAsync(f => f.UserId == currentUser.Id);
            var follower = await db.FollowUsers.CountAsync(f => f.UserIdFollowed == currentUser.Id);
            var followStock = await db.FollowStocks.CountAsync(f => f.UserId == currentUser.Id);

            ViewBag.TotalPost = post;
            ViewBag.Follow = follow;
            ViewBag.Follower = follower;
            ViewBag.followStock = followStock;
            #endregion

            //ViewBag.TabId = tabid;
            
            #region tab danh muc dau tu
            //if (tabid == 2)
            //{
            //    // load danh muc dau tu
            //    var followStockList = await (from sl in db.FollowStocks
            //                           where sl.UserId == currentUser.Id
            //                           select sl.StockFollowed).ToListAsync();

            //    //ViewBag.FollowStockList = followStockList.Result;
            //    #region gia cổ phieu cua cac ma dang theo doi
            //    ViewBag.listStockPriceFollow = _stockRealtime.GetAllStocksList(followStockList as List<string>).Result;
            //    #endregion
            //}

            //#endregion
            //#region tab theo doi
            //else if (tabid == 3)
            //{
            //    // load danh muc theo doi
            //    //var follow = await db.FollowUsers.CountAsync(f => f.UserId == currentUser.Id);
            //    var followList = await (from fl in db.FollowUsers
            //                      where fl.UserId == currentUser.Id
            //                      select new UserFollowView
            //                        {
            //                            UserId = fl.UserLogin1.Id,
            //                            UserName = fl.UserLogin1.UserNameCopy,
            //                            Avata = string.IsNullOrEmpty(fl.UserLogin1.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + fl.UserLogin1.AvataImage,
            //                            Status = fl.UserLogin1.Status
            //                        }).ToListAsync();

            //    ViewBag.FollowList = followList;

            //}
            //#endregion
            //#region tab duoc theo doi
            //else if (tabid == 4)
            //{
            //    // load danh muc duoc theo doi
            //    var followerList = await (from fl in db.FollowUsers
            //                        where fl.UserIdFollowed == currentUser.Id
            //                        select new UserFollowView
            //                        {
            //                            UserId = fl.UserLogin.Id,
            //                            UserName = fl.UserLogin.UserNameCopy,
            //                            Status = fl.UserLogin.Status,
            //                            Avata = string.IsNullOrEmpty(fl.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + fl.UserLogin.AvataImage
            //                        }).ToListAsync();

            //    ViewBag.FollowerList = followerList;
            //}
            #endregion
            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion

            #region random dan phim chuyen nghiem
            var DanPhimRandom = await (from u in db.UserLogins
                                       orderby Guid.NewGuid()
                                       where u.BrokerVIP == true
                                       select new UserRandom
                                      {
                                           Avata = string.IsNullOrEmpty(u.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + u.AvataImage,
                                           UserName = u.UserNameCopy,
                                           FullName = u.FullName
                                      }).Take(4).ToListAsync();
            ViewBag.DanPhimRandom = DanPhimRandom;
            #endregion
            #region danh muc co phieu nong
            var listHotStock = await AppHelper.GetListHotStock();
            ViewBag.ListStockHot = listHotStock;
            #endregion
            return View(currentUser);


        }
       
        public async Task<dynamic> GetPostMoreByUserId(int userid, int skipposition, string filter)
        {
            if (filter == "" || filter == "ALL")
            {
                var ret = await (from stockRelate in db.Posts
                           orderby stockRelate.PostedDate descending
                           where stockRelate.PostedBy == userid
                           select new
                           {
                               Message = stockRelate.Message,
                               Chart = stockRelate.ChartImageURL,               
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.UserLogin.AvataImage ,
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN,
                               SumLike = stockRelate.SumLike,
                               SumReply = stockRelate.SumReply
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "CHA")
            {
                var ret = await (from stockRelate in db.Posts
                           orderby stockRelate.PostedDate descending
                           where stockRelate.PostedBy == userid &&  stockRelate.ChartYN == true
                           select new
                           {
                               Message = stockRelate.Message,
                               Chart = stockRelate.ChartImageURL,               
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.UserLogin.AvataImage,
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN,
                               SumLike = stockRelate.SumLike,
                               SumReply = stockRelate.SumReply
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "STM")
            {
                var ret = await (from stockRelate in db.Posts
                           orderby stockRelate.PostedDate descending
                           where stockRelate.PostedBy == userid && stockRelate.NhanDinh > 0
                           select new
                           {
                               Message =  stockRelate.Message,
                               Chart = stockRelate.ChartImageURL,               
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.UserLogin.AvataImage,
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN,
                               SumLike = stockRelate.SumLike,
                               SumReply = stockRelate.SumReply
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            else
            {
                return null;
            }


        }
        //
        // GET: /User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
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
        // GET: /User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5
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
        // GET: /User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5
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
    }
    public class UserFollowView
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Avata { get; set; }
        public string Status { get; set; }
    }

    public class UserRandom
    {
        public string UserName { get; set; }
        public string Avata { get; set; }

        public string FullName { get; set; }
    }
}
