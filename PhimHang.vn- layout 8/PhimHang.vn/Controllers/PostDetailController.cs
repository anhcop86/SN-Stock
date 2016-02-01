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
using System.Text.RegularExpressions;

namespace PhimHang.Controllers
{
    //[Authorize] // xoa khi public
    public class PostDetailController : Controller
    {
        private readonly StockRealTimeTicker _stockRealtime;
        public PostDetailController()
            : this(StockRealTimeTicker.Instance, new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public PostDetailController(StockRealTimeTicker stockTicker, UserManager<ApplicationUser> userManager)
        {
            _stockRealtime = stockTicker;
            UserManager = userManager;
        }
       
        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        private const string ImageURLAvata = "/images/avatar/";
        private string AbsolutePathHostName = AppHelper.AbsolutePathHostName;

        public async Task<ActionResult> Index(long postid)
        {
            using (db = new testEntities())
            {

                if (User.Identity.IsAuthenticated)
                {
                    ApplicationUser currentUser = new ApplicationUser();
                    currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());                    
                    ViewBag.CureentUserId = currentUser.Id;
                    ViewBag.UserName = currentUser.UserName;
                    ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.UserExtentLogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.UserExtentLogin.AvataImage;
                    var numberMessegeNew = await db.NotificationMesseges.Where(nm => nm.UserReciver == currentUser.UserExtentLogin.Id && nm.NumNoti > 0).SumAsync(mn => mn.NumNoti);
                    ViewBag.NewMessege = numberMessegeNew;
                }
                else
                {
                    ViewBag.AvataEmage = ImageURLAvataDefault;
                }

                var post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postid);

                #region thong tin chi tiet bai viet o client bang viewbag

                ViewBag.Message =  post.Message;
                ViewBag.MessageNonHtml = AppHelper.StripTagsCharArray(post.Message);
                ViewBag.PostedByName = post.UserLogin.UserNameCopy;
                ViewBag.PostedByAvatar = string.IsNullOrEmpty(post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + post.UserLogin.AvataImage;
                ViewBag.PostedDate = post.PostedDate;
                ViewBag.PostId = post.PostId;
                ViewBag.StockPrimary = post.StockPrimary;
                ViewBag.Stm = post.NhanDinh;
                ViewBag.ChartYN = post.ChartYN;
                ViewBag.PostBy = post.PostedBy;
                ViewBag.Chart = post.ChartImageURL;
                ViewBag.SumReply = post.SumReply;
                ViewBag.SumLike = post.SumLike;
                ViewBag.BrkVip = post.UserLogin.BrokerVIP;
                ViewBag.AbsolutePathHostName = AbsolutePathHostName;

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
                // end
                return View();
            }
            
        }

        //[AllowAnonymous]
        [HttpGet]
        public async Task<dynamic> GetReplyByPostId(long replyid)
        {
            using (db = new testEntities())
            {
                var ret = await (from reply in db.PostComments
                           where reply.PostedBy == replyid
                           orderby reply.PostedDate descending
                           select new
                           {
                               ReplyMessage = reply.Message,
                               ReplyByName = reply.UserLogin.UserNameCopy,
                               ReplyByAvatar = string.IsNullOrEmpty(reply.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + reply.UserLogin.AvataImage,
                               ReplyDate = reply.PostedDate,
                               ReplyId = reply.PostCommentsId,
                               PostCommentsId = reply.PostCommentsId,
                               BrkVip = reply.UserLogin.BrokerVIP
                           }).ToArrayAsync();               

                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
        }


        #region temple
        //
        // GET: /PostDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PostDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PostDetail/Create
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
        // GET: /PostDetail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PostDetail/Edit/5
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
        // GET: /PostDetail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PostDetail/Delete/5
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
        #endregion
    }
}
