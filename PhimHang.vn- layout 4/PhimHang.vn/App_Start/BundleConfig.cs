﻿using System.Web;
using System.Web.Optimization;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;
namespace PhimHang
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            //This setting is used when if you have specfied the path Using System.web.Optimization.bundle.Cdnpath then it will try to fetch data from there first
            bundles.UseCdn = true;
            //NullBuilder class is responsible for prevention of early applying of the item transformations and combining of code.
            var nullBuilder = new NullBuilder();
            //StyleTransformer and ScriptTransformer classes produce processing of stylesheets and scripts.
            var styleTransformer = new StyleTransformer();

            var scriptTransformer = new ScriptTransformer();
            //NullOrderer class disables the built-in sorting mechanism and save assets sorted in the order they are declared.
            var nullOrderer = new NullOrderer();


            bundles.Add(new ScriptBundle("~/bundles/standardCoreRT").Include(
                                        //<!--Jquery standard-->
                                        "~/Scripts/jquery-2.1.1.min.js",
                                        "~/ThemeBlue/js/jquery/jquery.js",
                                         
                                        "~/ThemeBlue/js/jquery/jquery-ui.js",
                                        //<!--For realtime UI-->
                                        "~/Scripts/knockout-3.2.0.js",
                                        "~/Scripts/knockout.mapping-latest.js",                                                    
                                        //<!--For realtime core-->
                                        "~/Scripts/jquery.signalR-2.1.2.min.js",
                                        "~/ThemeBlue/js/Extention.js", // notification of top bar
                                        "~/ThemeBlue/js/SignalHubProxy.js"
                                        ));

            bundles.Add(new StyleBundle("~/bundles/StyleStandard").Include(
                    "~/ThemeBlue/css/jquery-ui/jquery-ui.css",
                      "~/ThemeBlue/css/style.css"
                      ));
            #region ticker bundless and encode
            bundles.Add(new ScriptBundle("~/bundles/Ticker").Include(                
                             "~/ThemeBlue/js/Ticker.js"));

            bundles.Add(new ScriptBundle("~/bundles/ChartRender").Include(
                             "~/ThemeBlue/js/highstock.js",
                             "~/ThemeBlue/js/exporting.js",
                             "~/ThemeBlue/js/Chartcp.js"
                             ));
            bundles.Add(new ScriptBundle("~/bundles/FollowStock").Include(
                             "~/ThemeBlue/js/FollowStock.js"));
            #endregion


            BundleTable.EnableOptimizations = true;
        }
    }
}
