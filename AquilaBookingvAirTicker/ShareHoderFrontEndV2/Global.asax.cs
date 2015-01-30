using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc.Resources;
using System.Web.Mvc;
using System.Web.Razor.Resources;
using System.Web.Mvc.Razor;
using System.Web.Routing;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Event;
using System.Web.WebPages;
using System.Globalization;
using System.Threading;


namespace ShareHoderFrontEndV2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
      
            
        }

        protected void Application_Start()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
          //  DisplayModes.Modes.Insert(0, new DefaultDisplayMode("Mobile")
           /*DisplayModeProvider.Instance.Modes.Insert(0,  new DefaultDisplayMode("Mobile")          
            {
                ContextCondition = (ctx => ctx.Request.UserAgent != null
                                            && (ctx.Request.UserAgent.IndexOf("Android", StringComparison.OrdinalIgnoreCase) >= 0
                                                || ctx.Request.UserAgent.IndexOf("mobile", StringComparison.OrdinalIgnoreCase) >= 0
                                                || ctx.Request.UserAgent.IndexOf("Opera Mobi", StringComparison.OrdinalIgnoreCase) >= 0
                                                || ctx.Request.UserAgent.IndexOf("Opera Mini", StringComparison.OrdinalIgnoreCase) >= 0
                                                || ctx.Request.UserAgent.IndexOf("iPad", StringComparison.OrdinalIgnoreCase) >= 0))
            }); */         
            this.InitializeValidator();
        }

        private void InitializeValidator()
        {
            var provider = new NHibernateSharedEngineProvider();
            NHibernate.Validator.Cfg.Environment.SharedEngineProvider = provider;
        }     
    }
}