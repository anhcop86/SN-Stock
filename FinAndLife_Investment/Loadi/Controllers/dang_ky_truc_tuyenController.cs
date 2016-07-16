using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loadi.Controllers
{
    public class dang_ky_truc_tuyenController : Controller
    {
        //
        // GET: /dang_ky_truc_tuyen/
        public ActionResult Index()
        {
            ViewBag.Title = "Đăng ký trực tuyến";
            return View();
        }
	}
}