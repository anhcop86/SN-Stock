using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for AppConstants
/// </summary>
/// 
namespace Vfs.WebCrawler.Utility
{
    public class AppConstants
    {
        public static string QS_USER_ID = "userId";
        public static string QS_PROPERTY_ID = "propertyId";
        public static string QS_PURPOSE = "purpose";
        public static string QS_TYPE_ID = "typeId";
        public static string QS_CITY_ID = "cityId";
        public static string QS_LOCATION_ID = "locationId";
        public static string QS_CURRENCY_ID = "currencyId";
        public static string QS_LANGUAGE_ID = "languageId";
        public static string QS_PRICE_FROM = "priceFrom";
        public static string QS_PRICE_TO = "priceTo";
        public static string QS_SHOW = "show";
        public static string QS_ADVANCED_SEARCH = "advancedSearch";
        public static string QS_SEARCH_WHAT = "searchWhat";
        public static string QS_ARTICLE_ID = "articleId";
        public static string QS_ACTION = "action";
        public static string QS_COMPANY_ID = "companyId";
        public static string QS_SHARE_TYPE_ID = "shareTypeId";
        public static string QS_SHARE_HODER_GROUP_ID = "ShareHolderGroupId";
        public static string QS_UNIT_ID = "unitId";
        public static string QS_PROPERTY_MANANER_ID = "propertyManagerId";
        public static string QS_PROPERTY_DIRECTION_ID = "directionId";
        public static string QS_FACILITY_ID = "facilityId";
        public static string QS_PROJECT_ID = "projectId";
        public static string QS_CONTENT_CATEGORY_ID = "contentCategoryId";
        public static string QS_ERROR_CODE = "errorCode";
        public static string QS_TRANSACTION_CATEGORY_ID = "transactionCategoryId";
        public static string QS_SHARE_HOLDER_ID = "shareHolderId";
        public static string QS_TRANSACTION_ID = "transactionId";
        public static string QS_TRANSACTION_DETAIL_ID = "transactionDetailId";
        public static string QS_SITE = "siteId";
        public static string QS_LINK = "linkId";
        public static string QS_NEW_ID = "newId";
        public static string QS_SERVICE_TYPE_ID = "serviceTypeId";
        public static string QS_CONTENT_TEMPLATE_ID = "contentTemplateID";
        public static string QS_CLICK_ED = "clicked";
        public static string QS_MESSAGE_CONTENET_ID = "messagecontentid";
        public static string QS_CALL_BACK = "callback";
        public static string QS_MESSAGE_CONTENT_SENT_ID = "messagecontentsentid";
        public static string QS_CUSTOMER_ID = "customerid";
        public static string QS_EMAIL = "email";
        public static string QS_TEMPLATE_ID = "templateID";
        public static string QS_SESSION = "session";

        public AppConstants()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public static Int32 GetInt32(string stringName)
        {
            Int32 stringValue;
            if (System.Web.HttpContext.Current.Request.QueryString[stringName] == "" || System.Web.HttpContext.Current.Request.QueryString[stringName] == null)
            {
                stringValue = 0;
            }
            else
            {
                stringValue = Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString[stringName]);
            }
            return stringValue;
        }

        public static string GetString(string stringName)
        {
            string stringValue;
            if (Convert.ToString(System.Web.HttpContext.Current.Request.QueryString[stringName]) == "" || System.Web.HttpContext.Current.Request.QueryString[stringName] == null)
            {
                stringValue = "";
            }
            else
            {
                stringValue = Convert.ToString(System.Web.HttpContext.Current.Request.QueryString[stringName]);
            }
            return stringValue;
        }

        public static Int64 GetInt64(string stringName)
        {
            Int64 stringValue;
            if (System.Web.HttpContext.Current.Request.QueryString[stringName] == "" || System.Web.HttpContext.Current.Request.QueryString[stringName] == null)
            {
                stringValue = 0;
            }
            else
            {
                stringValue = Convert.ToInt64(System.Web.HttpContext.Current.Request.QueryString[stringName]);
            }
            return stringValue;
        }

        public static string PropertyPhotosPath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["PropertyPhotosPath"].ToString();
            }
        }

        //public static string PropertyPhotosPathFromDatabase
        //{
        //    get
        //    {
        //        string rootPath = HttpContext.Current.Server.MapPath("~");
        //        return  "\\" + rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ParameterService.GetParameter(8).ParameterValue;

        //    }
        //}

        //public static string AdminSiteUrlFromDatabase
        //{
        //    get
        //    {
        //        return ParameterService.GetParameter(7).ParameterValue;

        //    }
        //}

        public static string PropertyMediaPath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["PropertyMediaPath"].ToString();
            }
        }

        public static string RelativePropertyPhotosPath
        {
            get
            {
                return "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["PropertyPhotosPath"].ToString().Replace(@"\", @"/");
            }
        }

        public static string NewsPhotosPath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["NewsPhotosPath"].ToString();
            }
        }

        public static string RelativeNewsPhotosPath
        {
            get
            {
                return "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["NewsPhotosPath"].ToString().Replace(@"\", @"/");
            }
        }
    }

    public class FriendlyUrlConstants
    {
        public static string ARTICLE_LIST_PREFIX = "ArticleList";
        public static string ARTICLE_PREFIX = "Article";
        public static string PROPERTY_LIST_PREFIX = "PropertyList";
        public static string PROPERTY_DETAILS_PREFIX = "PropertyDetails";
    }
}