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

        /*
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
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.Post.StockPrimary
                           }).Take(10).ToArray();
                //var listStock = new List<string>();              

                Clients.Client(Context.ConnectionId).loadPosts(ret);
              
            }
        }*/

        public async Task AddPost(Post post, string stockCurrent, int currentUserId, string userName, string avataImageUrl, byte nhanDinh)
        {
           
            #region format message
            string messagedefault = "";
            messagedefault = post.Message;
            List<string> listMessege = post.Message.Split(' ').ToList();
            string messageFromatHTML = "";
            foreach (var item in listMessege)
            {
                if (item.Contains("$") || item.Contains("@"))
                {
                    messageFromatHTML += "<b>" + item + "</b>" + " ";
                }
                else if (item.Contains("http") || item.Contains("www."))
                {
                    messageFromatHTML += "<a target='_blank' href='" + item + "'>[LINK]</a>" + " ";
                }
                else
                {
                    messageFromatHTML += item + " ";
                }
            }

            #endregion
            //messageFromatHTML += "</a>";
            post.Message = messageFromatHTML;
            post.PostedBy = currentUserId;
            post.PostedDate = DateTime.Now;
            post.StockPrimary = stockCurrent;
            post.NhanDinh = nhanDinh;
            
            var listStock = new List<string>();
            
            #region explan this passing messege to stockcode and username list

            List<string> listMessegeSplit = messagedefault.Split(' ').ToList().FindAll(p => p.Contains("$") || p.Contains("@"));
                        
            #endregion

            using (testEntities db = new testEntities())
            {
                db.Posts.Add(post);
                /* co phieu dau tien la chinh no */
                if (stockCurrent != "KEYMYPROFILE")
                {
                    StockRelate stockRelateFirst = new StockRelate();
                    stockRelateFirst.PostId = post.PostId;
                    stockRelateFirst.StockCodeRelate = stockCurrent;
                    db.StockRelates.Add(stockRelateFirst); // add to database
                    listStock.Add(stockCurrent.ToUpper()); // group of hub for client 
                }
                /* END */
                //db.Posts.Add(post);
                /* add post with stockrelate list */
                foreach (var item in listMessegeSplit)
                {
                    if (item.Contains("$") && !item.Contains(stockCurrent) && !listStock.Contains(item)) // find the stock with $
                    {
                        string stockcode = item.Replace("$", "").ToUpper();
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
                await db.SaveChangesAsync();

                var ret = new
                {
                    Message = post.Message,
                    //PostedBy = post.PostedBy,
                    PostedByName = userName,
                    PostedByAvatar = avataImageUrl.Replace("amp;", ""),
                    PostedDate = post.PostedDate,
                    PostId = post.PostId,
                    StockPrimary = post.StockPrimary,
                    Stm = post.NhanDinh
                };
               
               await Clients.Groups(listStock).addPost(ret);
            }
        }

        public async Task AddReply(PostComment reply, string stockCurrent, int currentUserId, string userName, string avataImageUrl)
        {
            reply.CommentBy = currentUserId;
            reply.PostedDate = DateTime.Now;
            var listStock = new List<string>();
            listStock.Add(stockCurrent.ToUpper());
            using (testEntities db = new testEntities())
            {
                db.PostComments.Add(reply);
                await db.SaveChangesAsync();

                var ret = new
                {
                    ReplyMessage = reply.Message,
                    //PostedBy = post.PostedBy,
                    ReplyByName = userName,
                    ReplyByAvatar = avataImageUrl.Replace("amp;", ""),
                    ReplyDate = reply.PostedDate,
                    ReplyId = reply.PostCommentsId,
                    PostCommentsId = reply.PostCommentsId
                };
                await Clients.Caller.addReply(ret);
                //await Clients.OthersInGroups(listStock).newReplyNoti(1, reply.PostedBy);
            }
        }

        public async Task JoinRoom(string stockCurrent)
        {
            await Groups.Add(Context.ConnectionId, stockCurrent);
        }

        public async Task LeaveRoom(string stockCurrent)
        {
            await Groups.Remove(Context.ConnectionId, stockCurrent);
        }

        //public override Task OnConnected()
        //{
        //    var connectionId = Context.ConnectionId;

        //    return base.OnConnected();
             
        //}

        //public override Task OnReconnected()
        //{
        //    var connectionId = Context.ConnectionId;
        //    return base.OnReconnected();
        //}

        // ondisconnected => remove group existed
        public override Task OnDisconnected(bool stopCall)
        {
            var connectionId = Context.ConnectionId;
            return base.OnDisconnected(stopCall);
        }

        ///////////////////////////////////////////////////// profile
        //reply
        /////////////////////////////////////////////////////
        public async Task AddReply(PostComment postcomment, string stockCurrent, int currentUserId, string userName, string avataImageUrl, long postid)
        {
            #region format message
            string messagedefault = "";
            messagedefault = postcomment.Message;
            List<string> listMessege = postcomment.Message.Split(' ').ToList();
            string messageFromatHTML = "";
            foreach (var item in listMessege)
            {
                if (item.Contains("$") || item.Contains("@"))
                {
                    messageFromatHTML += "<b>" + item + "</b>" + " ";
                }
                else if (item.Contains("http") || item.Contains("www."))
                {
                    messageFromatHTML += "<a target='_blank' href='" + item + "'>[LINK]</a>" + " ";
                }
                else
                {
                    messageFromatHTML += item + " ";
                }
            }

            postcomment.Message = messageFromatHTML;
            postcomment.PostedDate = DateTime.Now;
            postcomment.PostedBy = postid;
            postcomment.CommentBy = currentUserId;
            #endregion

            var listStock = new List<string>();
            listStock.Add(stockCurrent.ToUpper());
            #region explan this passing messege to stockcode and username list

            //List<string> listMessegeSplit = messagedefault.Split(' ').ToList().FindAll(p => p.Contains("$") || p.Contains("@"));

            #endregion

            using (testEntities db = new testEntities())
            {
                db.PostComments.Add(postcomment);
                await db.SaveChangesAsync();

                var ret = new
                {
                    ReplyMessage = postcomment.Message,
                    //PostedBy = post.PostedBy,
                    ReplyByName = userName,
                    ReplyByAvatar = "/" + avataImageUrl.Replace("amp;", ""),
                    ReplyDate = postcomment.PostedDate,
                    ReplyId = postcomment.PostCommentsId
                };
                await Clients.Client(Context.ConnectionId).addReply(ret);
                await Clients.OthersInGroups(listStock).newReplyNoti(1, postid);
            }
        }

    }
}