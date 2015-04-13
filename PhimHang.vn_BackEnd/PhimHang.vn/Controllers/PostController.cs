using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhimHang.Models;
using PagedList;
using System.Data.Entity;


namespace PhimHang.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        //
        // GET: /Post/
        private db_cungphim_FrontEnd dbcungphim = new db_cungphim_FrontEnd();
        public async Task<ActionResult> Index(long postid)
        {

            return View();
        }
        public async Task<ActionResult> Detail(long postid, string returnUrl)
        {
            ViewBag.linkAbsolutePath = Request.Url.Query.Replace("?postid=" + postid + "&returnUrl=", "");
            if (postid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await dbcungphim.Posts.FindAsync(postid);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Detail")] // xóa phuong thuc post, tạm thời để ten la detail
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long postid)
        {
            var url = Request.Url.Query.Replace("?postid=" + postid + "&returnUrl=", "");
            // remove notification 
            var notifications = await dbcungphim.NotificationMesseges.Where(n => n.PostId == postid).ToListAsync();
            dbcungphim.NotificationMesseges.RemoveRange(notifications);
            //
            // remove  stockRelate
            var stockRelates = await dbcungphim.StockRelates.Where(n => n.PostId == postid).ToListAsync();
            dbcungphim.StockRelates.RemoveRange(stockRelates);
            //
            // remove  stockRelate
            var comments = await dbcungphim.PostComments.Where(n => n.PostedBy == postid).ToListAsync();
            dbcungphim.PostComments.RemoveRange(comments);
            //

            Post post = await dbcungphim.Posts.FindAsync(postid);
            dbcungphim.Posts.Remove(post);
            await dbcungphim.SaveChangesAsync();
            return RedirectToLocal(url);
            
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}