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
        [Authorize]
        public async Task AddPost(Post post,  byte nhanDinh, string chartImage)
        {
            using (testEntities db = new testEntities())
            {
                #region user login
                var userlogin = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == Context.User.Identity.Name);
                if (userlogin == null)
                {
                    return;
                }
                #endregion
                #region format message
                string messagedefault = "";
                string stockTag = ""; // dinh dang stock|stock|stock de tim co phieu lien quan
                messagedefault = post.Message;
                List<string> listMessege = post.Message.Split(' ').ToList();
                string messageFromatHTML = "";
                foreach (var item in listMessege)
                {
                    if (item.Contains("$"))
                    {
                        string ticker = item.Replace("$", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToUpper();
                        messageFromatHTML += "<b><a onclick=selectMe(event,\"#\") target='_blank' href='/ticker/" + ticker + "'>" + item + "</a></b>" + " ";
                        stockTag += ticker + "|";
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
                        tu.PostedDate = DateTime.Now;
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
                post.Message = AppHelper.FilteringWord(messageFromatHTML);
                post.PostedBy = userlogin.Id;
                post.PostedDate = DateTime.Now;
                post.StockPrimary = stockTag;
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


                db.Posts.Add(post);
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
                    else if (item.Contains("@")) //find the user with @
                    {
                        string user = item.Replace("@", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToLower();
                        var finduser = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == user);
                        if (finduser != null)
                        {
                            NotificationMessege nM = new NotificationMessege { UserPost = userlogin.Id, UserReciver = finduser.Id, PostId = post.PostId, NumNoti = 1, TypeNoti = "U", CreateDate = DateTime.Now, XemYN = true };
                            db.NotificationMesseges.Add(nM);
                            listUsersendMessege.Add(user);
                        }
                    }
                }

                /* add stockrelate */
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // log                    
                }

                var ret = new
                {
                    Message = post.Message,
                    Chart = post.ChartImageURL,
                    PostedByName = userlogin.UserNameCopy,
                    PostedByAvatar = string.IsNullOrEmpty(userlogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + userlogin.AvataImage,
                    PostedDate = post.PostedDate,
                    PostId = post.PostId,
                    StockPrimary = post.StockPrimary,
                    Stm = post.NhanDinh,
                    ChartYN = post.ChartYN,
                    PostBy = post.PostedBy,
                    SumLike = 0,
                    SumReply = 0
                };

                await Clients.Groups(listStock).addPost(ret); // ad group co phieu lien quan
                await Clients.All.addPostGlobal(ret); // add vào profile va home
                if (listUsersendMessege.Count > 0)
                {
                    await Clients.Users(listUsersendMessege).MessegeOfUserPost(1); // gui tin bao cho user nao có @
                }
            }
        }
        [Authorize]
        public async Task AddReply(PostComment reply)
        {

            using (testEntities db = new testEntities())
            {
                #region user login
                var userlogin = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == Context.User.Identity.Name);
                if (userlogin == null)
                {
                    return;
                }
                #endregion
                reply.CommentBy = userlogin.Id;
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
                        tu.PostedDate = DateTime.Now;
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

                var listUsersendMessege = new List<string>();
                ///////////////////////////////////////////
                var getPost = db.Posts.Find(reply.PostedBy); // lay thong tin cua bài post đó
                // cap nhat tong so luong reply
                getPost.SumReply += 1;

                #region gui tin cho chu da post bài

                if (getPost.PostedBy != userlogin.Id)
                {
                    var nMRecive = db.NotificationMesseges.FirstOrDefault(nm => nm.UserReciver == getPost.UserLogin.Id && nm.PostId == reply.PostedBy);
                    if (nMRecive == null)
                    {
                        NotificationMessege nM = new NotificationMessege { UserPost = userlogin.Id, UserReciver = getPost.UserLogin.Id, PostId = reply.PostedBy, NumNoti = 1, TypeNoti = "R", CreateDate = DateTime.Now, XemYN = true };
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
                List<string> listMessegeSplit = messagedefault.Split(' ').ToList().FindAll(p => p.Contains("$") || p.Contains("@"));
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
                                NotificationMessege nM = new NotificationMessege { UserPost = userlogin.Id, UserReciver = finduser.Id, PostId = reply.PostedBy, NumNoti = 1, TypeNoti = "R", CreateDate = DateTime.Now, XemYN = true };
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
                    ReplyByName = userlogin.UserNameCopy,
                    ReplyByAvatar = string.IsNullOrEmpty(userlogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + userlogin.AvataImage,
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