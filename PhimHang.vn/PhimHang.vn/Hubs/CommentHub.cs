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
                               PostId = stockRelate.PostId,
                               StockPrimary = stockRelate.Post.StockPrimary
                           }).Take(10).ToArray();
                //var listStock = new List<string>();              

                Clients.Client(Context.ConnectionId).loadPosts(ret);
              
            }
        }

        public void AddPost(Post post, string stockCurrent, int currentUserId, string userName, string avataImageUrl, int nhanDinh)
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

            if (nhanDinh == -1)
            {
                messageFromatHTML += " <span class='sentiment bullishs'>Giảm</span>";
            }
            else if (nhanDinh == 0)
            {
                messageFromatHTML += " <span class='sentiment Normals'>Đứng</span>";
            }
            else
            {
                messageFromatHTML += " <span class='sentiment bearishs'>Tăng</span>";
            }
            #endregion
            //messageFromatHTML += "</a>";
            post.Message = messageFromatHTML;
            post.PostedBy = currentUserId;
            post.PostedDate = DateTime.Now;
            post.StockPrimary = stockCurrent;
            
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
                    if (item.Contains("$") && !item.Contains(stockCurrent)) // find the stock with $
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
                 db.SaveChanges();

                var ret = new
                {
                    Message = post.Message,
                    //PostedBy = post.PostedBy,
                    PostedByName = userName,
                    PostedByAvatar = "/" + avataImageUrl.Replace("amp;", ""),
                    PostedDate = post.PostedDate,
                    PostId = post.PostId,
                    StockPrimary = post.StockPrimary
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

        //public override Task OnDisconnected(bool stopCall)
        //{
        //    var connectionId = Context.ConnectionId;            
        //    return base.OnDisconnected(stopCall);
        //}

        ///////////////////////////////////////////////////// profile
        //reply
        /////////////////////////////////////////////////////
        public void AddReply(PostComment postcomment, string stockCurrent, int currentUserId, string userName, string avataImageUrl, long postid)
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
                db.SaveChanges();

                var ret = new
                {
                    ReplyMessage = postcomment.Message,
                    //PostedBy = post.PostedBy,
                    ReplyByName = userName,
                    ReplyByAvatar = "/" + avataImageUrl.Replace("amp;", ""),
                    ReplyDate = postcomment.PostedDate,
                    ReplyId = postcomment.PostCommentsId              
                };
                Clients.Client(Context.ConnectionId).addReply(ret);
                Clients.OthersInGroups(listStock).newReplyNoti(1, postid);
            } 
        }

    }
}