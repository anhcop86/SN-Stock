using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loadi.Controllers
{
    public class FUJI_RECOREDController : Controller
    {
        //
        // GET: /FUJI_RECORED/

        public RedirectToRouteResult Index()
        {
            return RedirectToAction("chia_se_20_phan_tram_loi_nhuan", "FUJI_RECORED");
        }
        public ActionResult chia_se_20_phan_tram_loi_nhuan()
        {
            //ViewBag.ControllerName = Request.RequestContext.RouteData.Values["controller"];
            ViewBag.Title = string.Format("{0}{1}", "Chia sẻ 20% lợi nhuận", "");
            return View();
        }

        //
        // GET: /FUJI_RECORED/
        public ActionResult ty_suat_loi_nhuan_hon_10()
        {
            ViewBag.Title = string.Format("{0}{1}", "Tỷ suất lợi nhuận hơn 10%", "");
            return View();
        }

        // GET: /FUJI_RECORED/
        public ActionResult chia_se_rui_ro_voi_khach()
        {
            ViewBag.Title = string.Format("{0}{1}", "Chia sẻ rủi ro với khách", "");
            return View();
        }
	}
}