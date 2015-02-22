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
    public class PostDetailController : Controller
    {
        //
        public PostDetailController()
            : this( new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public PostDetailController( UserManager<ApplicationUser> userManager)
        {            
            UserManager = userManager;
        }
       
        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        private const string ImageURLAvata = "/images/avatar/";
 

        public async Task<ActionResult> Index(long postid)
        {
            using (db = new testEntities())
            {

                if (User.Identity.IsAuthenticated)
                {
                    ApplicationUser currentUser = new ApplicationUser();
                    currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                }

                var post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postid);

                ViewBag.Message = post.ChartYN == true ? post.Message + "<br/><img src='" + post.ChartImageURL + "?width=215&height=120&mode=crop' >" : post.Message;
                ViewBag.PostedByName = post.UserLogin.UserNameCopy;
                ViewBag.PostedByAvatar = string.IsNullOrEmpty(post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + post.UserLogin.AvataImage;
                ViewBag.PostedDate = post.PostedDate;
                ViewBag.PostId = post.PostId;
                ViewBag.StockPrimary = post.StockPrimary;
                ViewBag.Stm = post.NhanDinh;
                ViewBag.ChartYN = post.ChartYN;
                ViewBag.PostBy = post.PostedBy;

                // end
                return View();
            }
            
        }

        public async Task<dynamic> GetReplyByPostId(long replyid)
        {

            using (db = new testEntities())
            {

                var ret = (from reply in await db.PostComments.ToListAsync()
                           where reply.PostedBy == replyid
                           orderby reply.PostedDate ascending
                           select new
                           {
                               ReplyMessage = reply.Message,
                               ReplyByName = reply.UserLogin.UserNameCopy,
                               ReplyByAvatar = string.IsNullOrEmpty(reply.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + reply.UserLogin.AvataImage + "?width=46&height=46&mode=crop",
                               ReplyDate = reply.PostedDate,
                               ReplyId = reply.PostCommentsId,
                           }).Take(100).ToArray();
                //return await Task.FromResult(ret);

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
