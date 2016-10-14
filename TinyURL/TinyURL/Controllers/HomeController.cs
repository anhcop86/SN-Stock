using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TinyURL.Models;
namespace TinyURL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(long url)
        {
            var findURL = IndexModel.GetData(url).URLName;

            if (findURL != null) {
                return Redirect(findURL.Trim());
            }
            else {
                return View();
            }
        }
    }
}