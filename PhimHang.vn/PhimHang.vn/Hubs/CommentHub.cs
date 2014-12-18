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
using PhimHang.Models;

namespace PhimHang.Hubs
{

    public class CommentHub : Hub
    {
         
        // GET: /Post/
        
        private const string ImageURLAvataDefault = "img/avatar_default.jpg";
        private const string ImageURLAvata = "images/avatar/";

        public void GetPosts(string stockCurrent)
        {
            using (testEntities db = new testEntities())
            {
                var ret = (from stockrelate in db.StockRelates.ToList()
                           where stockrelate.StockCodeRelate == stockCurrent
                           orderby stockrelate.Post.PostedDate descending
                           select new
                           {
                               Message = stockrelate.Post.Message,
                               PostedBy = stockrelate.Post.PostedDate,
                               PostedByName = stockrelate.Post.UserLogin.FullName,
                               PostedByAvatar = string.IsNullOrEmpty(stockrelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockrelate.Post.UserLogin.AvataImage,
                               PostedDate = stockrelate.Post.PostedDate,
                               PostId = stockrelate.PostId
                           }).ToArray();

                Clients.Group(stockCurrent).loadPosts(ret);

            }            
        }
    }
}