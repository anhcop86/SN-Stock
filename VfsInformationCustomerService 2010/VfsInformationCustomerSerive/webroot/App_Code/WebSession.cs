using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;

namespace Vfs.WebCrawler.Utility
{
    public class WebSession
    {
        public static string Referrer
        {
            get
            {

                if (HttpContext.Current.Session["Referrer"] != null)
                {
                    return (string)HttpContext.Current.Session["Referrer"];
                }
                return string.Empty;

            }
            set { HttpContext.Current.Session["Referrer"] = value; }
        }
        public static Profile Profile
        {
            get
            {
                Profile result = (Profile)HttpContext.Current.Session["UserProfile"];
                if (result == null)
                {
                    result = new Profile.Anonymous();
                    HttpContext.Current.Session["UserProfile"] = result;
                }

                return result;
            }
            set { HttpContext.Current.Session["UserProfile"] = value; }
        }
        public static SessionCreateAccount SessionCreateAccount
        {
            get
            {
                return (SessionCreateAccount)HttpContext.Current.Session["SessionCreateAccount"];
            }
            set
            {
                HttpContext.Current.Session["SessionCreateAccount"] = value;
            }
        }

        public static void RemoveAllSession()
        {

            WebSession.SessionCreateAccount = null;
        }

        public static void Reset()
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (session == null)
            {
                return;
            }

            session.RemoveAll();
            Profile = new Profile.Anonymous();
        }
        public static void RemovePurchaseOrder()
        {
            HttpSessionState session = HttpContext.Current.Session;
            session.Remove("PurchaseOrder");
        }
        public static void RemoveSalesOrder()
        {
            HttpContext.Current.Session.Remove("SalesOrder");
        }
        public static void RemoveEditRecipient()
        {
            HttpContext.Current.Session.Remove("EditRecipient");
        }
    }
}