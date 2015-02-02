using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhimHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
              name: "symbol",
              url: "Symbol/{symbolName}",
              defaults: new { controller = "Symbol", action = "Index", symbolName = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "user",
              url: "User/{username}/tab/{tabid}",
              defaults: new { controller = "User", action = "Index", username = UrlParameter.Optional, tabid = UrlParameter.Optional }
          );
    
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
