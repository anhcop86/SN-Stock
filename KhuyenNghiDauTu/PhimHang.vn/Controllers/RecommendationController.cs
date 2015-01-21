using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhimHang.Models;

namespace PhimHang.Controllers
{
    [Authorize]
    public class RecommendationController : Controller
    {
        private KNDTLocalConnection db = new KNDTLocalConnection();
        private StoxDataEntities dbstox = new StoxDataEntities();
        // GET: /Recommendation/
        public ActionResult BuyRecommend()
        {
            //ViewBag.PostBy = new SelectList(db.UserLogins, "Id", "KeyLogin");
            LoadInit();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BuyRecommend([Bind(Include = "ID,StockCode,BuyPrice,StockHoldingTime,TargetSell,Description,CreatedDate,PostBy")] BuyRecommendModel buyRecommendModel)
        {
            if (ModelState.IsValid)
            {
                var recommentToDb = new RecommendStock();

                recommentToDb.CreatedDate = DateTime.Now.Date;
                recommentToDb.CreatedModify = DateTime.Now;
                recommentToDb.PostBy =  db.UserLogins.FirstOrDefault(u=> u.UserNameCopy == User.Identity.Name).Id;
                recommentToDb.StockCode = buyRecommendModel.StockCode;
                recommentToDb.StockHoldingTime = buyRecommendModel.StockHoldingTime;
                recommentToDb.TargetSell = buyRecommendModel.TargetSell;
                recommentToDb.BuyPrice = buyRecommendModel.BuyPrice;
                recommentToDb.Description = buyRecommendModel.Description;
                recommentToDb.TYPERecommend = "MUA";
                recommentToDb.SumComment = 0;
                db.RecommendStocks.Add(recommentToDb);
                await db.SaveChangesAsync();
                return RedirectToAction("", "Home");
            }
            LoadInit();
            //ViewBag.PostBy = new SelectList(db.UserLogins, "Id", "KeyLogin", recommentToDb.PostBy);
            return View(buyRecommendModel);
        }
        private async Task LoadInit()
        {
            var listStock = (from s in dbstox.stox_tb_Company
                             where s.ExchangeID == 0 || s.ExchangeID == 1
                             orderby s.Ticker
                             select new
                             {
                                 Ticker = s.Ticker
                             }
                             ).ToList();
            ViewBag.listStockCode = new SelectList(listStock, "Ticker", "Ticker");

            var listTypeRecomendation = new List<dynamic>
                    { 
                        new { Id = "MUA", Name = "MUA" }
                        
                    }.ToList();

            ViewBag.listTypeRecomendation = new SelectList(listTypeRecomendation, "Id", "Name");

        }


        public async Task<ActionResult> Index()
        {

            var recommendstocks = db.RecommendStocks.Include(r => r.UserLogin);
            return View(await recommendstocks.ToListAsync());
        }

        // GET: /Recommendation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendStock recommendstock = await db.RecommendStocks.FindAsync(id);
            if (recommendstock == null)
            {
                return HttpNotFound();
            }
            return View(recommendstock);
        }

        // GET: /Recommendation/Create
        public ActionResult Create()
        {
            ViewBag.PostBy = new SelectList(db.UserLogins, "Id", "KeyLogin");
            return View();
        }

        // POST: /Recommendation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,StockCode,TYPERecommend,BuyPrice,StockHoldingTime,TargetSell,Description,CreatedDate,PostBy")] RecommendStock recommendstock)
        {
            if (ModelState.IsValid)
            {
                recommendstock.CreatedDate = DateTime.Now.Date;
                db.RecommendStocks.Add(recommendstock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PostBy = new SelectList(db.UserLogins, "Id", "KeyLogin", recommendstock.PostBy);
            return View(recommendstock);
        }

        // GET: /Recommendation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendStock recommendstock = await db.RecommendStocks.FindAsync(id);
            if (recommendstock == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostBy = new SelectList(db.UserLogins, "Id", "KeyLogin", recommendstock.PostBy);
            return View(recommendstock);
        }

        // POST: /Recommendation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,StockCode,TYPERecommend,BuyPrice,StockHoldingTime,TargetSell,Description,CreatedDate,PostBy")] RecommendStock recommendstock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recommendstock).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PostBy = new SelectList(db.UserLogins, "Id", "KeyLogin", recommendstock.PostBy);
            return View(recommendstock);
        }

        // GET: /Recommendation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendStock recommendstock = await db.RecommendStocks.FindAsync(id);
            if (recommendstock == null)
            {
                return HttpNotFound();
            }
            return View(recommendstock);
        }

        // POST: /Recommendation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RecommendStock recommendstock = await db.RecommendStocks.FindAsync(id);
            db.RecommendStocks.Remove(recommendstock);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //--------------------------SELL

        public ActionResult SellRecommend()
        {
            //ViewBag.PostBy = new SelectList(db.UserLogins, "Id", "KeyLogin");
            LoadInitSell();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SellRecommend([Bind(Include = "ID,StockCode,BuyPrice,Description")] SellRecommendModel sellRecommendModel)
        {
            if (ModelState.IsValid)
            {
                var recommentToDb = new RecommendStock();

                recommentToDb.CreatedDate = DateTime.Now.Date;
                recommentToDb.CreatedModify = DateTime.Now;
                recommentToDb.PostBy = db.UserLogins.FirstOrDefault(u => u.UserNameCopy == User.Identity.Name).Id;
                recommentToDb.StockCode = sellRecommendModel.StockCode;
                recommentToDb.Description = sellRecommendModel.Description;
                recommentToDb.BuyPrice = sellRecommendModel.BuyPrice;
                recommentToDb.TYPERecommend = "BAN";
                recommentToDb.SumComment = 0;
                db.RecommendStocks.Add(recommentToDb);
                await db.SaveChangesAsync();
                return RedirectToAction("", "Home");
            }
            LoadInitSell();
            //ViewBag.PostBy = new SelectList(db.UserLogins, "Id", "KeyLogin", recommentToDb.PostBy);
            return View(sellRecommendModel);
        }
        private async Task LoadInitSell()
        {
            var listStock = (from s in dbstox.stox_tb_Company
                             where s.ExchangeID == 0 || s.ExchangeID == 1
                             orderby s.Ticker
                             select new
                             {
                                 Ticker = s.Ticker
                             }
                             ).ToList();
            ViewBag.listStockCode = new SelectList(listStock, "Ticker", "Ticker");

            var listTypeRecomendation = new List<dynamic>
                    { 
                        new { Id = "BAN", Name = "BÁN" }
                        
                    }.ToList();

            ViewBag.listTypeRecomendation = new SelectList(listTypeRecomendation, "Id", "Name");

        }
        private const string ImageURLAvataDefault = "/img/avatar_default.jpg";
        public ActionResult Detail(int id)
        {
            ViewBag.AvataEmage = ImageURLAvataDefault;
            var recomment = db.RecommendStocks.FirstOrDefault(rs => rs.ID == id);
            ViewBag.IdRecommend = id;
            return View(recomment);
        }

        public async Task<dynamic> GetCommentFromId(int id)
        {
            //var result = db.Comments.Where(c => c.PostedBy == id).ToList();
            var ret = (from reply in db.Comments
                       where reply.PostedBy == id
                       orderby reply.PostedDate descending
                       select new
                       {
                           ReplyMessage = reply.Message,
                           ReplyByName = reply.UserLogin.UserNameCopy,
                           ReplyByAvatar = ImageURLAvataDefault + "?width=46&height=46&mode=crop",
                           ReplyDate = reply.PostedDate,
                           ReplyId = reply.CommentsId,
                       }).ToArray();


            //return Json(ret, JsonRequestBehavior.AllowGet);
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
            return result;
        }
        [HttpPost]
        public async Task<dynamic> AddNewComment(int idkn, string messege)
        {
            //var result = db.Comments.Where(c => c.PostedBy == id).ToList();
            var comment = new Comment();
            comment.Message = messege;
            comment.CommentBy = db.UserLogins.FirstOrDefault(ul => ul.UserNameCopy == User.Identity.Name).Id;
            comment.PostedBy = idkn;
            comment.PostedDate = DateTime.Now;

            RecommendStock recommendstock = await db.RecommendStocks.FindAsync(idkn);
            recommendstock.SumComment = recommendstock.SumComment + 1;
            db.Entry(recommendstock).State = EntityState.Modified;            

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



    }
}
