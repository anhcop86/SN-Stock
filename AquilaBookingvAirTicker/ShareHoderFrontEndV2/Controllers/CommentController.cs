using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;

namespace ShareHoderFrontEndV2.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        //
        // GET: /Comment/

        public ActionResult Index()
        {
            return View();
        }
        private const string ImageURLAvataDefault = "/images/avatar_default.jpg";
        [HttpGet]
        [AllowAnonymous]
        public dynamic GetCommentFromId(int id)
        {
            IHotelCommentRepository<HotelComment> irHComment = new HotelCommentRepository();
            //var result = db.Comments.Where(c => c.PostedBy == id).ToList();
            var ret = (from reply in irHComment.getListHotelCommentFromHotelId(id)                       
                       orderby reply.CommentDate descending
                       select new
                       {
                           ReplyMessage = reply.comment,
                           ReplyByName = reply.MemberId,
                           ReplyByAvatar = ImageURLAvataDefault + "?width=46&height=46&mode=crop",
                           ReplyDate = reply.CommentDate,
                           ReplyId = reply.CommentId,
                       }).ToArray();


            //return Json(ret, JsonRequestBehavior.AllowGet);
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            return result;
        }
        /*
         [HttpPost]
        public dynamic AddNewComment(int idkn, string messege)
        {
            //var result = db.Comments.Where(c => c.PostedBy == id).ToList();
            var comment = new HotelComment();
            comment.comment = messege;
            comment.MemberId = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == User.Identity.Name).Id;
            comment.PostedBy = idkn;
            comment.PostedDate = DateTime.Now;

                      

            try
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            catch (Exception)
            {
                
                throw;
            }
            var ret = (from reply in db.Comments
                       where reply.CommentsId == comment.CommentsId
                       orderby reply.PostedDate ascending
                       select new
                       {
                           ReplyMessage = reply.Message,
                           ReplyByName = reply.UserLogin.UserNameCopy,
                           ReplyByAvatar = ImageURLAvataDefault + "?width=46&height=46&mode=crop",
                           ReplyDate = reply.PostedDate,
                           ReplyId = reply.CommentsId,
                       }).ToArray();


            var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            return result;
        }
        */
    }
}
