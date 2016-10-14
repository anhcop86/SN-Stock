using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiStockPriceFromURL.Controllers
{
    public class HomeController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net  

        public ActionResult Index()
        {
            string ip = Request.UserHostAddress; // get IP address from client
            logger.Warn(ip.ToString());  

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
