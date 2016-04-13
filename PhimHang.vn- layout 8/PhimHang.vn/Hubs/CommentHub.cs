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
    /// <summary>
    /// Hub to connect these majob client to create a global group (Tạo hub để kết nối các client thành 1 nhóm)
    /// </summary>
    [HubName("CommentHub")]    
    public class CommentHub : Hub
    {         
        // GET: /Post/
        //private const string ImageURLAvataDefault = "/img/avatar2.jpg";
        //private const string ImageURLAvata = "/images/avatar/";
        private string hostURL = AppHelper.TinyURL;
        testEntities db;
        TinyURLEntities dbtinyURL = new TinyURLEntities();
        /// <summary>
        /// Create A new post from the user
        /// </summary>
        /// <param name="post"></param>
        /// <param name="nhanDinh"></param>
        /// <param name="chartImage"></param>
        /// <param name="userpageid"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<string> AddPost(Post post,  byte nhanDinh, string chartImage, long? userpageid)
        {
            using (db = new testEntities())
            {
                #region user login
                string resultString = string.Empty;
                // get info login (Lấy thông tin đăng nhập)
                var userlogin = await (from ul in db.UserLogins
                                 where ul.UserNameCopy == Context.User.Identity.Name
                                 select new { ul.Id, ul.BrokerVIP, ul.UserNameCopy, ul.AvataImage, ul.DisableUser }).FirstOrDefaultAsync();
                if (userlogin == null || userlogin.DisableUser == true) // user khong tim thay hoac bi disable
                {                    
                    return resultString = "L"; // user is disable
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
                    if (item.Length > 0 && item.Length < 2000)
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
                string replyRelated = string.Empty;
                string replyRelatedUser = string.Empty; 
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
                            var finduser = await db.UserLogins.FirstOrDefaultAsync(ul => ul.UserNameCopy == user);
                            if (finduser != null)
                            {                                
                                NotificationMessege nM = new NotificationMessege { UserPost = userlogin.Id, UserReciver = finduser.Id, PostId = post.PostId, NumNoti = 1, TypeNoti = "U", CreateDate = DateTime.Now, XemYN = true };
                                db.NotificationMesseges.Add(nM);
                                listUsersendMessege.Add(user); // add user to send notification
                                replyRelated += user + "|";
                                replyRelatedUser += finduser.Id + "|";
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
                post.ReplyRelated = replyRelated; // thong bao cho toan user voi tag user|user|user
                post.ReplyRelatedUser = replyRelatedUser; // thong bao cho toan user voi tag id|id|id
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
                    PostedByAvatar = string.IsNullOrEmpty(userlogin.AvataImage) == true ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + userlogin.AvataImage,
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
                    resultString = "S"; // post thanh cong len trang home va profile
                }
                else
                {
                    resultString = "O"; // chi post o trang ca nhan
                }

                if (userpageid > 0) // gửi cho cùng 1 nhóm đag mở cùng 1 user page
                {
                    listStock.Add(userpageid.ToString());
                }
                
                await Clients.Groups(listStock).addPost(ret); // ad group co phieu lien quan

                if (listUsersendMessege.Count > 0)
                {
                    await Clients.Users(listUsersendMessege).MessegeOfUserPost(1); // gui tin bao cho user nao có @
                }
                return resultString;
                #endregion
            }
        }
        /// <summary>
        /// Reply a post
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<string> AddReply(PostComment reply)
        {

            using (db = new testEntities())
            {
                #region user login
                //string resultString = string.Empty;
                //var userlogin = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == Context.User.Identity.Name);
                var userlogin = await (from ul in db.UserLogins
                                       where ul.UserNameCopy == Context.User.Identity.Name
                                       select new { ul.Id, ul.BrokerVIP, ul.UserNameCopy, ul.AvataImage, ul.DisableUser }).FirstOrDefaultAsync();
                if (userlogin == null || userlogin.DisableUser == true)
                {
                    return "L"; // user is disable
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
                    if (item.Length > 0 && item.Length < 2000)
                    {
                        if (item.IndexOf("$", 0, 1) != -1)
                        {
                            string ticker = item.RemoveSpecialString().ToUpper();
                            messageFromatHTML += "<b><a target='_blank' href='/ticker/" + ticker + "'>" + item + "</a></b>" + " ";
                        }
                        else if (item.IndexOf("@", 0, 1) != -1)
                        {
                            // đề cập đến user
                            string user = item.RemoveSpecialString().ToLower();
                            messageFromatHTML += "<a target='_blank' href='/" + user + "'>" + item + "</a>" + " ";
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
                #endregion

                #region reply có đề cập đến user nào không ??
                var getPost = await db.Posts.FindAsync(reply.PostedBy); // get post (Lấy bài post)
                // cap nhat tong so luong reply
                getPost.SumReply += 1;
                // Tao Array for split user (Tạo mảng để xử lý User)
                string[] userReplyRelated = new string[2];
                userReplyRelated[0] = getPost.ReplyRelated;
                userReplyRelated[1] = getPost.ReplyRelatedUser;

                List<string> listUsersendMessege = new List<string>();
                List<string> listMessegeSplit = messagedefault.Split(' ').ToList().FindAll(p => p.Contains("@"));
                foreach (var item in listMessegeSplit)
                {
                    if (item.IndexOf("@", 0, 1) != -1) //find the user with @
                    {
                        string user = item.RemoveSpecialString().ToLower(); // remove special character (Loại những ký tự đặc biệt)
                        var finduser = await db.UserLogins.FirstOrDefaultAsync(ul => ul.UserNameCopy == user);// check user exist
                        if (finduser != null)
                        {
                            userReplyRelated = AppHelper.StringUserSlipt(userReplyRelated, user, finduser.Id.ToString());
                        }
                    }
                }
                #endregion

                #region gui tin cho những người đã đề cập trong bài post

                // replace chính người post để không tạo thêm notification
                string[] replyRelated = userReplyRelated[0].Replace(userlogin.UserNameCopy + "|", "").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] replyRelatedIdUser = userReplyRelated[1].Replace(userlogin.Id + "|", "").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < replyRelatedIdUser.Length; i++)
                {
                    try
                    {
                        int replyRelatedIdUserItem = int.Parse(replyRelatedIdUser[i]);
                        var nMRecive = await db.NotificationMesseges.FirstOrDefaultAsync(nm => nm.UserReciver == replyRelatedIdUserItem && nm.PostId == reply.PostedBy);
                        if (nMRecive == null)
                        {
                            NotificationMessegeModels.Create(userlogin.Id, int.Parse(replyRelatedIdUser[i]), reply.PostedBy);
                            listUsersendMessege.Add(replyRelated[i]); // add usser to send (1) notify
                        }
                        else
                        {
                            if (nMRecive.NumNoti <= 0 || nMRecive.NumNoti == null)
                            {
                                NotificationMessegeModels.Update(nMRecive);
                                listUsersendMessege.Add(replyRelated[i]); // add usser to send (1) notify
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //throw log;
                    }
                }

                ///////////////////////////////////////////
                #endregion
                
                #region luu du lieu vao db
                db.PostComments.Add(reply);

                try
                {
                    //AppHelper.StringUserSlipt(userReplyRelated, userlogin.UserNameCopy, userlogin.Id.ToString())[0];
                    getPost.ReplyRelated = AppHelper.StringUserSlipt(userReplyRelated, userlogin.UserNameCopy, userlogin.Id.ToString())[0];
                    getPost.ReplyRelatedUser = AppHelper.StringUserSlipt(userReplyRelated, userlogin.UserNameCopy, userlogin.Id.ToString())[1];
                    db.Entry(getPost).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // error thi van chay tiep

                }

                var ret = new
                {
                    ReplyMessage = reply.Message,
                    //PostedBy = post.PostedBy,
                    ReplyByName = userlogin.UserNameCopy,
                    ReplyByAvatar = string.IsNullOrEmpty(userlogin.AvataImage) == true ? AppHelper.ImageURLAvataDefault : AppHelper.ImageURLAvata + userlogin.AvataImage,
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
                return "S";
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