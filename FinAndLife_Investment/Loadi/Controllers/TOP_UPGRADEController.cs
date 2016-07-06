using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loadi.Controllers
{
    public class TOP_UPGRADEController : Controller
    {
        //
        // GET: /TOP_UPGRADE/danh_muc_top_upgrape
        public ActionResult danh_muc_top_upgrape()
        {
            ViewBag.Title = "Danh mục top upgrape";
            return View();
        }
        //
        // GET: /TOP_UPGRADE/co_che_hop_tac
        public ActionResult co_che_hop_tac()
        {
            ViewBag.Title = "Cơ chế hợp tác";
            return View();
        }
        // GET: /TOP_UPGRADE/hieu_qua_dau_tu
        public ActionResult hieu_qua_dau_tu()
        {
            ViewBag.Title = "Hiệu quả đầu tư";

            return View();
        }
        // GET: /TOP_UPGRADE/hieu_qua_dau_tu
        public ActionResult uu_diem()
        {
            ViewBag.Title = "Ưu điểm";

            return View();
        }
        //
        // GET: /TOP_UPGRADE/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TOP_UPGRADE/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TOP_UPGRADE/Create
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
        // GET: /TOP_UPGRADE/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TOP_UPGRADE/Edit/5
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
        // GET: /TOP_UPGRADE/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TOP_UPGRADE/Delete/5
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
