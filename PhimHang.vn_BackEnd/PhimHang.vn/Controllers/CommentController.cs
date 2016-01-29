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

    public class CommentController : Controller
    {
        //
        private db_cungphim_FrontEnd dbcungphim = new db_cungphim_FrontEnd();
        // GET: /Comment/
        public async Task<ActionResult> Index(int? page, string dateFilter)
        {
            ViewBag.linkAbsolutePath = Request.Url.PathAndQuery;
        

           
            var datetimeFilter = new DateTime();
            var datetimeFilterTo = new DateTime();
            if (string.IsNullOrWhiteSpace(dateFilter) || dateFilter == "ALL")
            {
                dateFilter = DateTime.Now.ToString("dd/MM/yyyy");
            }
            datetimeFilter = new DateTime(int.Parse(dateFilter.Substring(6, 4)), int.Parse(dateFilter.Substring(3, 2)), int.Parse(dateFilter.Substring(0, 2)));
            datetimeFilterTo = datetimeFilter.AddDays(1);

            ViewBag.datefilter = dateFilter;
            

            var posts = from p in dbcungphim.PostComments
                        orderby p.PostedDate descending
                        where ((p.PostedDate >= datetimeFilter && p.PostedDate < datetimeFilterTo) || new DateTime() == datetimeFilter)                         
                        select p;
            int pageSize = AppHelper.PageSize;
            int pageNumber = (page ?? 1);

            return View(Task.FromResult(posts.ToPagedList(pageNumber, pageSize)).Result);

        }
        public async Task<ActionResult> Delete(long commentid, long postid)
        {
            ViewBag.linkAbsolutePath = Request.Url.Query.Replace("?commentid=" + commentid + "&returnUrl=", "");
            if (commentid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostComment post = await dbcungphim.PostComments.FindAsync(commentid);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")] // xóa phuong thuc post, tạm thời để ten la detail
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long commentid, long postid)
        {
            using (dbcungphim = new db_cungphim_FrontEnd())
            {                
                // remove notification                 
                try
                {                    
                    PostComment postComment = await dbcungphim.PostComments.FindAsync(commentid);
                    Post post = await dbcungphim.Posts.FindAsync(postid);
                    if (post.SumReply > 0)
                    {
                        post.SumReply -= 1;
                        dbcungphim.Entry(post).State = EntityState.Modified;
                    }
                    dbcungphim.PostComments.Remove(postComment);
                    await dbcungphim.SaveChangesAsync();
                }
                catch (Exception)
                {
                    
                    //
                }
                
                return RedirectToAction("");
            }


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