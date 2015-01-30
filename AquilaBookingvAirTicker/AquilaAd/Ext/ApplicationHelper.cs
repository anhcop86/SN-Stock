using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using ShareHolderCore.Domain.Model;
using System.Net;
using System.IO;
using System.Configuration;
using System.Globalization;

namespace AquilaAd.Ext
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
        public static string formatS(object fs)
        {
            return String.Format("{0:#,0}", fs);
        }
        public static string ConvertDateToString(object dateObject)
        {
            DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
            datefomatProvider.DateSeparator = "/";
            datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";

            return Convert.ToDateTime(dateObject, datefomatProvider).ToString("dd/MM/yyyy");
        }
        public static string FormatPrice(Int64 price)
        {
            NumberFormatInfo numberfomatProvider = new NumberFormatInfo();
            numberfomatProvider.NumberGroupSeparator = ",";
            numberfomatProvider.NumberDecimalSeparator = ".";

            return price.ToString("#,##0", numberfomatProvider);

        }

        public static DateTime ConvertStringToDate(string dateString)
        {
            DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
            datefomatProvider.DateSeparator = "/";
            datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
            datefomatProvider.LongDatePattern = "dd/MM/yyyy";
            return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        }
        public static string ParsetoString(string dateString)
        {

            return Convert.ToDateTime(dateString).ToString("dd/MM/yyyy");
        }
        public static int ParseDatetoInt(string dateString)
        {

            return int.Parse(dateString.Substring(6, 4) + dateString.Substring(3, 2) + dateString.Substring(0, 2));
        }

        public static string PropertyApprovedEmailTemplatePath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["PropertyApprovedEmailTemplatePath"].ToString();
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

        public static string ChangePasswordEmailTemplatePath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["ChangePasswordEmailTemplatePath"];
            }
        }

        public static string ResetPasswordEmailTemplatePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ResetPasswordEmailTemplatePath"].ToString();
            }
        }



        public static string ListTransaction
        {
            get
            {
                return ConfigurationManager.AppSettings["ListTransaction"].ToString();
            }
        }
        public static string TransactionDetailOfSHList
        {
            get
            {
                return ConfigurationManager.AppSettings["TransactionDetailOfShareHolder"].ToString();
            }
        }
        public static string TransactionDetailOfSHList_Old
        {
            get
            {
                return ConfigurationManager.AppSettings["TransactionDetailOfShareHolder_Old"].ToString();
            }
        }
        public static string TransactionDetailOfSHListForVFS
        {
            get
            {
                return ConfigurationManager.AppSettings["TransactionDetailOfShareHolderForVFS"].ToString();
            }
        }

        public static string TransactionDetailOfSHListForHVC
        {
            get
            {
                return ConfigurationManager.AppSettings["TransactionDetailOfShareHolderForHVC"].ToString();
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

        public static string ApplicationEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationEmailAddress"].ToString();
            }
        }

        public static string ApplicationEmailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationEmailPassword"].ToString();
            }
        }

        public static string ImageDirectoryHotel
        {
            get
            {
                
                return HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ImageDirectoryHotel"].ToString());
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

        public static Int32 NewIssue
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["NewIssue"]);
            }
        }
        public static Int32 SHGroupId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["SHGroupId"]);
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
        public static string GetShareHolerInfoForVFS
        {
            get
            {
                return ConfigurationManager.AppSettings["Shareholderinfo_VFS"].ToString();
            }
        }
        public static string GetShareHolerInfoForHVC
        {
            get
            {
                return ConfigurationManager.AppSettings["ShareHolderInfo_HVC"].ToString();
            }
        }
        public static string LetterForVFS
        {
            get
            {
                return ConfigurationManager.AppSettings["LetterForVFS"].ToString();
            }
        }
    



       
       
        public static string GetIPAddressFromMachineName(string machineName)
        {
            string ipAdress = string.Empty;
            try
            {
                IPAddress[] ipAddresses = Dns.GetHostAddresses(machineName);

                IPAddress ip = ipAddresses[1];

                ipAdress = ip.ToString();
            }
            catch (Exception ex)
            {
                // Machine not found...   
            }
            return ipAdress;
        }

        public static string GetIPAddressFromMachineName()
        {

            string url = "http://checkip.dyndns.org";
            WebRequest req = System.Net.WebRequest.Create(url);
            try
            {
                WebResponse resp = req.GetResponse();

                StreamReader sr = new StreamReader(resp.GetResponseStream());
                string response = sr.ReadToEnd().Trim();
                string[] a = response.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                string a4 = a3[0];
                return a4;
            }
            catch (WebException)
            {
                return "";
                throw;
            }

        }
        public static IList<Availability> SumNumberQuatityRoom(IList<Availability> listShowAvailability)
        {
            int[] listcheck = new int[2];
            listcheck[0] = 1;
            listcheck[1] = 2;

            ArrayList list = new ArrayList();

            list.Add(listcheck);

            listcheck[0] = 2;
            listcheck[1] = 3;

            list.Add(listcheck);


            foreach (var item in listShowAvailability)
            {

            }

            return null;
        }
        public static int pageSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["pageSize"].ToString());
            }
        }
        public static int PageNumber(int countItem)
        {
            if (countItem % pageSize == 0)
            {
                return countItem / pageSize;
            }
            else
            {
                return countItem / pageSize + 1;
            }
        }

    }
}