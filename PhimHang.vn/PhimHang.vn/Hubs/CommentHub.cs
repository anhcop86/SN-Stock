using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhimHang.Models;
using System.Security.Principal;
using Microsoft.AspNet.SignalR.Hubs;

namespace PhimHang.Hubs
{
    [HubName("CommentHub")]
    public class CommentHub : Hub
    {
         
        // GET: /Post/
        
        private const string ImageURLAvataDefault = "img/avatar_default.jpg";
        private const string ImageURLAvata = "images/avatar/";

        public void GetPosts(string stockCurrent)
        {
            //var fjdsf = WebSecurity.CurrentUserId;
            using (testEntities db = new testEntities())
            {
                var ret = (from stockRelate in db.StockRelates.ToList()
                           where stockRelate.StockCodeRelate == stockCurrent
                           orderby stockRelate.Post.PostedDate descending
                           select new
                           {
                               Message = stockRelate.Post.Message
                               //PostedBy = stockRelate.Post.PostedDate,
                               //PostedByName = stockRelate.Post.UserLogin.FullName,
                               //PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage,
                               //PostedDate = stockRelate.Post.PostedDate,
                               //PostId = stockRelate.PostId
                           }).ToArray();
                var listStock = new List<string>();
                listStock.Add(stockCurrent);

                Clients.Groups(listStock).loadPosts(ret);

            }
        }

        public void AddPost(Post post, string stockCurrent, int currentUserId)
        {
            post.PostedBy = currentUserId;
            post.PostedDate = DateTime.UtcNow;
            StockRelate stockRelate = new StockRelate();
            using (testEntities db = new testEntities())
            {
                db.Posts.Add(post);
                stockRelate.PostId = post.PostId;
                stockRelate.StockCodeRelate = stockCurrent;
                db.StockRelates.Add(stockRelate);
                db.SaveChanges();
                //var usr = db.UserProfiles.FirstOrDefault(x => x.UserId == post.PostedBy);
                var ret = new
                {
                    Message = post.Message
                    //PostedBy = post.PostedBy,
                    //PostedByName = usr.UserName,
                    //PostedByAvatar = imgFolder + (String.IsNullOrEmpty(usr.AvatarExt) ? defaultAvatar : post.PostedBy + "." + post.UserProfile.AvatarExt),
                    //PostedDate = post.PostedDate,
                    //PostId = post.PostId
                };

                var listStock = new List<string>();
                listStock.Add(stockCurrent);

                Clients.Groups(listStock).addPost(ret);
            }
        }



        public Task JoinRoom(string stockCurrent)
        {
            return Groups.Add(Context.ConnectionId, stockCurrent);
        }

        public Task LeaveRoom(string stockCurrent)
        {
            return Groups.Remove(Context.ConnectionId, stockCurrent);
        }
    }
}