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
                          orderby u.Id ascending
                          where (u.URLName.Contains(searchContain) || "ALL" == searchContain)
                          select u;


              int pageSize = 20;
              int pageNumber = (page ?? 1);

              return View(Task.FromResult(users.ToPagedList(pageNumber, pageSize)).Result);
          }

	}
}