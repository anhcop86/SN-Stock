using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhimHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;

namespace PhimHang.Controllers
{

    public class CommentController : ApiController
    {
        private testEntities db = new testEntities();
        // GET api/comment
        
        public CommentController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {            
            
        }
        public CommentController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        // GET: /Post/
        
        private const string ImageURLAvataDefault = "img/avatar_default.jpg";
        private const string ImageURLAvata = "images/avatar/";
        public async Task<dynamic> Get(string stockCurrent)
        {

            var ret = (from post in await db.StockRelates.ToListAsync()
                       where post.StockCodeRelate == stockCurrent
                       orderby post.Post.PostedDate descending
                       select new
                       {
                           Message = post.Post.Message,
                           PostedBy = post.Post.PostedDate,
                           PostedByName = post.Post.UserLogin.FullName,
                           PostedByAvatar = string.IsNullOrEmpty(post.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + post.Post.UserLogin.AvataImage,
                           PostedDate = post.Post.PostedDate,
                           PostId = post.PostId
                       }).AsEnumerable();

            return ret;
        }

        // GET api/comment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/comment
        public void Post([FromBody]string value)
        {

        }

        // PUT api/comment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/comment/5
        public void Delete(int id)
        {
        }
    }
}
