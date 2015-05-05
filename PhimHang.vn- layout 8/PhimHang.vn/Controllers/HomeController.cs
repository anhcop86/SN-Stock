﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhimHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;

namespace PhimHang.Controllers
{
    //[Authorize] // xoa khi public
    public class HomeController : Controller
    {
        //private readonly FilterKeyworkSingleton _keyword;
        private readonly StockRealTimeTicker _stockRealtime;
        public HomeController()
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public HomeController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            //_keyword = KeyworkSing;
            _stockRealtime = stockTicker;
            UserManager = userManager;
        }


        public UserManager<ApplicationUser> UserManager { get; private set; }
        private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        private const string ImageURLCoverDefault = "/img/cover_default.jpg";
        private const string ImageURLAvata = "/images/avatar/";
        private const string ImageURLCover = "images/cover/";
        private string AbsolutePathHostName = AppHelper.AbsolutePathHostName;
        private testEntities db = new testEntities();
        public async Task<ActionResult> Index()
        {
            // get user info
            ViewBag.AbsolutePathHostName = AbsolutePathHostName;
            if (User.Identity.IsAuthenticated)
            {

                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                ViewBag.CureentUserId = currentUser.Id;
                ViewBag.UserName = currentUser.UserName;

                ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.UserExtentLogin.AvataImage;
                ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataCover) == true ? ImageURLCoverDefault : ImageURLCover + currentUser.UserExtentLogin.AvataCover;

                // End thong tin menu ben trai
                //so luong tin cua User
                var numberMessegeNew = db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).Sum(mn => mn.NumNoti);
                ViewBag.NewMessege = numberMessegeNew;
            }
            else
            {
                ViewBag.AvataEmage = ImageURLAvataDefault;
            }

          
            #region gia cổ phieu cua cac co phieu nong
            
            var listStock = await AppHelper.GetListHotStock();            
            ViewBag.ListStockHot = listStock;
            /*
            ViewBag.listStockPriceHot = _stockRealtime.GetAllStocksList(listStock as List<string>).Result;
             * */
            #endregion

            #region co phieu ngau ben trái
            ViewBag.listStockRandom = _stockRealtime.RandomStocksList().Result;
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
                                       }).Take(5).ToListAsync();
            ViewBag.DanPhimRandom = DanPhimRandom;
            #endregion
            #region gia chi so index va hnxindex
            var listIndex = new List<string>();
            listIndex.Add("VNINDEX");
            listIndex.Add("HNXINDEX");
            ViewBag.ListIndex = _stockRealtime.GetAllStocksList(listIndex).Result;
            #endregion
            return View();
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
        [HttpGet]
        public async Task<dynamic> GetMorePostsGlobal(int skipposition, string filter)
        {
            //var fjdsf = WebSecurity.CurrentUserId;
            if (filter == "" || filter == "ALL")
            {
                var ret = await (from stockRelate in db.Posts
                                 orderby stockRelate.PostedDate descending
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
            else
            {
                return null;
            }
        }
    }
}