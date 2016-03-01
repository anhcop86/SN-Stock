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
using System.Threading;

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
                return RedirectToAction("", "Search", new { q = username });
            }
            #region thong tin user
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser userLogin = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                ViewBag.AvataEmage = string.IsNullOrEmpty(userLogin.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + userLogin.UserExtentLogin.AvataImage;
                ViewBag.CureentUserId = userLogin.UserExtentLogin.Id;
                ViewBag.UserName = userLogin.UserName;
                ViewBag.AvataImageUrl = string.IsNullOrEmpty(userLogin.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + userLogin.UserExtentLogin.AvataImage;
                ViewBag.CharacterLimit = currentUser.CharacterLimit;
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
            ViewBag.StatusShare = currentUser.Status;
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
            
            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion

            #region danh muc co phieu nong
            var listHotStock = await AppHelper.GetListHotStock();
            ViewBag.ListStockHot = listHotStock;
            #endregion
            return View(currentUser);

        }
        public async Task<ActionResult> Tickers(string username)
        {
            ViewBag.AbsolutePathHostName = AbsolutePathHostName;
            var currentUser = await db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username); //db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username);

            if (currentUser == null || string.IsNullOrEmpty(username))
            {
                return RedirectToAction("", "Search", new { q = username });
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
            #region load danh muc dau tu
            var followStockList = await (from sl in db.FollowStocks
                                         where sl.UserId == currentUser.Id
                                         select sl.StockFollowed).ToListAsync();

            //ViewBag.FollowStockList = followStockList.Result;
            // gia cổ phieu cua cac ma dang theo doi
            ViewBag.listStockPriceFollow = _stockRealtime.GetAllStocksList(followStockList as List<string>).Result;

            #endregion

            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion

            #region danh muc co phieu nong
            var listHotStock = await AppHelper.GetListHotStock();
            ViewBag.ListStockHot = listHotStock;
            #endregion

            return View(currentUser);
        }
        public async Task<ActionResult> Followers(string username)
        {
            ViewBag.AbsolutePathHostName = AbsolutePathHostName;
            var currentUser = await db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username); //db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username);

            if (currentUser == null || string.IsNullOrEmpty(username))
            {
                return RedirectToAction("", "Search", new { q = username });
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
            #region theo doi
            // load danh muc theo doi            
            var followList = await (from fl in db.FollowUsers
                                    where fl.UserId == currentUser.Id
                                    orderby fl.UserLogin1.UserNameCopy ascending
                                    select new UserFollowView
                                      {
                                          UserId = fl.UserLogin1.Id,
                                          UserName = fl.UserLogin1.UserNameCopy,
                                          Avata = string.IsNullOrEmpty(fl.UserLogin1.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + fl.UserLogin1.AvataImage,
                                          Status = fl.UserLogin1.Status,
                                          BrkVip = fl.UserLogin1.BrokerVIP
                                      }).ToListAsync();

            ViewBag.FollowList = followList;
            #endregion

            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion

            #region danh muc co phieu nong
            var listHotStock = await AppHelper.GetListHotStock();
            ViewBag.ListStockHot = listHotStock;
            #endregion

            return View(currentUser);
        }
        public async Task<ActionResult> Following(string username)
        {
            ViewBag.AbsolutePathHostName = AbsolutePathHostName;
            var currentUser = await db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username); //db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username);

            if (currentUser == null || string.IsNullOrEmpty(username))
            {
                return RedirectToAction("", "Search", new { q = username });
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

            #region dang theo doi
            var followerList = await (from fl in db.FollowUsers
                                      where fl.UserIdFollowed == currentUser.Id
                                      orderby fl.UserLogin.UserNameCopy
                                      select new UserFollowView
                                      {
                                          UserId = fl.UserLogin.Id,
                                          UserName = fl.UserLogin.UserNameCopy,
                                          Status = fl.UserLogin.Status,
                                          Avata = string.IsNullOrEmpty(fl.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + fl.UserLogin.AvataImage,
                                          BrkVip = fl.UserLogin.BrokerVIP
                                      }).ToListAsync();

            ViewBag.FollowerList = followerList;
            #endregion


            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
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
                var ret = await (from posts in db.Posts
                           orderby posts.PostedDate descending
                           where posts.PostedBy == userid
                           select new
                           {
                               Message = posts.Message,
                               Chart = posts.ChartImageURL,               
                               PostedByName = posts.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(posts.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + posts.UserLogin.AvataImage ,
                               PostedDate = posts.PostedDate,
                               PostId = posts.PostId,
                               StockPrimary = posts.StockPrimary,
                               Stm = posts.NhanDinh,
                               ChartYN = posts.ChartYN,
                               SumLike = posts.SumLike,
                               SumReply = posts.SumReply,
                               BrkVip = posts.UserLogin.BrokerVIP
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "CHA")
            {
                var ret = await (from posts in db.Posts
                           orderby posts.PostedDate descending
                           where posts.PostedBy == userid &&  posts.ChartYN == true
                           select new
                           {
                               Message = posts.Message,
                               Chart = posts.ChartImageURL,               
                               PostedByName = posts.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(posts.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + posts.UserLogin.AvataImage,
                               PostedDate = posts.PostedDate,
                               PostId = posts.PostId,
                               StockPrimary = posts.StockPrimary,
                               Stm = posts.NhanDinh,
                               ChartYN = posts.ChartYN,
                               SumLike = posts.SumLike,
                               SumReply = posts.SumReply,
                               BrkVip = posts.UserLogin.BrokerVIP
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "STM")
            {
                var ret = await (from posts in db.Posts
                           orderby posts.PostedDate descending
                           where posts.PostedBy == userid && posts.NhanDinh > 0
                           select new
                           {
                               Message =  posts.Message,
                               Chart = posts.ChartImageURL,               
                               PostedByName = posts.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(posts.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + posts.UserLogin.AvataImage,
                               PostedDate = posts.PostedDate,
                               PostId = posts.PostId,
                               StockPrimary = posts.StockPrimary,
                               Stm = posts.NhanDinh,
                               ChartYN = posts.ChartYN,
                               SumLike = posts.SumLike,
                               SumReply = posts.SumReply,
                               BrkVip = posts.UserLogin.BrokerVIP
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
        public async Task<ActionResult> RandomProBroker(int numberBroker)
        {
            //Thread.Sleep(2000);
            #region random dan phim chuyen nghiem
            var DanPhimRandom = await (from u in db.UserLogins
                                       orderby Guid.NewGuid()
                                       where u.BrokerVIP == true
                                       select new UserRandom
                                       {
                                           Avata = string.IsNullOrEmpty(u.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + u.AvataImage,
                                           UserName = u.UserNameCopy,
                                           FullName = u.FullName
                                       }).Take(numberBroker).ToListAsync();            
            #endregion
            return PartialView("_Partial_Area_Right_User1", DanPhimRandom);
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

        public bool? BrkVip { get; set; }
    }

    public class UserRandom
    {
        public string UserName { get; set; }
        public string Avata { get; set; }

        public string FullName { get; set; }
    }
}
