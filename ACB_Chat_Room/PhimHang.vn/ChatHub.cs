using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PhimHang.Models;

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

        public void SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.User.Identity.Name;
            var idUser = db.UserLogins.FirstOrDefault(u => u.UserNameCopy == fromUserId).Id;
            int i;
            if (int.TryParse(toUserId, out i)) // send group
            {
                var listGroup = db.Group_User.Where(gu => gu.GroupId == i && gu.UserOfGroup != idUser).ToList();
                var listUserInGroup = new List<string>();
                var GroupName = db.Groups.FirstOrDefault(g=>g.GroupId == i).GroupName;
                
                foreach (var item in listGroup)
                {
                    listUserInGroup.Add(item.UserLogin.UserNameCopy);
                    //GroupName += item.UserLogin.UserNameCopy + "|";
                }

                //listGroup.Where(u => !ListUserNameConnection.Contains(u.UserName)).ToList();


                Clients.Users(listUserInGroup).sendPrivateMessage(toUserId, fromUserId, message, "G", GroupName);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUserId, message, "G", GroupName );

            }
            else // send direct
            {
                // send to 
                Clients.User(toUserId).sendPrivateMessage(fromUserId, fromUserId, message, "P", fromUserId);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUserId, message, "P", fromUserId);
            }
            
            // gia su select dc 2 user trong group tu database
            
            //

            //var toUser = ConnectedUsers.FirstOrDefault(x => x.UserName == toUserId);
            //var fromUser = ConnectedUsers.FirstOrDefault(x => x.UserName == fromUserId);

                          
            

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


        #endregion
    }
}