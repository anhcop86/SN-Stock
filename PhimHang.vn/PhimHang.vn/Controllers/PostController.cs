using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhimHang.vn.Models;

namespace PhimHang.vn.Controllers
{
    public class PostController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: /Post/
        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(p => p.AspNetUser);
            return View(await posts.ToListAsync());
        }

        // GET: /Post/Details/5
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

        // GET: /Post/Create
        public ActionResult Create()
        {
            ViewBag.PostedBy = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: /Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="PostId,Message,PostedBy,PostedDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.NhanDinh = 1;
                post.ChartImageURL = "fdasfdsaf";
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PostedBy = new SelectList(db.AspNetUsers, "Id", "UserName", post.PostedBy);
            return View(post);
        }

        // GET: /Post/Edit/5
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
            ViewBag.PostedBy = new SelectList(db.AspNetUsers, "Id", "UserName", post.PostedBy);
            return View(post);
        }

        // POST: /Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="PostId,Message,PostedBy,PostedDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.NhanDinh = 1;
                post.ChartImageURL = "fdasfdsaf";
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PostedBy = new SelectList(db.AspNetUsers, "Id", "UserName", post.PostedBy);
            return View(post);
        }

        // GET: /Post/Delete/5
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

        // POST: /Post/Delete/5
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
