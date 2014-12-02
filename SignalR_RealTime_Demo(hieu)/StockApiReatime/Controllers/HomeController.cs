using NHibernate;
using StockApiReatime.Models;
using StockApiReatime.Nhibernate;
using StockApiReatime.Nhibernate.Models;
using StockApiReatime.Nhibernate.Repositoies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StockApiReatime.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IRepository<IPProxy> jfdkjf= new IPProxyRepository();

            var test = jfdkjf.GetAll();
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}