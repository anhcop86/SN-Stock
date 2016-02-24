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
        
        private readonly StockRealTimeTicker _stockRealtime;
        public MyProfileController()
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public MyProfileController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
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
        public async Task<ActionResult> Index()
        {
            #region get user info
            ViewBag.AbsolutePathHostName = AbsolutePathHostName;
            var company = new StockCode();
            //ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            UserLoginDTO currentUser = await (from userlog in db.UserLogins
                                           where userlog.UserNameCopy == User.Identity.Name
                                           select new UserLoginDTO 
                                           {
                                               Id = userlog.Id,
                                               CharacterLimit = userlog.CharacterLimit,
                                               FullName = userlog.FullName,
                                               AvataImage = userlog.AvataImage,
                                               AvataCover = userlog.AvataCover,
                                               BrokerVIP = userlog.BrokerVIP
                                           }).FirstOrDefaultAsync();
            #endregion
            #region Thong tin menu ben trai
            //Thong tin menu ben trai
            var post = await db.Posts.CountAsync(p => p.PostedBy == currentUser.Id);
            var follow = await db.FollowUsers.CountAsync(f => f.UserId == currentUser.Id);
            var follower = await db.FollowUsers.CountAsync(f => f.UserIdFollowed == currentUser.Id);

            ViewBag.TotalPost = post;
            ViewBag.Follow = follow;
            ViewBag.Follower = follower;

            ViewBag.CureentUserId = currentUser.Id;
            ViewBag.CharacterLimit = currentUser.CharacterLimit;
            ViewBag.UserName = User.Identity.Name;
            ViewBag.FullName = currentUser.FullName;
            ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.AvataImage;
            ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.AvataCover) == true ? ImageURLCoverDefault : ImageURLCover + currentUser.AvataCover;
            

            // cac post duoc loc tu danh muc nguoi theo doi => dc load o duoi client san
            var listPersonFollow = await (from userFollow in db.FollowUsers
                                    where userFollow.UserId == currentUser.Id
                                    select userFollow.UserIdFollowed
                                 ).ToArrayAsync();

            ViewBag.ListFollow = listPersonFollow as int[]; //client

            // cac post dc loc tu danh muc dau tu => dc load o duoc client san
            var listStock = await (from followStock in db.FollowStocks
                                   orderby followStock.StockFollowed ascending
                                   where followStock.UserId == currentUser.Id
                                   select followStock.StockFollowed).ToListAsync();

            ViewBag.listStockFollow = listStock as List<string>; // load o cliet
            // End thong tin menu ben trai
            //so luong tin cua User
            var numberMessegeNew = db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.Id && nm.NumNoti > 0).Sum(mn => mn.NumNoti);
            ViewBag.NewMessege = numberMessegeNew;


            #endregion
            #region danh muc co phieu dang follow
            ViewBag.listStockPriceFollow = _stockRealtime.GetAllStocksList(listStock as List<string>).Result;
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
     

        public async Task<ActionResult> MessagesCenter()
        {
            #region thong tin user
            //ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            UserLoginDTO currentUser = await (from userlog in db.UserLogins
                                              where userlog.UserNameCopy == User.Identity.Name
                                              select new UserLoginDTO
                                              {
                                                  Id = userlog.Id,
                                                  CharacterLimit = userlog.CharacterLimit,
                                                  FullName = userlog.FullName,
                                                  AvataImage = userlog.AvataImage,
                                                  AvataCover = userlog.AvataCover,
                                                  BrokerVIP = userlog.BrokerVIP
                                              }).FirstOrDefaultAsync();
            ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.AvataImage) == true ? ImageURLAvataDefault : "/images/avatar/" + currentUser.AvataImage;
            ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.AvataCover) == true ? ImageURLCoverDefault : "/images/cover/" + currentUser.AvataCover;
            ViewBag.AvataImageUrl = string.IsNullOrEmpty(currentUser.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.AvataImage;
            ViewBag.CureentUserId = currentUser.Id;
            ViewBag.UserName = User.Identity.Name;
            @ViewBag.FullName = currentUser.FullName;
            #endregion
            #region thong tin co phieu ben phai
            var listStock = await (from followStock in db.FollowStocks
                             orderby followStock.StockFollowed ascending
                             where followStock.UserId == currentUser.Id
                             select followStock.StockFollowed
                               ).ToListAsync();
            //ViewBag.listStockFollow = listStock as List<string>; // client
            #region gia cổ phieu cua cac ma dang theo doi
            ViewBag.listStockPriceFollow = _stockRealtime.GetAllStocksList(listStock as List<string>).Result;
            #endregion
            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion
            #region reset lai so luong tin nhan
            ViewBag.NewMessege = 0;
            // luu database
           
            //
            #endregion

            #endregion
            #region danh muc co phieu nong
            var listHotStock = await AppHelper.GetListHotStock();
            ViewBag.ListStockHot = listHotStock;
            #endregion
            return View(currentUser);
        }
        #region load messagesCenter
        public async Task<dynamic> GetMessagesByUserId(string userid, int skipposition, string filter)
        {
            if (filter == "" || filter == "ALL")
            {
                var ret = await (from notiMesseges in db.NotificationMesseges
                           orderby notiMesseges.CreateDate descending, notiMesseges.XemYN descending
                           where notiMesseges.UserLogin1.UserNameCopy == User.Identity.Name
                           select new
                           {
                               Message = notiMesseges.Post.Message,
                               Chart = notiMesseges.Post.ChartImageURL,                               
                               PostedByName = notiMesseges.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(notiMesseges.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + notiMesseges.Post.UserLogin.AvataImage,
                               PostedDate = notiMesseges.Post.PostedDate,
                               PostId = notiMesseges.Post.PostId,                               
                               Stm = notiMesseges.Post.NhanDinh,
                               ChartYN = notiMesseges.Post.ChartYN,
                               XemYN = notiMesseges.XemYN,
                               SumLike = notiMesseges.Post.SumLike,
                               SumReply = notiMesseges.Post.SumReply,
                               BrkVip = notiMesseges.Post.UserLogin.BrokerVIP
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
        [HttpPost]
        public async Task ChangeStatusMessege(string userid)
        {
            var listUpdate = await db.NotificationMesseges.Where(nm => nm.UserLogin1.UserNameCopy == User.Identity.Name && nm.XemYN == true).ToListAsync();
            if (listUpdate.Count > 0)
            {
                foreach (var item in listUpdate)
                {
                    item.NumNoti = 0;
                    item.XemYN = false;
                    db.Entry(item).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
            }
        }

        #endregion

        [HttpGet]
        public async Task<dynamic> GetMorePostsGlobal(int skipposition, string filter)
        {
            //var fjdsf = WebSecurity.CurrentUserId;
            if (filter == "" || filter == "ALL")
            {
                var ret = await (from posts in db.Posts
                                 where posts.StockPrimary != ""
                                 orderby posts.Priority descending, posts.PostedDate descending
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
                                     BrkVip = posts.UserLogin.BrokerVIP,
                                     Pri = posts.Priority
                                 }).Skip(skipposition).Take(10).ToArrayAsync();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "CHA")
            {
                var ret = await (from posts in db.Posts
                           where posts.ChartYN == true && posts.StockPrimary != ""
                                 orderby posts.Priority descending, posts.PostedDate descending
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
                               BrkVip = posts.UserLogin.BrokerVIP,
                               Pri = posts.Priority
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "STM")
            {
                var ret = await (from posts in db.Posts
                                 where posts.NhanDinh > 0 && posts.StockPrimary != ""
                                 orderby posts.Priority descending, posts.PostedDate descending
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
                               BrkVip = posts.UserLogin.BrokerVIP,
                               Pri = posts.Priority
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            #region khong su dung
            //if (filter == "VIP")
            //{
            //    var ret = await (from pinStocks in db.PinStocks
            //                     where pinStocks.Post.StockPrimary != ""
            //                     orderby pinStocks.Post.PostedDate descending
            //                     select new
            //                     {
            //                         Message = pinStocks.Post.Message,
            //                         Chart = pinStocks.Post.ChartImageURL,
            //                         PostedByName = pinStocks.Post.UserLogin.UserNameCopy,
            //                         PostedByAvatar = string.IsNullOrEmpty(pinStocks.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + pinStocks.Post.UserLogin.AvataImage,
            //                         PostedDate = pinStocks.Post.PostedDate,
            //                         PostId = pinStocks.PostId,
            //                         StockPrimary = pinStocks.Post.StockPrimary,
            //                         Stm = pinStocks.Post.NhanDinh,
            //                         ChartYN = pinStocks.Post.ChartYN,
            //                         SumLike = pinStocks.Post.SumLike,
            //                         SumReply = pinStocks.Post.SumReply,
            //                         //BrkVip = pinStocks.UserLogin.BrokerVIP
            //                     }).Skip(skipposition).Take(10).ToArrayAsync();
            //    //var listStock = new List<string>();              
            //    var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            //    return result;
            //}
            #endregion
            if (filter == "PEF")
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var listPersonFollow = await (from userFollow in db.FollowUsers
                                        where userFollow.UserId == currentUser.UserExtentLogin.Id
                                        select userFollow.UserIdFollowed).ToListAsync();


                var ret = await (from posts in db.Posts
                                 where listPersonFollow.Contains(posts.PostedBy) && posts.StockPrimary != ""
                                 orderby posts.Priority descending, posts.PostedDate descending
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
                               BrkVip = posts.UserLogin.BrokerVIP,
                               Pri = posts.Priority
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
            if (filter == "STF")
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var listStock = await (from followStock in db.FollowStocks
                                 where followStock.UserId == currentUser.UserExtentLogin.Id
                                 select followStock.StockFollowed).ToListAsync();


                var ret = await (from posts in db.Posts
                           where listStock.Any(ls => posts.StockPrimary.IndexOf(ls) > -1)
                                 orderby posts.Priority descending, posts.PostedDate descending
                           select new
                           {
                               Message =  posts.Message,
                               Chart = posts.ChartImageURL,
                               PostedByName = posts.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(posts.UserLogin.AvataImage) ? ImageURLAvataDefault  : ImageURLAvata + posts.UserLogin.AvataImage ,
                               PostedDate = posts.PostedDate,
                               PostId = posts.PostId,
                               StockPrimary = posts.StockPrimary,
                               Stm = posts.NhanDinh,
                               ChartYN = posts.ChartYN,
                               SumLike = posts.SumLike,
                               SumReply = posts.SumReply,
                               BrkVip = posts.UserLogin.BrokerVIP,
                               Pri = posts.Priority
                           }).Skip(skipposition).Take(10).ToListAsync();
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
    public class UserLoginDTO
    {
        public int Id { get; set; }
        public int CharacterLimit { get; set; }
        public string FullName { get; set; }
        public string AvataImage { get; set; }
        public string AvataCover { get; set; }
        public Nullable<bool> BrokerVIP { get; set; }
    }
}
