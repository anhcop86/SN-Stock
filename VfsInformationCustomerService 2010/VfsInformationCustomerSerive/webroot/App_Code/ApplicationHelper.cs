using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;

/// <summary>
/// Summary description for ApplicationHelper
/// </summary>
/// 
using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;

namespace Vfs.WebCrawler.Utility
{
    public class ApplicationHelper
    {
        public ApplicationHelper()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public static string BuildUrl()
        {
            string returnUrl = string.Empty;

            for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
            {
                if (System.Web.HttpContext.Current.Request.QueryString.GetKey(i) != "Page")
                {
                    if (i == 0)
                    {
                        returnUrl += "?" + System.Web.HttpContext.Current.Request.QueryString.Keys[i] + "=" + System.Web.HttpContext.Current.Request.QueryString[i].ToString();
                    }
                    else
                    {
                        returnUrl += "&" + System.Web.HttpContext.Current.Request.QueryString.Keys[i] + "=" + System.Web.HttpContext.Current.Request.QueryString[i].ToString();
                    }
                }
            }
            return returnUrl;
        }

        public static string BuildParameters()
        {
            string returnValue = string.Empty;

            for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
            {
                if (i == 0)
                {
                    returnValue += "?" + System.Web.HttpContext.Current.Request.QueryString.Keys[i] + "=" + System.Web.HttpContext.Current.Request.QueryString[i].ToString();
                }
                else
                {
                    returnValue += "&" + System.Web.HttpContext.Current.Request.QueryString.Keys[i] + "=" + System.Web.HttpContext.Current.Request.QueryString[i].ToString();
                }
            }
            return returnValue;
        }

        public static string ConvertDateToString(object dateObject)
        {
            DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
            datefomatProvider.DateSeparator = "/";
            datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";

            return Convert.ToDateTime(dateObject, datefomatProvider).ToString("dd/MM/yyyy");
        }

        public static DateTime ConvertStringToDate(string dateString)
        {
            DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
            datefomatProvider.DateSeparator = "/";
            datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
            datefomatProvider.LongDatePattern = "dd/MM/yyyy";
            return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)));

        }

        public static string ConvertStringToFormatDate(object dateObject)
        {
            string stringFormatSameDate = Convert.ToString(dateObject);
            return stringFormatSameDate = stringFormatSameDate.Substring(6, 2) + "/" + stringFormatSameDate.Substring(4, 2) + "/" + stringFormatSameDate.Substring(0, 4);
        }

        public static string ParsetoString(string dateString)
        {
            return Convert.ToDateTime(dateString).ToString("dd/MM/yyyy");
        }

        public static string PropertyApprovedEmailTemplatePath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["PropertyApprovedEmailTemplatePath"].ToString();
            }
        }

        public static string UploadFolderPath1
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + "\\" + ConfigurationManager.AppSettings["UploadFolder"].ToString();
            }
        }

        public static string UploadFolderPath
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadFolder"].ToString();
            }
        }

        public static string SnapShotSampleFolderPath
        {
            get
            {
                return ConfigurationManager.AppSettings["SnapShotSampleFolder"].ToString();
            }
        }

        public static string SnapShotHoseOutPutFolderPath
        {
            get
            {
                return ConfigurationManager.AppSettings["SnapShotHoseOutPutFolder"].ToString();
            }
        }

        public static string SnapShotHnxOutPutFolderPath
        {
            get
            {
                return ConfigurationManager.AppSettings["SnapShotHnxOutPutFolder"].ToString();
            }
        }
        public static string ContentemplateId
        {
            get
            {
                return ConfigurationManager.AppSettings["ContentTemplateId"].ToString();
            }
        }
        public static string ContentemplateIdForPorfolioSMS
        {
            get
            {
                return ConfigurationManager.AppSettings["ContentemplateIdForPorfolioSMS"].ToString();
            }
        }
        public static string DailyContentTemplateId
        {
            get
            {
                return ConfigurationManager.AppSettings["DailyContentTemplateId"].ToString();
            }
        }
        
        public static string RelatedContentTemplateId
        {
            get
            {
                return ConfigurationManager.AppSettings["RelatedContentTemplateId"].ToString();
            }
        }

        public static string SmsUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["SmsUserName"].ToString();
            }
        }

        public static string SmsPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["SmsPassword"].ToString();
            }
        }

        public static string AttachementUploadFolderPath
        {
            get
            {
                return ContentParameterService.GetContentParameter(3).ContentParameterValue;
            }
        }

        public static string GetFullPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            string result = string.Empty;

            // TODO I think it would be better solution
            if (HttpContext.Current != null)
            {
                result = HttpContext.Current.Server.MapPath(path);
            }
            else
            {
                result = Path.GetFullPath(path);
            }

            return result;
        }

        public static string PropertyApprovedEmailSubject
        {
            get
            {
                return ConfigurationManager.AppSettings["PropertyApprovedEmailSubject"];
            }
        }

        public static string ApplicationEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationEmailAddress"].ToString();
            }
        }

        public static string FormatPrice(Int64 price)
        {
            NumberFormatInfo numberfomatProvider = new NumberFormatInfo();
            numberfomatProvider.NumberGroupSeparator = ",";
            numberfomatProvider.NumberDecimalSeparator = ".";

            return price.ToString("#,##0", numberfomatProvider);

        }

        public static string ListTransaction
        {
            get
            {
                return ConfigurationManager.AppSettings["ListTransaction"].ToString();
            }
        }
        public static string ExportSymbolPermLong
        {
            get
            {
                return ConfigurationManager.AppSettings["ExportSymbolPermLong"].ToString();
            }
        }
        public static string ExportTemplateOfStox
        {
            get
            {
                return ConfigurationManager.AppSettings["ExportTemplateOfStox"].ToString();
            }
        }
        public static string ExportStatisticTransaction
        {
            get
            {
                return ConfigurationManager.AppSettings["ExportStatistisTransaction"].ToString();
            }
        }
        public static string ExportSessionCompany
        {
            get
            {
                return ConfigurationManager.AppSettings["ExportSessionCompany"].ToString();
            }
        }
        public static string TransactionDetailOfSHList
        {
            get
            {
                return ConfigurationManager.AppSettings["TransactionDetailOfShareHolder"].ToString();
            }
        }
        public static string TransactionDetailOfSHList2
        {
            get
            {
                return ConfigurationManager.AppSettings["TransactionDetailOfShareHolder2"].ToString();
            }
        }

        public static string SmtpServer
        {
            get
            {
                return ConfigurationManager.AppSettings["SmtpServer"].ToString();
            }
        }

        public static Int16 SmtpPort
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["SmtpPort"].ToString());
            }
        }

        public static string PublicSiteUrl
        {
            get
            {
                string rootPath = ConfigurationManager.AppSettings["PublicSiteUrl"].ToString();
                return rootPath;
            }
        }

        public static string AutoNumber
        {
            get
            {
                string prefix = "VN";
                string autoNumber = prefix + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Millisecond.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(1);
                return autoNumber;
            }
        }
        public static string ListTransactionBallance
        {
            get
            {
                return ConfigurationManager.AppSettings["ListBallance"].ToString();
            }
        }

        public static Int16 PageSize
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
            }
        }
        public static string GetShareHolerInfo
        {
            get
            {
                return ConfigurationManager.AppSettings["Shareholderinfo"].ToString();
            }
        }
        public static string returnDate(object dafe)
        {
            return String.Format("{0:MM/dd/yyyy}", dafe);
        }
    }
}
