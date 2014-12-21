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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace PhimHang.Controllers
{
    [Authorize]
    public class FollowStockController : Controller
    {
        private testEntities db = new testEntities();
        public FollowStockController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }
        public FollowStockController(UserManager<ApplicationUser> userManager)
        {            
            UserManager = userManager;
        }
       
        public UserManager<ApplicationUser> UserManager { get; private set; }

        // GET: /FollowStock/
        public async Task<ActionResult> Index()
        {
            var followstocks = db.FollowStocks.Include(f => f.UserLogin);
            return View(await followstocks.ToListAsync());
        }

        // GET: /FollowStock/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowStock followstock = await db.FollowStocks.FindAsync(id);
            if (followstock == null)
            {
                return HttpNotFound();
            }
            return View(followstock);
        }

        // GET: /FollowStock/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserLogins, "Id", "KeyLogin");
            return View();
        }

        // POST: /FollowStock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        
        public async Task<string> Create(string stock)
        {
            
            using (db = new testEntities())
            {
                ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                
                var countStockFollowr = await db.FollowStocks.CountAsync(f => f.UserId == currentUser.UserExtentLogin.Id );
                var followrStockByUser = await db.FollowStocks.FirstOrDefaultAsync(f => f.UserId == currentUser.UserExtentLogin.Id && f.StockFollowed == stock);
                if (countStockFollowr >= 10 && followrStockByUser == null)
                {
                    return "M";
                }
                
                if (followrStockByUser == null && countStockFollowr < 10)
                {
                    var stockfollow = new FollowStock { UserId = currentUser.UserExtentLogin.Id, StockFollowed = stock , CreatedDate = DateTime.Now};
                    db.FollowStocks.Add(stockfollow);
                    await db.SaveChangesAsync();
                    return "A";
                }
                else 
                {
                    db.FollowStocks.Remove(followrStockByUser);
                    await db.SaveChangesAsync();
                    return "R";
                }

                
            }

                
            

            
            return "";
        }

        // GET: /FollowStock/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowStock followstock = await db.FollowStocks.FindAsync(id);
            if (followstock == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserLogins, "Id", "KeyLogin", followstock.UserId);
            return View(followstock);
        }

        // POST: /FollowStock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,UserId,StockFollowed,CreatedDate")] FollowStock followstock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(followstock).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserLogins, "Id", "KeyLogin", followstock.UserId);
            return View(followstock);
        }

        // GET: /FollowStock/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowStock followstock = await db.FollowStocks.FindAsync(id);
            if (followstock == null)
            {
                return HttpNotFound();
            }
            return View(followstock);
        }

        // POST: /FollowStock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            FollowStock followstock = await db.FollowStocks.FindAsync(id);
            db.FollowStocks.Remove(followstock);
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
    }
}
