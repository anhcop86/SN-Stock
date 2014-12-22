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
using System.Text.RegularExpressions;

namespace PhimHang.Hubs
{
    [HubName("CommentHub")]
    public class CommentHub : Hub
    {
         
        // GET: /Post/
        
        private const string ImageURLAvataDefault = "/img/avatar_default.jpg";
        private const string ImageURLAvata = "/images/avatar/";

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
                               Message = stockRelate.Post.Message,
                               //PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.Post.UserLogin.UserNameCopy,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage + "?width=46&height=46&mode=crop",
                               PostedDate = stockRelate.Post.PostedDate,
                               //PostId = stockRelate.PostId
                           }).ToArray();
                var listStock = new List<string>();
                listStock.Add(stockCurrent);

                Clients.Groups(listStock).loadPosts(ret);

            }
        }

        public void AddPost(Post post, string stockCurrent, int currentUserId, string userName, string avataImageUrl)
        {
            post.PostedBy = currentUserId;
            post.PostedDate = DateTime.UtcNow;
            
            var listStock = new List<string>();
            
            #region explan this passing messege to stockcode and username list

            List<string> listMessegeSplit = post.Message.Split(' ').ToList().FindAll(p => p.Contains("$") || p.Contains("@"));
                        
            #endregion

            using (testEntities db = new testEntities())
            {
                db.Posts.Add(post);
                /* co phieu dau tien la chinh no */
                if (stockCurrent != "")
                {
                    StockRelate stockRelateFirst = new StockRelate();
                    stockRelateFirst.PostId = post.PostId;
                    stockRelateFirst.StockCodeRelate = stockCurrent;
                    db.StockRelates.Add(stockRelateFirst); // add to database
                    listStock.Add(stockCurrent); // group of hub for client 
                }
                /* END */
                //db.Posts.Add(post);
                /* add post with stockrelate list */
                foreach (var item in listMessegeSplit)
                {
                    if (item.Contains("$") && !item.Contains(stockCurrent)) // find the stock with $
                    {
                        string stockcode = item.Replace("$", "");
                        StockRelate stockRelateLasts = new StockRelate();
                        stockRelateLasts.PostId = post.PostId;
                        stockRelateLasts.StockCodeRelate = stockcode;
                        db.StockRelates.Add(stockRelateLasts); // add to database
                        listStock.Add(stockcode); // group of hub for client 
                    }
                    else //find the user with @
                    {
                        // code later
                    }
                }
               

                /* add stockrelate */
                 db.SaveChanges();

                var ret = new
                {
                    Message = post.Message,
                    //PostedBy = post.PostedBy,
                    PostedByName = userName,
                    PostedByAvatar = "/" + avataImageUrl.Replace("amp;", ""),
                    PostedDate = post.PostedDate
                    //PostId = post.PostId
                };

               

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