using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loadi.Controllers
{
    public class Chia_Se_Loi_NhuanController : Controller
    {
        //
        // GET: /Chia_Se_Loi_Nhuan/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Chia_Se_Loi_Nhuan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Chia_Se_Loi_Nhuan/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Chia_Se_Loi_Nhuan/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Chia_Se_Loi_Nhuan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Chia_Se_Loi_Nhuan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Chia_Se_Loi_Nhuan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Chia_Se_Loi_Nhuan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
