using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;

using CoreSecurityService.Entities;
using CoreSecurityService.Data;
using CoreSecurityService.Business;

namespace UnitTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            CoreSecurityService.Entities.StockPriceCollection priceList = CoreSecurityService.Business.StockPriceService.GetStockPriceList(CoreSecurityService.Entities.StockPriceColumns.StockNo, "DESC");

            string url = @"http://stox.vn/stox/EarningsCentral/?MenuID=4";
            ParseCafefDocument parseDocument = new ParseCafefDocument(url);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();
            HtmlNode newDate;
            ParseCafefDocument parseDocument1;
            StockNew stockNew = null;


            string test = ReplaceAsciiToUniCode("Th&#7913; Ba, 20/07/2010, Ng&#224;y giao d&#7883;ch ch&#237;nh th&#7913;c c&#7911;a C&#244;ng ty c&#7893; ph&#7847;n D&#432;&#7907;c L&#226;m &#272;&#7891;ng &#8211; Ladophar (MCK: LDP)");
            test = char.ConvertFromUtf32(7913);
            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    HtmlNode link = null;
                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
                    
                    if (link != null)
                    {
                        parseDocument1 = new ParseCafefDocument("http://stox.vn/" + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine("http://stox.vn/" + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine("\n");
                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = DateTime.Now;
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        stockNew.NewsDate = DateTime.Now;
                        stockNew.NewsDescription = link.InnerHtml.Normalize();
                        stockNew.NewsSource = "Stox";
                        stockNew.NewsTitle = link.InnerHtml.Normalize();
                        stockNew.OriginalUrl = "http://stox.vn/" + link.Attributes["href"].Value.ToString().Remove(0, 1);
                        stockNew.LinkId = 9;
                        FeedItem(stockNew);
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteReportToDisk(ex.Message);
                //log4net.Util.LogLog.Error(ex.Message, ex);          
            }
        }

        public static string DoMD5(string SData)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
            byte[] result1 = md5.ComputeHash(encode.GetBytes(SData));
            string sResult2 = ToHexa(result1);
            return sResult2;
        }

        public static string ToHexa(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.AppendFormat("{0:X2}", data[i]);
            return sb.ToString();
        }

        public static string ServiceCodePraser(String messageContent)
        {
            string returnvalue = "void";
            string[] messageContentArray;
            if (messageContent != String.Empty)
            {
                messageContentArray = NormalizeWhitespace(messageContent.TrimEnd().TrimStart()).Split(' ');
                //messageContentArray = messageContent.Split(' ');
                returnvalue = messageContentArray[1];
            }
            return returnvalue.ToUpper();
        }

        public static String NormalizeWhitespace(String InputStr) 
        {
             Regex NormRx = new Regex("\\s\\s+");
             return NormRx.Replace(InputStr, " ");
        }

        public static DateTime ConvertStringToDate(string dateString)
        {
            DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
            datefomatProvider.DateSeparator = "/";
            datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
            datefomatProvider.LongDatePattern = "dd/MM/yyyy";
            return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)));

            //return DateTime.Parse(dateString);
            //return Convert.ToDateTime(dateString, datefomatProvider);
        }

        public static bool FeedItem(StockNew stockNew)
        {
            bool returnVal = true;
            try
            {
                StockNewService.CreateStockNew(stockNew);
            }
            catch (Exception ex)
            {
                //Ultility.Error(ex.InnerException);
                returnVal = false;
            }
            return returnVal;
        }

        //public static void FeedVnEconomyNews()
        //{
        //    Source source = SourceService.GetSource(5);
        //    LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
        //    foreach (Link lnk in links)
        //    {
        //       // Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
        //        //Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
        //        FeedVnEconomyNews(source.URL + lnk.Link, source.URL + lnk.LinkShortDescription, lnk.LinkId);
        //        //Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
        //    }
        //}

        //public static void FeedVnEconomyNews(string listUrl, string shortUrl, int linkId)
        //{
        //    ParseVnEconomyDocument parseDocument = new ParseVnEconomyDocument(listUrl);
        //    List<HtmlNode> TableCells = parseDocument.ParseTableCell();
        //    HtmlNode spans;         
        //    HtmlNode newDate;
        //    ParseVnEconomyDocument parseDocument1;
        //    StockNew stockNew = null;

        //    try
        //    {
        //        for (int i = 0; i < TableCells.Count; i++)
        //        {                    
        //            HtmlNode link = null;
                    
        //            link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
        //            newDate = parseDocument.ParseNewsDate((HtmlNode)TableCells[i]);           
        //            if (link != null)
        //            {
        //                parseDocument1 = new ParseVnEconomyDocument(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
        //                Console.WriteLine(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
        //                Console.WriteLine("\n");
        //                HtmlNode content = parseDocument1.ParseContent();
        //                stockNew = new StockNew();
        //                stockNew.FeedDate = DateTime.Now;
        //                stockNew.LanguageID = 2;
        //                stockNew.NewsContent = content.InnerHtml;                        
        //                stockNew.NewsDate = ConvertStringToDate(DateTime.Now.ToString("dd/MM/yyyy"));
        //                stockNew.NewsDescription = ((HtmlNode)parseDocument1.ParseNewsDescription()).InnerHtml.Trim();
        //                stockNew.NewsSource = "Vneconomy";
        //                stockNew.NewsTitle = link.InnerHtml.Trim();
        //                stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString().Remove(0,1);
        //                stockNew.LinkId = linkId;
        //                FeedItem(stockNew);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Ultility.Error(ex.Message);
        //    }
        //}

        public static bool ValidateEmail(string inputEmail)
        {
            if (inputEmail == string.Empty) return false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private static string ConvertAsciiToUnicode(string theAsciiString)
        {
            // Create two different encodings.
            Encoding aAsciiEncoding = Encoding.ASCII;
            Encoding aUnicodeEncoding = Encoding.UTF7;
            // Convert the string into a byte[].
            byte[] aAsciiBytes = aAsciiEncoding.GetBytes(theAsciiString);
            // Perform the conversion from one encoding to the other.
            byte[] aUnicodeBytes = Encoding.Convert(aAsciiEncoding, aUnicodeEncoding,
            aAsciiBytes);
            // Convert the new byte[] into a char[] and then into a string.
            char[] aUnicodeChars = new
            char[aUnicodeEncoding.GetCharCount(aUnicodeBytes, 0, aUnicodeBytes.Length)];
            aUnicodeEncoding.GetChars(aUnicodeBytes, 0, aUnicodeBytes.Length,
            aUnicodeChars, 0);
            string aUnicodeString = new string(aUnicodeChars);
            return aUnicodeString;
        }

        private static string ReplaceAsciiToUniCode(string input)
        {
            string text = input;

            MatchCollection matchColl = Regex.Matches(text, @"&#[a-z]*[0-9]*;");
            string result1;
            result1 = text;
            int i = 0;

            Match match = Regex.Match(text, @"&#[a-z]*[0-9]*;");
            while (match.Success)
            {
                string replacingString = char.ConvertFromUtf32(Convert.ToInt32(match.Value.Trim().Replace("&","").Replace("#","").Replace(";","")));
                result1 = result1.Replace(match.ToString(), replacingString);
                match = match.NextMatch();
                i++;
            }
            return result1;
        }
    }
}
