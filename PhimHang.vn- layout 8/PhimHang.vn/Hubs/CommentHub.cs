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
using System.Data.Entity;



namespace PhimHang.Hubs
{
    [HubName("CommentHub")]    
    public class CommentHub : Hub
    {
         
        // GET: /Post/

        private const string ImageURLAvataDefault = "/img/avatar2.jpg"; 
        private const string ImageURLAvata = "/images/avatar/";
        private string hostURL = AppHelper.TinyURL;
        testEntities db = new testEntities();
        TinyURLEntities dbtinyURL = new TinyURLEntities();
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
        [Authorize] 
        public async Task AddPost(Post post, string stockCurrent, int currentUserId, string userName, string avataImageUrl, byte nhanDinh, string chartImage)
        {
           
            #region format message
            string messagedefault = "";
            messagedefault = post.Message;
            List<string> listMessege = post.Message.Split(' ').ToList();
            string messageFromatHTML = "";
            foreach (var item in listMessege)
            {
                if (item.Contains("$"))
                {
                    string ticker = item.Replace("$", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToUpper();
                    messageFromatHTML += "<b><a onclick=selectMe(event,\"#\") target='_blank' href='/ticker/" + ticker + "'>" + item + "</a></b>" + " ";
                }
                else if (item.Contains("@"))
                {
                    string user = item.Replace("@", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToLower();
                    messageFromatHTML += "<a onclick=selectMe(event,\"#\") target='_blank' href='/user/" + user + "/tab/1'>" + item + "</a>" + " ";
                }
                else if (item.Contains("http") || item.Contains("www."))
                {
                    URLTiny tu = new URLTiny();
                    tu.URLName = item;
                    dbtinyURL.URLTinies.Add(tu);
                    try
                    {                        
                        await dbtinyURL.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        // log                    
                    }
                    messageFromatHTML += "<a onclick=selectMe(event,\"#\") target='_blank' href='" + hostURL + "/" + tu.Id + "'>" + AppHelper.GetDomain(item) + "...</a>" + " ";
                }
                else
                {
                    messageFromatHTML += item + " ";
                }
            }

            #endregion
            //messageFromatHTML += "</a>";
            post.Message = AppHelper.FilteringWord( messageFromatHTML);            
            post.PostedBy = currentUserId;
            post.PostedDate = DateTime.Now;
            post.StockPrimary = stockCurrent;
            post.NhanDinh = nhanDinh;
            post.SumLike = 0;
            if (!string.IsNullOrWhiteSpace(chartImage))
            {
                post.ChartYN = true;
                post.ChartImageURL = chartImage.Replace("?width=50&height=50&mode=crop", "");
            }
            
            var listStock = new List<string>();
            var listUsersendMessege = new List<string>();
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
                    string stockcode = item.Replace("$", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToUpper();
                    if (item.Contains("$") && !listStock.Contains(stockcode)) // find the stock with $
                    {

                        StockRelate stockRelateLasts = new StockRelate();
                        stockRelateLasts.PostId = post.PostId;
                        stockRelateLasts.StockCodeRelate = stockcode;
                        db.StockRelates.Add(stockRelateLasts); // add to database
                        listStock.Add(stockcode); // group of hub for client 
                    }
                    else if(item.Contains("@")) //find the user with @
                    {
                        // code later

                        string user = item.Replace("@", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToLower();
                        var finduser = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == user);
                        if (finduser != null)
                        {
                            NotificationMessege nM = new NotificationMessege { UserPost = currentUserId, UserReciver = finduser.Id, PostId = post.PostId, NumNoti = 1, TypeNoti = "U", CreateDate = DateTime.Now, XemYN = true };
                            db.NotificationMesseges.Add(nM);
                            listUsersendMessege.Add(user);
                        }

                    }
                }


                /* add stockrelate */
                try
                {
                    await db.SaveChangesAsync();
                    await dbtinyURL.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // log                    
                }
                

                var ret = new
                {
                    //Message = post.Message,
                    Message =  post.Message,
                    Chart = post.ChartImageURL,
                    //PostedBy = post.PostedBy,
                    PostedByName = userName,
                    PostedByAvatar = avataImageUrl,
                    PostedDate = post.PostedDate,
                    PostId = post.PostId,
                    StockPrimary = post.StockPrimary,
                    Stm = post.NhanDinh,
                    ChartYN = post.ChartYN,
                    PostBy = post.PostedBy,
                    SumLike = 0,
                    SumReply =0
                };

                await Clients.Groups(listStock).addPost(ret); // ad group
                await Clients.All.addPostGlobal(ret); // add vào profile
                if (listUsersendMessege.Count > 0)
                {
                    await Clients.Users(listUsersendMessege).MessegeOfUserPost(1);
                }
            }
        }
        [Authorize] 
        public async Task AddReply(PostComment reply, string stockCurrent, int currentUserId, string userName, string avataImageUrl)
        {
            reply.CommentBy = currentUserId;
            reply.PostedDate = DateTime.Now;

            #region format message
            string messagedefault = "";
            messagedefault = reply.Message;
            List<string> listMessege = reply.Message.Split(' ').ToList();
            string messageFromatHTML = "";
            foreach (var item in listMessege)
            {
                if (item.Contains("$"))
                {
                    string ticker = item.Replace("$", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToUpper();
                    messageFromatHTML += "<b><a onclick=selectMe(event,\"#\") target='_blank' href='/ticker/" + ticker + "'>" + item + "</a></b>" + " ";
                }
                else if (item.Contains("@"))
                {
                    string user = item.Replace("@", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToLower();
                    messageFromatHTML += "<a onclick=selectMe(event,\"#\") target='_blank' href='/user/" + user + "/tab/1'>" + item + "</a>" + " ";
                }
                else if (item.Contains("http") || item.Contains("www."))
                {
                    URLTiny tu = new URLTiny();
                    tu.URLName = item;
                    dbtinyURL.URLTinies.Add(tu);
                    try
                    {
                        await dbtinyURL.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        // log                    
                    }
                    messageFromatHTML += "<a onclick=selectMe(event,\"#\") target='_blank' href='" + hostURL + "/" + tu.Id + "'>" + AppHelper.GetDomain(item) + "...</a>" + " ";
                }
                else
                {
                    messageFromatHTML += item + " ";
                }
            }

            #endregion
            reply.Message = AppHelper.FilteringWord(messageFromatHTML);
            //var listStock = new List<string>();
            //listStock.Add(stockCurrent.ToUpper());
            using (testEntities db = new testEntities())
            {
                var listUsersendMessege = new List<string>();
                ///////////////////////////////////////////
                var getPost = db.Posts.Find(reply.PostedBy); // lay thong tin cua bài post đó
                // cap nhat tong so luong reply
                getPost.SumReply += 1;
                
                #region gui tin cho chu da post bài
                
                if (getPost.PostedBy != currentUserId)
                {
                    var nMRecive = db.NotificationMesseges.FirstOrDefault(nm => nm.UserReciver == getPost.UserLogin.Id && nm.PostId == reply.PostedBy);
                    if (nMRecive == null)
                    {
                        NotificationMessege nM = new NotificationMessege { UserPost = currentUserId, UserReciver = getPost.UserLogin.Id, PostId = reply.PostedBy, NumNoti = 1, TypeNoti = "R", CreateDate = DateTime.Now, XemYN = true };
                        db.NotificationMesseges.Add(nM);
                    }
                    else
                    {
                        // get messge and update 
                        nMRecive.NumNoti += 1;
                        nMRecive.XemYN = true;
                        nMRecive.CreateDate = DateTime.Now;
                        db.Entry(nMRecive).State = EntityState.Modified;
                    }
                    listUsersendMessege.Add(getPost.UserLogin.UserNameCopy);
                }
                ///////////////////////////////////////////
                #endregion


                #region reply có đề cập đến user nào không ??
                List<string> listMessegeSplit = reply.Message.Split(' ').ToList().FindAll(p => p.Contains("$") || p.Contains("@"));
                foreach (var item in listMessegeSplit)
                {
                    if (item.Contains("@")) //find the user with @
                    {
                        string user = item.Replace("@", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToLower();
                        var finduser = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == user);
                        if (finduser != null)
                        {
                            var nMuser = db.NotificationMesseges.FirstOrDefault(nm => nm.UserReciver == finduser.Id && nm.PostId == reply.PostedBy);
                            if (nMuser == null)
                            {
                                NotificationMessege nM = new NotificationMessege { UserPost = currentUserId, UserReciver = finduser.Id, PostId = reply.PostedBy, NumNoti = 1, TypeNoti = "R", CreateDate = DateTime.Now, XemYN = true };
                                db.NotificationMesseges.Add(nM);
                            }
                            else
                            {
                                // get messge and update 
                                nMuser.NumNoti += 1;
                                nMuser.XemYN = true;
                                nMuser.CreateDate = DateTime.Now;
                                db.Entry(nMuser).State = EntityState.Modified;
                            }
                            listUsersendMessege.Add(user);
                        }
                    }
                }
                #endregion

                db.PostComments.Add(reply);
                
                try
                {
                    db.Entry(getPost).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // chay tiep
                    
                }
               

                var ret = new
                {
                    ReplyMessage = reply.Message,
                    //PostedBy = post.PostedBy,
                    ReplyByName = userName,
                    ReplyByAvatar = avataImageUrl,
                    ReplyDate = reply.PostedDate,
                    ReplyId = reply.PostCommentsId,
                    PostCommentsId = reply.PostCommentsId
                };


                await Clients.Caller.addReply(ret); // chính người đã reply
                if (listUsersendMessege.Count > 0)
                {
                    await Clients.Users(listUsersendMessege).MessegeOfUserPost(1);
                }
                await Clients.All.newReplyNoti(reply.PostedBy);
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

        
        public async Task AddNewLike(long postid)
        {            
            var postFind = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postid);
            if (postFind != null)
            {
                postFind.SumLike = postFind.SumLike + 1;
                try
                {
                    db.Entry(postFind).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {

                    //throw;
                }
                await Clients.All.addNewLike(postid);

            }
        }

    }
}