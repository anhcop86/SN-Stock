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
using PagedList;
using System.Data.Entity;

namespace PhimHang.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        DBChatGroup db = new DBChatGroup();
        public async Task<ActionResult> Index()
        {

            

            return View();
        }

        private async Task LoadInit()
        {

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public Task<JsonResult> GetListUserExcept()
        {
            var userNamelogin = User.Identity.Name;
            //var result = db.UserLogins.Where(ul => !ul.UserNameCopy.Contains(userNamelogin)).ToList();
            var result = (from ul in db.UserLogins
                          where !ul.UserNameCopy.Contains(userNamelogin)
                          select new
                          {
                              id = ul.UserNameCopy,
                              name = ul.UserNameCopy
                          }).ToListAsync();


            return Task.FromResult(Json(result.Result, JsonRequestBehavior.AllowGet));
        }
    }
}