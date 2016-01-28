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
    public class ShortenLinkController : Controller
    {
        //
        // GET: /ShortenLink/
          private ShortenLinkEntities dbshortLink = new ShortenLinkEntities();
          public async Task<ActionResult> Index(int? page, string searchContain) // list user
          {
              ViewBag.linkAbsolutePath = Request.Url.PathAndQuery;
              if (string.IsNullOrWhiteSpace(searchContain))
              {
                  searchContain = "ALL";
              }
              ViewBag.searchContain = searchContain;
              var users = from u in dbshortLink.URLTinies
                          orderby u.PostedDate descending
                          where (u.URLName.Contains(searchContain) || "ALL" == searchContain)
                          select u;


              int pageSize = AppHelper.PageSize;
              int pageNumber = (page ?? 1);

              return View(Task.FromResult(users.ToPagedList(pageNumber, pageSize)).Result);
          }
          public async Task<ActionResult> Delete(long linkid)
          {
              ViewBag.linkAbsolutePath = Request.Url.Query.Replace("?postid=" + linkid + "&returnUrl=", "");
              if (linkid == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              URLTiny post = await dbshortLink.URLTinies.FindAsync(linkid);
              if (post == null)
              {
                  return HttpNotFound();
              }
              return View(post);
          }

          [HttpPost, ActionName("Delete")] // xóa phuong thuc post, tạm thời để ten la detail
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> DeleteConfirmed(long linkid)
          {
              using (dbshortLink = new ShortenLinkEntities())
              {
                  var url = Request.Url.Query.Replace("?linkid=" + linkid + "&returnUrl=", "");


                  URLTiny post = await dbshortLink.URLTinies.FindAsync(linkid);
                  dbshortLink.URLTinies.Remove(post);
                  await dbshortLink.SaveChangesAsync();
                  return RedirectToAction("");
              }


          }
	}
}