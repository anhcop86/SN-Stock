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
          //  routes.MapRoute(
          //    name: "symbol",
          //    url: "Symbol/{symbolName}",
          //    defaults: new { controller = "Symbol", action = "Index", symbolName = UrlParameter.Optional }
          //);
            // map router ticker (new)
            routes.MapRoute(
              name: "ticker",
              url: "Ticker/{symbolName}",
              defaults: new { controller = "Ticker", action = "Index", symbolName = UrlParameter.Optional }
          );

          //  routes.MapRoute(
          //    name: "user",
          //    url: "User/{username}/tab/{tabid}",
          //    defaults: new { controller = "User", action = "Index", username = UrlParameter.Optional, tabid = UrlParameter.Optional }
            //);/tickers


            routes.MapRoute(
              "user",
             "{username}",
             new { controller = "User", action = "Index" }
             , new { username = new UserNameConstraint() }
          );
            routes.MapRoute(
             "userTicker",
            "{username}/portfolio",
            new { controller = "User", action = "Tickers" }
            , new { username = new UserNameConstraint() }
         );
            routes.MapRoute(
             "userFollower",
            "{username}/followers",
            new { controller = "User", action = "Followers" }
            , new { username = new UserNameConstraint() }
         );
            routes.MapRoute(
             "userFollowing",
            "{username}/following",
            new { controller = "User", action = "Following" }
            , new { username = new UserNameConstraint() }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

          
        }
    }
    public class UserNameConstraint: IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            List<string> userName = new List<string> { "myprofile", "postdetail", "search", "thitruong", "post" };        
            return !userName.Contains(values[parameterName].ToString().ToLower());
        }
    }
}
