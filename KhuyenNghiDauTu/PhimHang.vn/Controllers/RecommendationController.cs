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
using System.IO;

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
        private const string ImageURLAvata = "/Chart/";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BuyRecommend([Bind(Include = "ID,StockCode,BuyPrice,StockHoldingTime,TargetSell,Description,CreatedDate,PostBy,ChartImange")] BuyRecommendModel buyRecommendModel)
        {
            if (ModelState.IsValid)
            {
                var uploadDir = ImageURLAvata;
                var geraralFileName = User.Identity.Name + DateTime.Now.ToString("yyyyMMddHHmmss") + "_chart";
                string NameFiletimeupload = geraralFileName;
                if (buyRecommendModel.ChartImange!=null)
                {
                    #region upload file                    
                    
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), NameFiletimeupload + Path.GetExtension(buyRecommendModel.ChartImange.FileName));
                    buyRecommendModel.ChartImange.SaveAs(imagePath);
                    #endregion    
                }
                

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
                if (buyRecommendModel.ChartImange != null)
                {
                    recommentToDb.ImageUrl = uploadDir + NameFiletimeupload + Path.GetExtension(buyRecommendModel.ChartImange.FileName);    
                }
                
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
            if (User.Identity.Name != "long.thai")
            {
                return RedirectToAction("", "");
            }
            var commentrecommendstock = db.Comments.Where(cm => cm.PostedBy == id).ToList();
            RecommendStock recommendstock = await db.RecommendStocks.FindAsync(id);

            if (recommendstock == null)
            {
                return HttpNotFound();
            }
            db.Comments.RemoveRange(commentrecommendstock);
            db.RecommendStocks.Remove(recommendstock);
            await db.SaveChangesAsync();


            return RedirectToAction("ModifyRecommend" , "Home");
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
        public async Task<ActionResult> SellRecommend([Bind(Include = "ID,StockCode,BuyPrice,Description,ChartImange")] SellRecommendModel sellRecommendModel)
        {
            if (ModelState.IsValid)
            {
                var uploadDir = ImageURLAvata;
                var geraralFileName = User.Identity.Name + DateTime.Now.ToString("yyyyMMddHHmmss") + "_chart";
                string NameFiletimeupload = geraralFileName;
                if (sellRecommendModel.ChartImange != null)
                {
                    #region upload file

                    var imagePath = Path.Combine(Server.MapPath(uploadDir), NameFiletimeupload + Path.GetExtension(sellRecommendModel.ChartImange.FileName));
                    sellRecommendModel.ChartImange.SaveAs(imagePath);
                    #endregion
                }
                var recommentToDb = new RecommendStock();

                recommentToDb.CreatedDate = DateTime.Now.Date;
                recommentToDb.CreatedModify = DateTime.Now;
                recommentToDb.PostBy = db.UserLogins.FirstOrDefault(u => u.UserNameCopy == User.Identity.Name).Id;
                recommentToDb.StockCode = sellRecommendModel.StockCode;
                recommentToDb.Description = sellRecommendModel.Description;
                recommentToDb.BuyPrice = sellRecommendModel.BuyPrice;
                recommentToDb.TYPERecommend = "BAN";
                recommentToDb.SumComment = 0;
                if (sellRecommendModel.ChartImange != null)
                {
                    recommentToDb.ImageUrl = uploadDir + NameFiletimeupload + Path.GetExtension(sellRecommendModel.ChartImange.FileName);
                }
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
            // thong tin stox tu database stox
            var maxdate = (from s in dbstox.stox_tb_Ratio where s.Ticker == recomment.StockCode select s.UpdateDate).Max();
            
            var stoxinfo = (from si in dbstox.stox_tb_Ratio
                            where si.Ticker == recomment.StockCode
                            && (si.UpdateDate == maxdate)
                            select new
                            {
                                KLCPLH = si.F5_1b,
                                PE = si.F5_50,
                                PB = si.F5_54,
                                EPS = si.F5_11
                            }).FirstOrDefault();

            var stoxCompany = (from sc in dbstox.stox_tb_Company
                               where sc.Ticker == recomment.StockCode
                               select new
                               {
                                   KLNY = sc.ShareIssue
                               }).FirstOrDefault();

            ViewBag.KLCPLH = stoxinfo.KLCPLH;
            ViewBag.PE = stoxinfo.PE;
            ViewBag.PB = stoxinfo.PB;
            ViewBag.EPS = stoxinfo.EPS;
            ViewBag.KLNY = stoxCompany.KLNY;

            var phien_date_hose_10 = (from ht in dbstox.stox_tb_HOSE_Trading
                                      orderby ht.DateReport descending
                                      where ht.StockSymbol == recomment.StockCode
                                      select new
                                      {                                          
                                          NumberShare = ht.Totalshare
                                      }).Take(10).ToList();

            var phien_date_hnx_10 = (from ht in dbstox.stox_tb_StocksInfo
                                      orderby ht.dateReport descending
                                      where ht.code == recomment.StockCode
                                      select new
                                      {                                          
                                          NumberShare = ht.total_trading_qtty
                                      }).Take(10).ToList();
            if (phien_date_hose_10.Count > 0)
            {
                ViewBag.BQ10Phien = phien_date_hose_10.Average(m => m.NumberShare);
            }
            else
            {
                ViewBag.BQ10Phien = phien_date_hnx_10.Average(m => m.NumberShare);
            }
            //
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

        public async Task<dynamic> GetInfoStoxByAjax(string selectStockCode)
        {
            if (!string.IsNullOrWhiteSpace(selectStockCode))
            {


                // thong tin stox tu database stox
                var maxdate = (from s in dbstox.stox_tb_Ratio where s.Ticker == selectStockCode select s.UpdateDate).Max();

                var stoxinfo = (from si in dbstox.stox_tb_Ratio
                                where si.Ticker == selectStockCode
                                && (si.UpdateDate == maxdate)
                                select new
                                {
                                    KLCPLH = si.F5_1b,
                                    PE = si.F5_50,
                                    PB = si.F5_54,
                                    EPS = si.F5_11
                                }).FirstOrDefault();

                var stoxCompany = (from sc in dbstox.stox_tb_Company
                                   where sc.Ticker == selectStockCode
                                   select new
                                   {
                                       KLNY = sc.ShareIssue
                                   }).FirstOrDefault();

                var KLCPLH = stoxinfo.KLCPLH;
                var PE = stoxinfo.PE;
                var PB = stoxinfo.PB;
                var EPS = stoxinfo.EPS;
                var KLNY = stoxCompany.KLNY;

                var phien_date_hose_10 = (from ht in dbstox.stox_tb_HOSE_Trading
                                          orderby ht.DateReport descending
                                          where ht.StockSymbol == selectStockCode
                                          select new
                                          {
                                              NumberShare = ht.Totalshare
                                          }).Take(10).ToList();

                var phien_date_hnx_10 = (from ht in dbstox.stox_tb_StocksInfo
                                         orderby ht.dateReport descending
                                         where ht.code == selectStockCode
                                         select new
                                         {
                                             NumberShare = ht.total_trading_qtty
                                         }).Take(10).ToList();
                dynamic BQ10Phien = 0;
                if (phien_date_hose_10.Count > 0)
                {
                    BQ10Phien = phien_date_hose_10.Average(m => m.NumberShare);
                }
                else
                {
                    BQ10Phien = phien_date_hnx_10.Average(m => m.NumberShare);
                }
                var ret = new
                {
                    KLCPLH = string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N0}", KLCPLH),
                    PE = string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N2}", PE),
                    PB = string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N2}", PB),
                    EPS = string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N0}", EPS),
                    KLNY = string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N0}",KLNY),
                    BQ10Phien = string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N0}",BQ10Phien)
                };

                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ret = new
                {
                    KLCPLH = "Chọn mã CP",
                    PE = "Chọn mã CP",
                    PB = "Chọn mã CP",
                    EPS = "Chọn mã CP",
                    KLNY = "Chọn mã CP",
                    BQ10Phien = "Chọn mã CP",
                };

                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
