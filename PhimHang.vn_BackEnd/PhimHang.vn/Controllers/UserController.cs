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
    public class UserController : Controller
    {
        //
        // GET: /User/
        private db_cungphim_FrontEnd dbcungphim = new db_cungphim_FrontEnd();
        public async Task<ActionResult> Index(int? page, string username) // list user
        {
            ViewBag.linkAbsolutePath = Request.Url.PathAndQuery;
            if (string.IsNullOrWhiteSpace(username))
            {
                username = "ALL";
            }
            ViewBag.userName = username;
            var users = from u in dbcungphim.UserLogins
                        orderby u.CreatedDate descending, u.UserNameCopy ascending
                        where (u.UserNameCopy.Contains(username) || "ALL" == username)
                        select new UserModel
                        {
                            Id  = u.Id,
                            UserName = u.UserNameCopy,
                            CreatedDate = u.CreatedDate,
                            Name = u.FullName,
                            BrokerType = (u.BrokerVIP == true ? "YES" : "NO"),
                            LockAccount = (u.DisableUser == true ? "YES" : "NO")
                        };


            int pageSize = AppHelper.PageSize; ;
            int pageNumber = (page ?? 1);

            return View(Task.FromResult(users.ToPagedList(pageNumber, pageSize)).Result); 
        }

        public async Task<ActionResult> update(int userid) // list user
        {
            var url = Request.Url.Query.Replace("?userid=" + userid + "&returnUrl=", "");
            ViewBag.linkAbsolutePath = url;
            var user = await dbcungphim.UserLogins.FindAsync(userid);
            LoadInit(user);
            return View(user);
        }

       

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Boolean? brokerVIP, int userid, int characterLimitInput, bool? disableUser)
        {
            var url = Request.Url.Query.Replace("?userid=" + userid + "&returnUrl=", "");
            using (dbcungphim = new db_cungphim_FrontEnd())
            {
                var user = await dbcungphim.UserLogins.FindAsync(userid);
                if (user != null)
                {
                    if (user.BrokerVIP != brokerVIP)
                    {
                        user.BrokerVIP = brokerVIP;
                        try
                        {
                            dbcungphim.Entry(user).State = EntityState.Modified;
                            await dbcungphim.SaveChangesAsync();
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }
                    if (user.CharacterLimit != characterLimitInput)
                    {
                        user.CharacterLimit = characterLimitInput > 6000 ? 6000 : characterLimitInput;
                        try
                        {
                            dbcungphim.Entry(user).State = EntityState.Modified;
                            await dbcungphim.SaveChangesAsync();
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }
                    if (user.DisableUser != disableUser)
                    {
                        user.DisableUser = disableUser;
                        try
                        {
                            dbcungphim.Entry(user).State = EntityState.Modified;
                            await dbcungphim.SaveChangesAsync();
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }

                }
            }


            return Redirect(url);
        }

        private void LoadInit(UserLogin user)
        {
            // load chi box dan phim chuyen nghiep
            var setBrokerPro = new List<dynamic>
            {
                new {Id = false , Name = "False"},
                new {Id = true , Name = "True"}
            };
            // load box trạng thái đóng mở của user
            var setLockUser = new List<dynamic>
            {
                new {Id = false , Name = "False"},
                new {Id = true , Name = "True"}
            };


            ViewBag.listTypeBroker = new SelectList(setBrokerPro, "Id", "Name", user.BrokerVIP);
            ViewBag.setLockUser = new SelectList(setLockUser, "Id", "Name", user.DisableUser);
        }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BrokerType { get; set; }
        public string LockAccount { get; set; }
    }
}