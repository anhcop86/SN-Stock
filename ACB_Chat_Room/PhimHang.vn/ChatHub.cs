using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PhimHang.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PhimHang
{
    public class ChatHub : Hub
    {
        #region Data Members

        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        #endregion

        #region Methods
        DBChatGroup db = new DBChatGroup();
        public void Connect()
        {
            var userName = Context.User.Identity.Name;
            var id = Context.ConnectionId;

        
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0) // check exist user
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName });
                // list user
                var userInDbs = (from u in db.UserLogins
                                 orderby u.UserNameCopy                                 
                                 select new
                                 {
                                     UserName = u.UserNameCopy,
                                     UserId = u.Id
                                     
                                 }).ToList();
                var ListUserNameConnection = new List<string>();
                 ConnectedUsers.ForEach(cu=> ListUserNameConnection.Add(cu.UserName));

                var userStatusOnline = userInDbs.Where(u => ListUserNameConnection.Contains(u.UserName)).ToList();
                var userStatusOffline = userInDbs.Where(u => !ListUserNameConnection.Contains(u.UserName)).ToList();

                // list recent
                var idUser = db.UserLogins.FirstOrDefault(u=> u.UserNameCopy == userName).Id;
                var listGroup = (from g in db.Group_User
                                 where g.UserOfGroup == idUser
                                 select new
                                 {
                                    UserId = g.GroupId,
                                    UserName = g.Group.GroupName
                                 }).Distinct().ToList();

                // send to caller
                Clients.Caller.onConnected(id, userName, userStatusOnline, "O"); // List ONLine
                Clients.Caller.onConnected(id, userName, userStatusOffline,"F"); // List OFFLine

                Clients.Caller.onConnected(id, userName, listGroup, "G"); // List Group

                // send to all except caller client switch online
                Clients.AllExcept(id).onNewUserConnected(id, userName); 

            }

        }

        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            AddMessageinCache(userName, message);

            // Broad cast message
            Clients.All.messageReceived(userName, message);
        }

        public async Task SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.User.Identity.Name;
            var idFromUser = db.UserLogins.FirstOrDefault(u => u.UserNameCopy == fromUserId).Id;            
            int i;
            if (int.TryParse(toUserId, out i)) // send group
            {
                var listGroup = db.Group_User.Where(gu => gu.GroupId == i && gu.UserOfGroup != idFromUser).ToList();
                var listUserInGroup = new List<string>();
                var GroupName = db.Groups.FirstOrDefault(g => g.GroupId == i).GroupName;

                foreach (var item in listGroup)
                {
                    listUserInGroup.Add(item.UserLogin.UserNameCopy);
                    //GroupName += item.UserLogin.UserNameCopy + "|";
                    var group = db.StatusWindows.FirstOrDefault(sw => sw.KeyWindowName == toUserId && sw.UserName == item.UserLogin.UserNameCopy);
                    if (group == null)
                    {
                        StatusWindow sw = new StatusWindow
                        {
                            CtrId = "Group_" + i,
                            WindowName = GroupName,
                            TopPosition = "200" + "px",
                            LeftPosition = "100" + "px",
                            UserName = item.UserLogin.UserNameCopy,
                            KeyWindowName = toUserId

                        };
                        db.StatusWindows.Add(sw);
                        await db.SaveChangesAsync();
                    }
                }

             
                Group_User_Messege group_User_Messege = new Group_User_Messege
                {
                    ConentMesseger = message,
                    CreatedDate = DateTime.Now,
                    GroupId = i,
                    WhoChat = fromUserId
                };

                db.Group_User_Messege.Add(group_User_Messege);
                await db.SaveChangesAsync();

                Clients.Users(listUserInGroup).sendPrivateMessage(toUserId, fromUserId, message, "G", GroupName);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUserId, message, "G", GroupName);

            }
            else // send direct messenge
            {
                var idToUser = db.UserLogins.FirstOrDefault(u => u.UserNameCopy == toUserId).Id;                
                MessegeDirect messegeDirect = new MessegeDirect
                {
                    ConentMesseger = message,
                    CreatedDate = DateTime.Now,
                    FromUser = idFromUser,
                    ToUser = idToUser,
                    WhoChat = fromUserId
                };

                db.MessegeDirects.Add(messegeDirect);
                await db.SaveChangesAsync();
                // send to 

                Clients.User(toUserId).sendPrivateMessage(fromUserId, fromUserId, message, "P", fromUserId);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUserId, message, "P", fromUserId);
            }            
          // tạo cua so khi khong nhan dc  tin nhắn
            //var checkesistwindow = db.StatusWindows.Where(w => w.CtrId == ctrId && w.UserName == User.Identity.Name).ToList();
            

        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCall)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }

            return base.OnDisconnected(stopCall);
        }


        #endregion

        #region private Messages

        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        public void CreateGroup_User(string[] listUser, string groupName)
        {
            if (listUser != null)
            {
                string myUserId = Context.User.Identity.Name; // tạo group phải có mình trong đó
                string groupnameDefault = "";
                string NameFirstsend = "";

                foreach (var item in listUser)
                {
                    groupnameDefault += item + "|";
                    NameFirstsend += item + "|";
                }

                groupnameDefault += myUserId;

                Group group = new Group();
                group.GroupName = string.IsNullOrWhiteSpace(groupName) ? groupnameDefault : groupName;
                group.GroupType = "G";
                db.Groups.Add(group);

                foreach (var item in listUser)
                {
                    Group_User userGroup = new Group_User();
                    userGroup.GroupId = group.GroupId;
                    userGroup.UserOfGroup = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == item).Id;
                    db.Group_User.Add(userGroup);
                }

                Group_User myGroup = new Group_User();
                myGroup.GroupId = group.GroupId;
                myGroup.UserOfGroup = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == myUserId).Id;
                db.Group_User.Add(myGroup);

                try
                {
                    db.SaveChanges();

                }
                catch (Exception)
                {

                    throw;
                }

                SendPrivateMessage(group.GroupId.ToString(), myUserId + " Đã thêm " + NameFirstsend + " vào nhóm ");

            }

        }

        public async Task CloseWindow(string groupid, string username)
        {
            var group = db.StatusWindows.FirstOrDefault(sw => sw.KeyWindowName == groupid && sw.UserName == username);
            db.StatusWindows.Remove(group);
            await db.SaveChangesAsync();
        }

      



        #endregion

        #region load history messege

        #endregion
    }
}