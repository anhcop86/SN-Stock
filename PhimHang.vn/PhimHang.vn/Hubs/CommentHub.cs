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
                var ret = (from stockRelate in db.StockRelates.ToList()
                           where stockRelate.StockCodeRelate == stockCurrent
                           orderby stockRelate.Post.PostedDate descending
                           select new
                           {
                               Message = stockRelate.Post.Message,
                               PostedBy = stockRelate.Post.PostedDate,
                               PostedByName = stockRelate.Post.UserLogin.FullName,
                               PostedByAvatar = string.IsNullOrEmpty(stockRelate.Post.UserLogin.AvataImage) ? ImageURLAvataDefault : ImageURLAvata + stockRelate.Post.UserLogin.AvataImage,
                               PostedDate = stockRelate.Post.PostedDate,
                               PostId = stockRelate.PostId
                           }).ToArray();

                Clients.Group(stockCurrent).loadPosts(ret);

            }            
        }
    }
}