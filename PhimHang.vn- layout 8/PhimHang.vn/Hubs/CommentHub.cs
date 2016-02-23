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
        public async Task AddPost(Post post,  byte nhanDinh, string chartImage, long? userpageid)
        {
            using (testEntities db = new testEntities())
            {
                #region user login
                //var userlogin = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == Context.User.Identity.Name);
                var userlogin = await (from ul in db.UserLogins
                                 where ul.UserNameCopy == Context.User.Identity.Name
                                 select new { ul.Id, ul.BrokerVIP, ul.UserNameCopy, ul.AvataImage, ul.DisableUser }).FirstOrDefaultAsync();
                if (userlogin == null || userlogin.DisableUser == true) // user khong tim thay hoac bi disable
                {
                    //await Clients.Caller.statusUser(0);//0: lockuser
                    throw new ApplicationException("false");                    
                }
                #endregion

                #region format message
                string messagedefault = "";
                string stockTag = ""; // dinh dang stock|stock|stock de tim co phieu lien quan
                messagedefault = post.Message;
                string[] listMessege = post.Message.Replace("\n", " <br> ").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string messageFromatHTML = "";
                foreach (var item in listMessege)
                {
                    if (item.Length > 0 && item.Length < 20)
                    {
                        if (item.IndexOf("$", 0, 1) != -1) // tag ma co phieu
                        {
                            string ticker = item.RemoveSpecialString().ToUpper(); //item.Replace("$", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Trim().ToUpper();
                            messageFromatHTML += "<b><a onclick=selectMe(event,\"#\") target='_blank' href='/ticker/" + ticker + "'>" + item + "</a></b>" + " ";                            
                        }
                        else if (item.IndexOf("@", 0, 1) != -1) // tag nguoi dung
                        {
                            string user = item.RemoveSpecialString().ToLower();
                            messageFromatHTML += "<a onclick=selectMe(event,\"#\") target='_blank' href='/" + user + "'>" + item + "</a>" + " ";
                        }
                        else if ((item.Contains("http") || item.Contains("www.")) && item.Length >= 4)
                        {
                            if (item.IndexOf("http", 0, 4) != -1 || item.IndexOf("www.", 0, 4) != -1)
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
                                messageFromatHTML += "Nguồn tại <a onclick=selectMe(event,\"#\") target='_blank' href='" + hostURL + "/" + tu.Id + "'>" + AppHelper.GetDomain(item) + "...</a>" + " ";
                            }
                            else
                            {
                                messageFromatHTML += item + " ";
                            }
                        }
                        else
                        {
                            messageFromatHTML += item + " ";
                        }
                    }
                }

                #endregion

                #region explan this passing messege to stockcode and username list
                //messageFromatHTML += "</a>";
            
                var listStock = new List<string>();
                var listUsersendMessege = new List<string>();
                List<string> listMessegeSplit = messagedefault.Replace("\n", " ").Split(' ').ToList().FindAll(p => p.Contains("$") || p.Contains("@"));

                #endregion

                #region gui message co phieu và user lien quan
                
                foreach (var item in listMessegeSplit)
                {
                    if (item.Length > 0)
                    {
                        string stockcode = item.RemoveSpecialString().ToUpper();
                        if (item.IndexOf("$", 0, 1) != -1 && !listStock.Contains(stockcode) && StockRealTimeTicker.CheckExistStock(stockcode)) // find the stock with $
                        {                            
                            stockTag += stockcode + "|";
                            listStock.Add(stockcode); // group of hub for client 
                        }
                        else if (item.IndexOf("@", 0, 1) != -1) //find the user with @
                        {
                            string user = item.RemoveSpecialString().ToLower();
                            var finduser = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == user);
                            if (finduser != null)
                            {
                                NotificationMessege nM = new NotificationMessege { UserPost = userlogin.Id, UserReciver = finduser.Id, PostId = post.PostId, NumNoti = 1, TypeNoti = "U", CreateDate = DateTime.Now, XemYN = true };
                                db.NotificationMesseges.Add(nM);
                                listUsersendMessege.Add(user);
                            }
                        }
                    }
                }
                #endregion

                #region luu vao db
                /* add stockrelate */
                post.Message = AppHelper.FilteringWord(messageFromatHTML); // Filteringword lọc từ khóa bậy
                post.PostedBy = userlogin.Id;
                post.PostedDate = DateTime.Now;
                post.NhanDinh = nhanDinh;
                post.SumLike = 0;
                if (!string.IsNullOrWhiteSpace(chartImage))
                {
                    post.ChartYN = true;
                    post.ChartImageURL = chartImage.Replace("?width=50&height=50&mode=crop", "");
                }
                post.StockPrimary = stockTag; // lấy các stock #tag cuối GAS!PAS
                try
                {
                    db.Posts.Add(post);
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
                    SumReply = 0,
                    BrkVip = userlogin.BrokerVIP
                };

                #endregion

                #region gui message
                if (listStock.Count > 0)
                {
                    await Clients.All.addPostGlobal(ret); // add message vào profile va home    
                }
                //else
                //{
                //    await Clients.Caller.addPostForUser();
                //}

                if (userpageid > 0) // gửi cho cùng 1 nhóm đag mở cùng 1 user page
                {
                    listStock.Add(userpageid.ToString());
                }
                
                await Clients.Groups(listStock).addPost(ret); // ad group co phieu lien quan

                if (listUsersendMessege.Count > 0)
                {
                    await Clients.Users(listUsersendMessege).MessegeOfUserPost(1); // gui tin bao cho user nao có @
                }

                #endregion
            }
        }
        [Authorize]
        public async Task AddReply(PostComment reply)
        {

            using (testEntities db = new testEntities())
            {
                #region user login
                //var userlogin = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == Context.User.Identity.Name);
                var userlogin = await (from ul in db.UserLogins
                                       where ul.UserNameCopy == Context.User.Identity.Name
                                       select new { ul.Id, ul.BrokerVIP, ul.UserNameCopy, ul.AvataImage, ul.DisableUser }).FirstOrDefaultAsync();
                if (userlogin == null || userlogin.DisableUser == true)
                {
                    return;
                }
                
                reply.CommentBy = userlogin.Id;
                reply.PostedDate = DateTime.Now;
                #endregion
                
                #region format message
                string messagedefault = "";
                messagedefault = reply.Message;
                string[] listMessege = reply.Message.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string messageFromatHTML = "";
                foreach (var item in listMessege)
                {
                    if (item.Length > 0 && item.Length < 20)
                    {
                        if (item.IndexOf("$", 0, 1) != -1)
                        {
                            string ticker = item.RemoveSpecialString().ToUpper();
                            messageFromatHTML += "<b><a onclick=selectMe(event,\"#\") target='_blank' href='/ticker/" + ticker + "'>" + item + "</a></b>" + " ";
                        }
                        else if (item.IndexOf("@", 0, 1) != -1)
                        {
                            string user = item.RemoveSpecialString().ToLower();
                            messageFromatHTML += "<a onclick=selectMe(event,\"#\") target='_blank' href='/" + user + "'>" + item + "</a>" + " ";
                        }
                        else if ((item.Contains("http") || item.Contains("www.")) && item.Length >= 4)
                        {
                            if (item.IndexOf("http", 0, 4) != -1 || item.IndexOf("www.", 0, 4) != -1)
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
                                messageFromatHTML += "Nguồn tại <a onclick=selectMe(event,\"#\") target='_blank' href='" + hostURL + "/" + tu.Id + "'>" + AppHelper.GetDomain(item) + "...</a>" + " ";
                            }
                            else
                            {
                                messageFromatHTML += item + " ";
                            }
                        }
                        else
                        {
                            messageFromatHTML += item + " ";
                        }
                    }
                }

                
                reply.Message = AppHelper.FilteringWord(messageFromatHTML); // lọc từ khóa                

                var listUsersendMessege = new List<string>();
                ///////////////////////////////////////////
                var getPost = db.Posts.Find(reply.PostedBy); // lay thong tin cua bài post đó
                // cap nhat tong so luong reply
                getPost.SumReply += 1;
                #endregion
                
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
                List<string> listMessegeSplit = messagedefault.Split(' ').ToList().FindAll(p => p.Contains("@"));
                foreach (var item in listMessegeSplit)
                {
                    if (item.IndexOf("@", 0, 1) != -1) //find the user with @
                    {
                        string user = item.RemoveSpecialString().ToLower();
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
                
                #region luu du lieu vao db
                db.PostComments.Add(reply);

                try
                {
                    db.Entry(getPost).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // erro thi van chay tiep

                }

                var ret = new
                {
                    ReplyMessage = reply.Message,
                    //PostedBy = post.PostedBy,
                    ReplyByName = userlogin.UserNameCopy,
                    ReplyByAvatar = string.IsNullOrEmpty(userlogin.AvataImage) == true ? ImageURLAvataDefault : ImageURLAvata + userlogin.AvataImage,
                    ReplyDate = reply.PostedDate,
                    ReplyId = reply.PostCommentsId,
                    PostCommentsId = reply.PostCommentsId,
                    BrkVip = userlogin.BrokerVIP
                };

                #endregion
                
                #region push message
                await Clients.Caller.addReply(ret); // gửi cho chính người đã reply (tạo trả lời bên dưới)
                if (listUsersendMessege.Count > 0)
                {
                    await Clients.Users(listUsersendMessege).MessegeOfUserPost(1); // gửi thông báo (1) liên quan những người trong list post đã có người comment
                }
                await Clients.All.newReplyNoti(reply.PostedBy); // +1 cho ai đang mở bài post đó
                #endregion
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