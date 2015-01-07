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
    public class UserController : Controller
    {
        //
        // GET: /User/
        public UserController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public UserController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
       
        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "/img/avatar_default.jpg";        
        private const string ImageURLAvata = "/images/avatar/";
        

        public async Task<ActionResult> Index(string username, int tabid)
        {
            using (db=new testEntities())
            {
                var currentUser = await db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username); //db.UserLogins.FirstOrDefaultAsync(u => u.UserNameCopy == username);

                if (currentUser == null || string.IsNullOrEmpty(username) || tabid > 5 || tabid < 0)
                {
                    return RedirectToAction("","Search");
                }
                #region thong tin user
                
                ViewBag.AvataImageUrl = string.IsNullOrEmpty(currentUser.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + currentUser.AvataImage + "?width=98&height=98&mode=crop";
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

                ViewBag.TabId = tabid;
                if (tabid == 1)
                {
                    Session["DataTimeVistUser"] = DateTime.Now;
                }
                else if (tabid == 2)
                {

                }
                else if (tabid == 3)
                {

                }
                else if (tabid == 4)
                {

                }
                
                return View(currentUser);
            }
            
        }

        public async Task<dynamic> GetPostByUserId(int userid, int skipposition)
        {

            if (Session["DataTimeVistUser"] == null)
            {
                Session["DataTimeVistUser"] = DateTime.Now;
            }
            var dataTimeVistUser = (DateTime)Session["DataTimeVistUser"];
            using (db = new testEntities())
            {

                var ret = (from post in await db.Posts.ToListAsync()
                           where post.PostedBy == userid
                           && (post.PostedDate < dataTimeVistUser)
                           orderby post.PostedDate descending
                           select new
                           {
                               Message = post.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + post.UserLogin.AvataImage + "?width=46&height=46&mode=crop",
                               PostedDate = post.PostedDate,
                               PostId = post.PostId,
                               StockPrimary = post.StockPrimary
                           }).Skip(skipposition).Take(10).ToArray();
                //return await Task.FromResult(ret);

                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
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
}
