using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockAdmin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult didLogin(string txtUserName, string txtPassword)
        {
            return RedirectToAction("AdminHomeView", "AdminHome");
        }
    }
}
