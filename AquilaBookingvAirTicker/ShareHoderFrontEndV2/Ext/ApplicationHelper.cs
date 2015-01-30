using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Configuration;
using System.IO;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ShareHoderFrontEndV2.Models;
using System.Net;
using System.Collections;
using System.Web.Providers.Entities;

namespace ShareHoderFrontEndV2.Ext
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

        public static string BookingEmailTemplatePath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["BookingEmailTemplatePath"];
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


        public static Int16 SmtpPort
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["SmtpPort"].ToString());
            }
        }
        public static bool EnableSSL
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"].ToString());
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
        public static IList<Hotel> getlistComparehotel()
        {
            var listcompare = (List<int>)HttpContext.Current.Session["comparelistold"];
            if (listcompare == null)
            {
                listcompare = new List<int>();
            }

            IRepository<Hotel> rpHotel = new HotelRepository();
            IList<Hotel> listComparehotel = new List<Hotel>();
            foreach (var item in listcompare)
            {
                Hotel hotel = rpHotel.GetById(item);
                listComparehotel.Add(hotel);

            }
            return listComparehotel;
 
        }
        public static IList<MonthOption> getListMonthOption()
        {
            IList<MonthOption> ListMonthOption = new List<MonthOption>();
            TimeSpan amonth = new TimeSpan(30, 0, 0, 0);
            DateTime now = DateTime.Now;

            //DateTime nextMoth = now + amonth;
            for (int i = 0; i <= 11; i++)
            {

                MonthOption MonthOption = new MonthOption();
                MonthOption.ValueMonthYear = (now.AddMonths(i).Month.ToString().Length == 2 ? now.AddMonths(i).Month.ToString() : "0" + now.AddMonths(i).Month) + "/" + now.AddMonths(i).Year;
                MonthOption.TextMonthYear = "Tháng " + "" + (now.AddMonths(i).Month.ToString().Length == 2 ? now.AddMonths(i).Month.ToString() : "0" + now.AddMonths(i).Month) + "/" + now.AddMonths(i).Year;

                ListMonthOption.Add(MonthOption);
            }

            return ListMonthOption;

        }
        public static string ConvertToNonUnicode(string str)
        {
            /*for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str; */
            return ConvertToNonUnicodeV2(str);
        }
        private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỢỞỠ",
            "úùụủũứừựửữ",
            "ÚÙỤỦŨỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string ConvertToNonUnicodeV2(string text)
        {
            string[] pattern = new string[7];

            pattern[0] = "a|(á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ)";

            pattern[1] = "o|(ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ)";

            pattern[2] = "e|(é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ)";

            pattern[3] = "u|(ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ)";

            pattern[4] = "i|(í|ì|ỉ|ị|ĩ)";

            pattern[5] = "y|(ý|ỳ|ỷ|ỵ|ỹ)";

            pattern[6] = "d|đ";

            for (int i = 0; i < pattern.Length; i++)
            {

                // kí tự sẽ thay thế

                char replaceChar = pattern[i][0];

                MatchCollection matchs = Regex.Matches(text, pattern[i]);

                foreach (Match m in matchs)
                {
                    text = text.Replace(m.Value[0], replaceChar);
                }

            }

            return text;
        }

        public static void WirteJson(IList<LocationModel> listL)
        {
            using (StreamWriter file = File.CreateText(System.Web.HttpContext.Current.Server.MapPath(@"Upload\location.json")))
            {

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listL);
            }
        }
        public static IList<String> LocationTag()
        {
            #region Location Tag
            IList<String> stringLocationTag = new List<String>();
            IHotelRepository<Hotel> rp = new HotelRepository();
            if (HttpContext.Current.Session["textboxSeachValue"] != null)
            {
                string textboxSeachValue = HttpContext.Current.Session["textboxSeachValue"].ToString();
                LocationTag locationtag = rp.getHotelLocationTag(ApplicationHelper.ConvertToNonUnicode(textboxSeachValue.Trim()));
                string SearchStringConvertUnicode = string.Empty;
                string SearchStringConvertUnicodeFromDb = string.Empty;
                SearchStringConvertUnicode = ApplicationHelper.ConvertToNonUnicode(textboxSeachValue.Trim());
                //IList<LocationModel> listL = ApplicationHelper.ReadJson();
                //LocationModel locationModel = null;
                if (locationtag == null)
                {
                    SearchStringConvertUnicode = ApplicationHelper.ConvertToNonUnicode(textboxSeachValue);
                    string[] a = SearchStringConvertUnicode.Split(' ');
                    SearchStringConvertUnicode = a[0];
                    locationtag = rp.getHotelLocationTag(ApplicationHelper.ConvertToNonUnicode(SearchStringConvertUnicode));
                }
                if (locationtag==null)
                {
                    stringLocationTag.Add(Resources.Resource.noResultFound + " '" + textboxSeachValue + "...'");
                    return stringLocationTag;
                }
                foreach (var item in locationtag.LocationTagName.Split(';'))
                {

                    SearchStringConvertUnicodeFromDb = ApplicationHelper.ConvertToNonUnicode(item.Trim());
                    if (SearchStringConvertUnicodeFromDb.ToUpper().IndexOf(SearchStringConvertUnicode.ToUpper()) == -1)
                    {
                        stringLocationTag.Add(item);
                    }
                    else
                    {
                        if (SearchStringConvertUnicode.Length == SearchStringConvertUnicodeFromDb.Length)
                        {
                            stringLocationTag.Add(item);

                        }
                        else
                        {
                            stringLocationTag.Add(Resources.Resource.searchFor + " " + "'" + textboxSeachValue + "...'");
                        }
                        return stringLocationTag;
                    }

                }
            }
            return stringLocationTag;

            #endregion



        }
        public static IList<LocationModel> ReadJson()
        {
            IList<LocationModel>  listL = new List<LocationModel>();
            using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(@"..\..\Upload\location.json")))
            {
                JsonSerializer serializer = new JsonSerializer();
                listL = (IList<LocationModel>)serializer.Deserialize(file, typeof(IList<LocationModel>));
            }

            return listL;
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

            /*string url = "http://checkip.dyndns.org";
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
                return "Your IP hide";
                throw;
            }*/

            return System.Web.HttpContext.Current.Request.UserHostAddress;
          
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
        public static string ImageDirectoryHotel
        {
            get
            {

                return ConfigurationManager.AppSettings["ImageDirectoryHotel"].ToString();
            }
        }

        public static int pageSize
        {
            get
            {
                return int.Parse( ConfigurationManager.AppSettings["pageSize"].ToString());
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
                return  countItem / pageSize + 1;
            }
        }


        public static decimal GetMinPriceOfHotelHotDeal(int hotelId)
        {
            IAvailabilityRepository<Availability> IrA = new AvailabilityRepository();
            return IrA.GetMinPriceOfHotelHotDeal(hotelId);
        }

        public static decimal GetSaleOffRateHotDeal(int hotelId)
        {
            IAvailabilityRepository<Availability> IrA = new AvailabilityRepository();
            decimal max = IrA.GetMaxPriceOfHotelHotDeal(hotelId);
            decimal min = GetMinPriceOfHotelHotDeal(hotelId);
            return 0;
            //return decimal.Round((min / max) * 100, 1);
        }

        public static string GetNameOfMembershipForComment(int memberid)
        {
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> IrA = new MembershipRepository();

            return IrA.GetByMemberId(memberid).MemberName;
            
        }

        public static IList<BookingDetail> getBookingDetailFromBookingid(long bookingid)
        {
            IBookingDetailRepository<BookingDetail> irBd = new BookingDetailRepository();
            IList<BookingDetail> listBd = new List<BookingDetail>();
            
            return irBd.getListByBookingid(bookingid);
        }
    }
}