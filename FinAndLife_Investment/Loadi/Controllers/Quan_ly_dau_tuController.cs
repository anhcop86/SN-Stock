using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loadi.Controllers
{
    public class Quan_ly_dau_tuController : Controller
    {
        //
        // GET: /Quan_ly_dau_tu/
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("quan_ly_dau_tu_la_gi");
        }
        public ActionResult quan_ly_dau_tu_la_gi()
        {
            ViewBag.Title = "Quản lý đầu tư là gì?";

            return View();
        }
	}
}