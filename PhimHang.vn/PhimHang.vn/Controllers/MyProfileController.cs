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
        public MyProfileController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public MyProfileController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        private testEntities db = new testEntities();
        private const string ImageURLAvataDefault = "/img/avatar_default.jpg";
        private const string ImageURLCoverDefault = "/img/cover_default.jpg";
        private const string ImageURLAvata = "/images/avatar/";
        private const string ImageURLCover = "/images/cover/";
        public async Task<ActionResult> Index()
        {
            // get user info
            ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            // thong tin bai phim
            var post = await db.Posts.CountAsync(p=> p.PostedBy == currentUser.UserExtentLogin.Id);
            var follow = await db.FollowUsers.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id);
            var follower = await db.FollowUsers.CountAsync(f => f.UserIdFollowed == currentUser.UserExtentLogin.Id);


            ViewBag.TotalPost = post;
            ViewBag.Follow = follow;
            ViewBag.Follower = follower;
           
            ViewBag.CoverImage = string.IsNullOrEmpty(currentUser.AvataCover) == true ?  ImageURLCoverDefault :  ImageURLCover + currentUser.AvataCover;
            ViewBag.AvataEmage = string.IsNullOrEmpty(currentUser.AvataImage) == true ?  ImageURLAvataDefault :  ImageURLAvata + currentUser.AvataImage;


            return View(currentUser);
        }

        //
        // GET: /MyProfile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MyProfile/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MyProfile/Create
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
        // GET: /MyProfile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MyProfile/Edit/5
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
        // GET: /MyProfile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MyProfile/Delete/5
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
