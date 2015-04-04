using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TinyURL.Models;
namespace TinyURL.Controllers
{
    public class HomeController : Controller
    {
        TinyURLEntitiesDB db = new TinyURLEntitiesDB();
        public async Task<ActionResult> Index(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return View();    
            }
            else
            {
                var findURL = await (from urllink in db.URLTinies
                                     where urllink.Id.ToString() == url
                                     select urllink.URLName).FirstOrDefaultAsync();
                if (findURL != null)
                {
                    return Redirect(findURL); 
                }
                else
                {
                    return View();    
                }
                
            }
        }   
        public async Task<ActionResult> TEST()
        {
            AppHelper.CheckDayOfWeekend();
            return View();
        }
    }
}