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

        public async Task<ActionResult> Layout_dark()
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

        public async Task<JsonResult> LoadHistoryOfMessege(string userOrGroup, string groupname, int top, int left, string ctrId)
        {
            int i;
            var idFromUser = db.UserLogins.FirstOrDefault(u => u.UserNameCopy == User.Identity.Name).Id;
            //save status window in database
            var checkesistwindow = db.StatusWindows.Where(w => w.CtrId == ctrId && w.UserName == User.Identity.Name).ToList();
            if (checkesistwindow.Count == 0)
            {
                StatusWindow sw = new StatusWindow
                {
                    CtrId = ctrId,
                    WindowName = groupname,
                    TopPosition = top.ToString() + "px",
                    LeftPosition = left.ToString() + "px",
                    UserName = User.Identity.Name,
                    KeyWindowName = userOrGroup

                };
                db.StatusWindows.Add(sw);
                await db.SaveChangesAsync();
            }
            else
            {
                var windowstatusupdate = checkesistwindow[0];
                windowstatusupdate.TopPosition = top.ToString() + "px";
                windowstatusupdate.LeftPosition = left.ToString() + "px";
                db.Entry(windowstatusupdate).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            //
            if (int.TryParse(userOrGroup, out i)) // load  group messege
            {
                var result = (from md in db.Group_User_Messege
                              //orderby md ascending
                              where (md.GroupId == i)
                              select new
                              {
                                  messege = "<div class='message'><span class='userName'>" + md.WhoChat + "</span>: " + md.ConentMesseger + "</div>"
                              }).ToListAsync();


                return await Task.FromResult(Json(result.Result, JsonRequestBehavior.AllowGet));
            }
            else // load user messege
            {
                var idToUser = db.UserLogins.FirstOrDefault(u => u.UserNameCopy == userOrGroup).Id;
                var result = (from md in db.MessegeDirects
                              //orderby md ascending
                              where (md.FromUser == idFromUser && md.ToUser == idToUser) ||
                              (md.FromUser == idToUser && md.ToUser == idFromUser)
                              select new
                              {
                                  messege = "<div class='message'><span class='userName'>" + md.WhoChat + "</span>: " + md.ConentMesseger + "</div>"
                              }).ToListAsync();


                return await Task.FromResult(Json(result.Result, JsonRequestBehavior.AllowGet));
            }
        }

        public Task<JsonResult> LoadWindowStatusOnLoad()
        {
            var userNamelogin = User.Identity.Name;
            //var result = db.UserLogins.Where(ul => !ul.UserNameCopy.Contains(userNamelogin)).ToList();
            var result = (from ul in db.StatusWindows
                          where ul.UserName == userNamelogin
                          select new
                          {
                              c = ul.CtrId,
                              w = ul.WindowName,
                              l = ul.LeftPosition,
                              t = ul.TopPosition,
                              u = ul.UserName,
                              k = ul.KeyWindowName
                          }).ToListAsync();
            return Task.FromResult(Json(result.Result, JsonRequestBehavior.AllowGet));
        }

        public async Task ChangePositionWindow(string groupid, int top, int left)
        {
            var userNamelogin = User.Identity.Name;
            var group = db.StatusWindows.FirstOrDefault(sw => sw.KeyWindowName == groupid && sw.UserName == userNamelogin);
            group.TopPosition = top.ToString() + "px";
            group.LeftPosition = left.ToString() + "px";
            db.Entry(group).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}