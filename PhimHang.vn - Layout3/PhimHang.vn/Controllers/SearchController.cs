using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhimHang.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return RedirectToAction("Helper");
            }
            else
            {
                return RedirectToAction("", "Symbol/" + q.ToUpper());
            }
        }
        public ActionResult Helper()
        {
            return View();
        }
	}
}