using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhimHang.Models;
using PagedList;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PhimHang.Controllers
{
    public class BadWordController : Controller
    {
        //
        // GET: /BadWord/
        public ActionResult Index()
        {
            return View();
        }
	}
}