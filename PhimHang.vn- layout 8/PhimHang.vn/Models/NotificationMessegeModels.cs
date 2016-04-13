using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    
    public static class NotificationMessegeModels
    {
        private static testEntities db;
        #region Porperties
        #endregion

        #region Action
        /// <summary>
        /// Create NotificationMessege
        /// </summary>
        /// <param name="userlogin"></param> user's Logging
        /// <param name="userReciver"></param> user reciver messege
        ///  <param name="postedBy"></param> post id
        public static void Create(int userlogin, int userReciver, long postedBy)
        {
            using (db = new testEntities())
            {
                NotificationMessege notificationMessege = new NotificationMessege();
                notificationMessege.UserPost = userlogin;
                notificationMessege.UserReciver = userReciver;
                notificationMessege.PostId = postedBy; 
                // default value of new notification
                notificationMessege.NumNoti = 1;
                notificationMessege.TypeNoti = "R";
                notificationMessege.CreateDate = DateTime.Now;
                notificationMessege.XemYN = true;
                db.NotificationMesseges.Add(notificationMessege);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    
                }
            }
        }

        public static  void Update(NotificationMessege notificationMessege)
        {
            using (db = new testEntities())
            {
                notificationMessege.NumNoti += 1;
                notificationMessege.XemYN = true;
                notificationMessege.CreateDate = DateTime.Now;
                
                try
                {
                    db.Entry(notificationMessege).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }

            }
        }
        #endregion
    }
}