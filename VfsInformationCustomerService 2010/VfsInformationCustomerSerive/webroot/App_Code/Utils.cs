using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for Utils
/// </summary>
public static class Utils
{
	
    public static bool StringIsNullOrWhitespace(string value)
    {
        return ((value == null) || (value.Trim().Length == 0));
    }

    //public static string VirtualPath
    //{
    //     get
    //        {
    //            return WebConfigurationManager.AppSettings["Customer.VirtualPath"] ?? "~/";
    //        }
    //}
    //public static string ApplicationRelativeWebRoot
    //{
    //    get
    //    {
    //        //return applicationRelativeWebRoot ??
    //        //       (applicationRelativeWebRoot =
    //        //        VirtualPathUtility.ToAbsolute(VirtualPath));
    //    }
    //}

    //public static string RelativeWebRoot
    //{
    //    get
    //    {
    //        return Blog.CurrentInstance.RelativeWebRoot;
    //    }
    //}
}