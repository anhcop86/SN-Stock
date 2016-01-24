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
                               PostCommentsId = reply.PostCommentsId
                           }).Take(10).ToArrayAsync();
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
                var ret = await(from pinStocks in db.PinStocks
                           where pinStocks.StockCodePin == stockCurrent
                           orderby pinStocks.CreatedDate descending
                           select new
                           {
                               Message = pinStocks.Post.Message,
                               Chart = pinStocks.Post.ChartImageURL,                               
                               PostedByName = pinStocks.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(pinStocks.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + pinStocks.Post.UserLogin.AvataImage,
                               PostedDate = pinStocks.Post.PostedDate,
                               PostId = pinStocks.PostId,                               
                               Stm = pinStocks.Post.NhanDinh,
                               ChartYN = pinStocks.Post.ChartYN,
                               SumLike = pinStocks.Post.SumLike,
                               SumReply = pinStocks.Post.SumReply
                           }).Take(5).ToArrayAsync();                
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
                var ret = await (from posts in db.Posts
                           where posts.StockPrimary.Contains(stockCurrent)
                           orderby posts.PostedDate descending
                           select new
                           {
                               Message = posts.Message,
                               Chart = posts.ChartImageURL,
                               PostedByName = posts.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(posts.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + posts.UserLogin.AvataImage,
                               PostedDate = posts.PostedDate,
                               PostId = posts.PostId,                               
                               Stm = posts.NhanDinh,
                               ChartYN = posts.ChartYN,
                               SumLike = posts.SumLike,
                               SumReply = posts.SumReply
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            }
            if (filter == "CHA")
            {
                var ret = await (from posts in db.Posts
                                 where posts.StockPrimary.Contains(stockCurrent) && posts.ChartYN == true
                           orderby posts.PostedDate descending
                           select new
                           {
                               Message = posts.Message,
                               Chart = posts.ChartImageURL,
                               PostedByName = posts.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(posts.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + posts.UserLogin.AvataImage,
                               PostedDate = posts.PostedDate,
                               PostId = posts.PostId,                               
                               Stm = posts.NhanDinh,
                               ChartYN = posts.ChartYN,
                               SumLike = posts.SumLike,
                               SumReply = posts.SumReply
                           }).Skip(skipposition).Take(10).ToArrayAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            }
            if (filter == "STM")
            {
                var ret = await (from posts in db.Posts
                                 where posts.StockPrimary.Contains(stockCurrent) && posts.NhanDinh > 0
                           orderby posts.PostedDate descending
                           select new
                           {
                               Message = posts.Message,
                               Chart = posts.ChartImageURL,
                               PostedByName = posts.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(posts.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + posts.UserLogin.AvataImage,
                               PostedDate = posts.PostedDate,
                               PostId = posts.PostId,                               
                               Stm = posts.NhanDinh,
                               ChartYN = posts.ChartYN,
                               SumLike = posts.SumLike,
                               SumReply = posts.SumReply
                           }).Skip(skipposition).Take(10).ToArrayAsync();
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
            #region check valid file

            var validImageTypes = new string[]
                                                {
                                                    "image/gif",
                                                    "image/jpeg",
                                                    "image/pjpeg",
                                                    "image/png"
                                                };
            if (httpPostedFile == null || httpPostedFile.ContentLength == 0) // check file null or file corrupt
            {
                return "Chưa chọn file upload";
            }

            if (!validImageTypes.Contains(httpPostedFile.ContentType)) // check file type
            {
                return "Please choose either a GIF, JPG or PNG image.";
            }

            if (httpPostedFile.ContentLength > 3000000) // check file size
            {
                return "File's very larg: File must be less than 700KB";
            }

            #endregion
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

        [HttpPost]
        public async Task<string> ReportError(long postid)
        {
            return "Y";
        }

        public async Task<bool> CheckButtonDelete(long postid, int? userid)
        {
            try
            {
                var post = await db.Posts.CountAsync(p => p.PostId == postid && p.PostedBy == userid);
                if (post > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;                
            }            
            
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> DeletePostFromClientRequest(long postid)
        {
            try
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var notifications = await db.NotificationMesseges.Where(nm => nm.PostId == postid).ToListAsync();
                var stockRelate = await db.StockRelates.Where(st => st.PostId == postid).ToListAsync();
                var postComments = await db.PostComments.Where(pc => pc.PostedBy == postid).ToListAsync();
                
                Post post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postid && p.PostedBy == currentUser.UserExtentLogin.Id);
                if (post!= null)
                {
                    db.NotificationMesseges.RemoveRange(notifications);
                    db.StockRelates.RemoveRange(stockRelate);
                    db.PostComments.RemoveRange(postComments);
                    db.Posts.Remove(post);
                    await db.SaveChangesAsync();
                    return true;    
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        [AllowAnonymous]
        public async Task UpdateLike(long postid)
        {
            var postFind = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postid);
            if (postFind != null)
            {
                postFind.SumLike = postFind.SumLike + 1;
                
                try
                {
                    db.Entry(postFind).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {

                    //throw;
                }

            }
        }
        
    }
}
