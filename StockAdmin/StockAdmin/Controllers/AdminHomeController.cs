using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockAdmin.Controllers
{
    public class AdminHomeController : Controller
    {
        //
        // GET: /AdminHome/

        public ActionResult AdminHomeView()
        {
            return View();
        }

    }
}
