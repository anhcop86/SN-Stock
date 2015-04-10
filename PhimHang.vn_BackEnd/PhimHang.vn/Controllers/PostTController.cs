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
    public class PostTController : Controller
    {
        private db_cungphim_FrontEnd db = new db_cungphim_FrontEnd();

        // GET: /PostT/
        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(p => p.UserLogin);
            return View(await posts.ToListAsync());
        }

        // GET: /PostT/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: /PostT/Create
        public ActionResult Create()
        {
            ViewBag.PostedBy = new SelectList(db.UserLogins, "Id", "KeyLogin");
            return View();
        }

        // POST: /PostT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="PostId,Message,PostedBy,PostedDate,ChartImageURL,NhanDinh,Vir,StockPrimary,ChartYN,SumLike,SumReply")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PostedBy = new SelectList(db.UserLogins, "Id", "KeyLogin", post.PostedBy);
            return View(post);
        }

        // GET: /PostT/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostedBy = new SelectList(db.UserLogins, "Id", "KeyLogin", post.PostedBy);
            return View(post);
        }

        // POST: /PostT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="PostId,Message,PostedBy,PostedDate,ChartImageURL,NhanDinh,Vir,StockPrimary,ChartYN,SumLike,SumReply")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PostedBy = new SelectList(db.UserLogins, "Id", "KeyLogin", post.PostedBy);
            return View(post);
        }

        // GET: /PostT/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /PostT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
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
