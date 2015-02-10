using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhimHang.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace PhimHang.Controllers
{
    public class PostController : Controller
    {
        private testEntities db = new testEntities();

        public PostController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {

        }
        public PostController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        // GET: /Post/
        public async Task<ActionResult> Index()
        {
            return View(await db.Posts.ToListAsync());
        }

        // GET: /Post/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: /Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostId,Message,ChartImageURL,NhanDinh,Vir")] Post post)
        {
            if (ModelState.IsValid)
            {

                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }
                var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                post.PostedBy = currentUser.UserExtentLogin.Id;
                post.PostedDate = DateTime.Now;
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: /Post/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostId,Message,ChartImageURL,NhanDinh,Vir")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostedBy = 1;
                post.PostedDate = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: /Post/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        private const string ImageURLAvata = "/images/avatar/";


        [AllowAnonymous]
        [HttpGet]
        public async Task<dynamic> GetReplyByPostId(long replyid)
        {

            using (db = new testEntities())
            {

                var ret = (from reply in await db.PostComments.ToListAsync()
                           where reply.PostedBy == replyid
                           orderby reply.PostedDate descending
                           select new
                           {
                               ReplyMessage = reply.Message,
                               ReplyByName = reply.UserLogin.UserNameCopy,
                               ReplyByAvatar = string.IsNullOrEmpty(reply.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + reply.UserLogin.AvataImage,
                               ReplyDate = reply.PostedDate,
                               ReplyId = reply.PostCommentsId,
                               PostCommentsId = reply.PostCommentsId
                           }).Take(10).ToArray();
                //return await Task.FromResult(ret);

                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<dynamic> GetPostsByStockPin(string stockCurrent)
        {
            //var fjdsf = WebSecurity.CurrentUserId;
            using (db = new testEntities())
            {
                var ret = (from stockRelate in await db.PinStocks.ToListAsync()
                           where stockRelate.StockCodePin == stockCurrent
                           orderby stockRelate.CreatedDate descending
                           select new
                           {
                               Message = stockRelate.Post.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage,
                               PostedDate = stockRelate.Post.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.Post.StockPrimary,
                               Stm = stockRelate.Post.NhanDinh,
                               ChartYN = stockRelate.Post.ChartYN
                           }).Take(5).ToArray();
                //var listStock = new List<string>();              
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
                return result;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<dynamic> GetMorePostsByStock(string stockCurrent, int skipposition, string filter)
        {
            //var fjdsf = WebSecurity.CurrentUserId;     
            if (filter == "" || filter == "ALL")
            {
                var ret = (from stockRelate in await db.StockRelates.ToListAsync()
                           where stockRelate.StockCodeRelate == stockCurrent
                           orderby stockRelate.Post.PostedDate descending
                           select new
                           {
                               Message = stockRelate.Post.ChartYN == true ? stockRelate.Post.Message + "<br/><img src='" + stockRelate.Post.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Post.Message,
                               PostedByName = stockRelate.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage,
                               PostedDate = stockRelate.Post.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.Post.StockPrimary,
                               Stm = stockRelate.Post.NhanDinh,
                               ChartYN = stockRelate.Post.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            }
            if (filter == "CHA")
            {
                var ret = (from stockRelate in await db.StockRelates.ToListAsync()
                           where stockRelate.StockCodeRelate == stockCurrent && stockRelate.Post.ChartYN == true
                           orderby stockRelate.Post.PostedDate descending
                           select new
                           {
                               Message = stockRelate.Post.ChartYN == true ? stockRelate.Post.Message + "<br/><img src='" + stockRelate.Post.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Post.Message,
                               PostedByName = stockRelate.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage,
                               PostedDate = stockRelate.Post.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.Post.StockPrimary,
                               Stm = stockRelate.Post.NhanDinh,
                               ChartYN = stockRelate.Post.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            }
            if (filter == "STM")
            {
                var ret = (from stockRelate in await db.StockRelates.ToListAsync()
                           where stockRelate.StockCodeRelate == stockCurrent && stockRelate.Post.NhanDinh > 0
                           orderby stockRelate.Post.PostedDate descending
                           select new
                           {
                               Message = stockRelate.Post.ChartYN == true ? stockRelate.Post.Message + "<br/><img src='" + stockRelate.Post.ChartImageURL + "?width=215&height=120&mode=crop' >" : stockRelate.Post.Message,
                               PostedByName = stockRelate.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage,
                               PostedDate = stockRelate.Post.PostedDate,
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.Post.StockPrimary,
                               Stm = stockRelate.Post.NhanDinh,
                               ChartYN = stockRelate.Post.ChartYN
                           }).Skip(skipposition).Take(10).ToArray();
                return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            }
            else
            {
                return null;
            }

           
        }
        private const string ChartImageDirectory = "/Chart/";
        [HttpPost]
        public string UploadFileChart()
        {          
            var httpPostedFile = HttpContext.Request.Files["UploadedImage"];
            if (httpPostedFile != null)
            {
                // upload iamge
                var uploadDir = ChartImageDirectory;
                var geraralFileName = User.Identity.Name + DateTime.Now.ToString("yyyyMMddHHmmss") + "_chart";
                var realfileName = uploadDir + geraralFileName + Path.GetExtension(httpPostedFile.FileName) + "?width=50&height=50&mode=crop"; // directory return client

                #region upload file

                var imagePath = Path.Combine(Server.MapPath(uploadDir), geraralFileName + Path.GetExtension(httpPostedFile.FileName));
                httpPostedFile.SaveAs(imagePath);

                return realfileName; // return name file
                #endregion    
            }
            else
            {
                return "error"; // return name file error
            }
        }
        
    }
}
