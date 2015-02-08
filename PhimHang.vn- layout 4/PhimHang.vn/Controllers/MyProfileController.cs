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

    [Authorize]
    public class MyProfileController : Controller
    {
        //
        // GET: /MyProfile/
        private readonly FilterKeyworkSingleton _keyword;
        public MyProfileController()
            : this(FilterKeyworkSingleton.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public MyProfileController(FilterKeyworkSingleton KeyworkSing, UserManager<ApplicationUser> userManager)
        {
            _keyword = KeyworkSing;
            UserManager = userManager;
        }


        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "/img/avatar2.jpg"; 
        private const string ImageURLCoverDefault = "/img/cover_default.jpg";
        private const string ImageURLAvata = "/images/avatar/";
        private const string ImageURLCover = "images/cover/";

        public async Task<ActionResult> Index()
        {
            // get user info

            var company = new StockCode();
            ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            #region danh muc co phieu dang follow


            #endregion

            #region Thong tin menu ben trai
             //Thong tin menu ben trai
            var post = await db.Posts.CountAsync(p => p.PostedBy == currentUser.UserExtentLogin.Id);
            var follow = await db.FollowUsers.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id);
            var follower = await db.FollowUsers.CountAsync(f => f.UserIdFollowed == currentUser.UserExtentLogin.Id);

            //var countStockFollowr = await db.FollowStocks.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id && f.StockFollowed == symbolName);
            //if (countStockFollowr == 1)
            //{
            //    ViewBag.CheckStockExist = "Y";
            //}
            //else
            //{
            //    ViewBag.CheckStockExist = "N";
            //}
            ViewBag.TotalPost = post;
            ViewBag.Follow = follow;
            ViewBag.Follower = follower;           
                        
            ViewBag.CureentUserId = currentUser.UserExtentLogin.Id;
            ViewBag.UserName = currentUser.UserName;

            ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : "/images/avatar/" + currentUser.UserExtentLogin.AvataImage;
            ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataCover) == true ? ImageURLCoverDefault : "/images/cover/" + currentUser.UserExtentLogin.AvataCover;
            ViewBag.AvataImageUrl = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + currentUser.UserExtentLogin.AvataImage + "?width=50&height=50&mode=crop";
            
            // cac post duoc loc tu danh muc nguoi theo doi => dc load o duoi client san
            var listPersonFollow = (from userFollow in db.FollowUsers.ToList()
                                    where userFollow.UserId == currentUser.UserExtentLogin.Id
                                    select userFollow.UserIdFollowed
                                 ).ToArray();

            ViewBag.ListFollow = listPersonFollow as int[]; //client

            // cac post dc loc tu danh muc dau tu => dc load o duoc client san
            var listStock = (from followStock in db.FollowStocks.ToList()
                             orderby followStock.StockFollowed ascending
                             where followStock.UserId == currentUser.UserExtentLogin.Id
                             select followStock.StockFollowed
                                ).ToList();

            ViewBag.listStockFollow = listStock as List<string>; // client
            // End thong tin menu ben trai


            #endregion

            return View(currentUser);

        }
        [HttpGet]
        public async Task<dynamic> GetPostsGlobal()
        {
            //var fjdsf = WebSecurity.CurrentUserId;
            using (db = new testEntities())
            {
                var ret = (from stockRelate in await db.Posts.ToListAsync()                           
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
        }


        public async Task<dynamic> GetPostsGlobalByFilter(string filter)
        {
            //var fjdsf = WebSecurity.CurrentUserId;
            if (filter == "" || filter == "ALL")
            {
                var ret = (from stockRelate in await db.Posts.ToListAsync()                           
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "CHA")
            {
                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where stockRelate.ChartYN == true
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "STM")
            {
                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where stockRelate.NhanDinh > 0
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "DIS")
            {
                 var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where stockRelate.NhanDinh !=1 && stockRelate.NhanDinh !=2  && stockRelate.ChartYN != true
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "VIP")
            {
                var ret = (from stockRelate in await db.PinStocks.ToListAsync()
                           orderby stockRelate.Post.PostedDate descending
                           select new
                           {
                               Message = stockRelate.Post.ChartYN == true ? stockRelate.Post.Message + "<br/><img src='" + stockRelate.Post.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Post.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.Post.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.Post.StockPrimary,
                               Stm = stockRelate.Post.NhanDinh,
                               ChartYN = stockRelate.Post.ChartYN
                           }).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "PEF")
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var listPersonFollow = (from userFollow in db.FollowUsers.ToList()
                                        where userFollow.UserId == currentUser.UserExtentLogin.Id
                                        select userFollow.UserIdFollowed
                                 ).ToList();
                                    

                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where listPersonFollow.Contains(stockRelate.PostedBy)                       
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Take(10).ToArray();                     
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "STF")
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var listStock = (from followStock in db.FollowStocks.ToList()
                                 where followStock.UserId == currentUser.UserExtentLogin.Id
                                 select followStock.StockFollowed
                                 ).ToList();


                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where listStock.Contains(stockRelate.StockPrimary)
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Take(10).ToArray();
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        public async Task<dynamic> GetMorePostsGlobal(int skipposition, string filter)
        {
            //var fjdsf = WebSecurity.CurrentUserId;
            if (filter == "" || filter == "ALL")
            {
                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "CHA")
            {
                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where stockRelate.ChartYN == true
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "STM")
            {
                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where stockRelate.NhanDinh > 0
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "DIS")
            {
                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where stockRelate.NhanDinh != 1 && stockRelate.NhanDinh != 2 && stockRelate.ChartYN != true
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "VIP")
            {
                var ret = (from stockRelate in await db.PinStocks.ToListAsync()
                           orderby stockRelate.Post.PostedDate descending
                           select new
                           {
                               Message = stockRelate.Post.ChartYN == true ? stockRelate.Post.Message + "<br/><img src='" + stockRelate.Post.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Post.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.Post.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.Post.StockPrimary,
                               Stm = stockRelate.Post.NhanDinh,
                               ChartYN = stockRelate.Post.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "PEF")
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var listPersonFollow = (from userFollow in db.FollowUsers.ToList()
                                        where userFollow.UserId == currentUser.UserExtentLogin.Id
                                        select userFollow.UserIdFollowed
                                 ).ToList();


                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where listPersonFollow.Contains(stockRelate.PostedBy)
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "STF")
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var listStock = (from followStock in db.FollowStocks.ToList()
                                 where followStock.UserId == currentUser.UserExtentLogin.Id
                                 select followStock.StockFollowed
                                 ).ToList();


                var ret = (from stockRelate in await db.Posts.ToListAsync()
                           where listStock.Contains(stockRelate.StockPrimary)
                           orderby stockRelate.PostedDate descending
                           select new
                           {
                               Message = stockRelate.ChartYN == true ? stockRelate.Message + "<br/><img src='" + stockRelate.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.UserLogin.AvataImage) ? ImageURLAvataDefault + "?width=50&height=50&mode=crop" : ImageURLAvata + stockRelate.UserLogin.AvataImage + "?width=50&height=50&mode=crop",
                               PostedDate = stockRelate.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.StockPrimary,
                               Stm = stockRelate.NhanDinh,
                               ChartYN = stockRelate.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            else
            {
                return null;
            }
        }



        #region extention
        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
    }



    public class StockDetail
    {
        public string Stock { get; set; }
        public string ShortName { get; set; }
    }
#endregion
}
