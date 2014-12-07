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
    public class FollowStockController : Controller
    {
        private testEntities db = new testEntities();

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,UserId,StockFollowed,CreatedDate")] FollowStock followstock)
        {
            if (ModelState.IsValid)
            {
                db.FollowStocks.Add(followstock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserLogins, "Id", "KeyLogin", followstock.UserId);
            return View(followstock);
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
