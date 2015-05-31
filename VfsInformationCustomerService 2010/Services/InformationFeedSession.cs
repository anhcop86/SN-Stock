using System;
using System.Collections.Generic;
using System.Text;
using VfsInformationFeedService.Crawler;
using System.Configuration;
using System.IO;
using System.Collections;

using HtmlAgilityPack;
using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

namespace VfsInformationFeedService
{
    public class InformationFeedSession
    {
        private List<Exception> listError;
        private int numberOfitem;
        
        public IList<Exception> ListError
        {
            get { return listError; }
        }

        public int NumberOfitem
        {
            get { return numberOfitem; }
        }
        
        public InformationFeedSession(int numberOfItem)
        {
            this.numberOfitem = numberOfItem;
            this.Initialize();
        }

        public bool FeedItem(StockNew stockNew)
        {
            bool returnVal = true;
            try
            {
                StockNewService.CreateStockNew(stockNew);
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.InnerException);
                returnVal = false;
            }
            return returnVal;
        }

        public void FeedNews(int SourceId)
        {
            Source source = SourceService.GetSource(SourceId);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach(Link lnk in links)
            {
                Ultility.Info("----- Feed from:" + lnk.LinkDescription +   " -----");
                Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedHoseNews(source.URL + lnk.Link, source.URL + lnk.LinkShortDescription, lnk.LinkId);
                Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedHoseNews()
        {
            Source source = SourceService.GetSource(1);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedHoseNews(source.URL + lnk.Link, source.URL + lnk.LinkShortDescription, lnk.LinkId);
                Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedHoseNews(string listUrl, string shortUrl, int linkId)
        {
            //testing listUrl = @"http://www.hsx.vn/hsx/Modules/News/News.aspx?type=TTGD";
            ParseDocument parseDocument = new ParseDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();
            HtmlNode spans;
            HtmlNode link;
            ParseDocument parseDocument1;
            StockNew stockNew = null;

            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    spans = parseDocument.ParseSpan((HtmlNode)TableCells[i]);
                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
                    //parseDocument1 = new ParseDocument("http://www.hsx.vn/hsx/Modules/News/" + link.Attributes["href"].Value.ToString());
                    parseDocument1 = new ParseDocument(shortUrl + link.Attributes["href"].Value.ToString());

                    HtmlNode content = parseDocument1.ParseContent();
                    stockNew = new StockNew();
                    stockNew.FeedDate = DateTime.Now;
                    stockNew.LanguageID = 2;
                    stockNew.NewsContent = content.InnerHtml;
                    stockNew.NewsDate = Ultility.ConvertStringToDate(spans.InnerHtml.ToString());
                    stockNew.NewsDescription = link.InnerHtml;
                    stockNew.NewsSource = "HOSE";
                    stockNew.NewsTitle = link.InnerHtml;
                    stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString();
                    stockNew.LinkId = linkId;
                    FeedItem(stockNew);
                }
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.Message);
            }
        }

        public void FeedVsdNews()
        {
            Source source = SourceService.GetSource(3);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedVsdNews(source.URL + lnk.Link, source.URL + lnk.LinkShortDescription, lnk.LinkId);
                Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedVsdNews(string listUrl, string shortUrl, int linkId)
        {
            //testing listUrl = @"http://www.hsx.vn/hsx/Modules/News/News.aspx?type=TTGD";
            ParseVsdDocument parseDocument = new ParseVsdDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();
            HtmlNode spans;
            HtmlNode link;
            ParseVsdDocument parseDocument1;
            StockNew stockNew = null;

            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    //spans = parseDocument.ParseSpan((HtmlNode)TableCells[i]);
                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
                    //parseDocument1 = new ParseDocument("http://www.hsx.vn/hsx/Modules/News/" + link.Attributes["href"].Value.ToString());
                    if (link != null)
                    {
                        parseDocument1 = new ParseVsdDocument(shortUrl + link.Attributes["href"].Value.ToString());

                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = DateTime.Now;
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        string newDate = link.InnerHtml.Substring(link.InnerHtml.IndexOf('[') + 1, link.InnerHtml.IndexOf(']') - link.InnerHtml.IndexOf('['));
                        stockNew.NewsDate = Ultility.ConvertStringToDate(newDate);
                        stockNew.NewsDescription = link.InnerHtml.Substring(0, link.InnerHtml.IndexOf('['));
                        stockNew.NewsDescription = stockNew.NewsDescription.TrimStart().TrimEnd().Replace("&nbsp", " ");
                        stockNew.NewsSource = "TTLK";
                        /*if (link.InnerHtml.ToString().IndexOf("<img") >= 0)
                            stockNew.NewsTitle = link.InnerHtml.ToString().Remove(link.InnerHtml.ToString().IndexOf("<img"));
                        else
                            stockNew.NewsTitle = link.InnerHtml;*/
                        stockNew.NewsTitle = stockNew.NewsDescription.Replace("&nbsp", " ");
                        stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString();
                        stockNew.LinkId = linkId;
                        FeedItem(stockNew);
                    }
                }
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.Message);
            }
        }

        public void FeedCafefNews()
        {
            Source source = SourceService.GetSource(4);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedCafefNews(source.URL + lnk.Link, source.URL , lnk.LinkId);
                Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedCafefNews(string listUrl, string shortUrl, int linkId)
        {
            ParseCafefDocument parseDocument = new ParseCafefDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();            
            HtmlNode newDate;
            ParseCafefDocument parseDocument1;
            StockNew stockNew = null;
            int post = 0;
            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    HtmlNode link = null;
                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
                    newDate = parseDocument.ParseNewsDate((HtmlNode)TableCells[i]);
                    if (link != null)
                    {
                        parseDocument1 = new ParseCafefDocument(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine("\n");
                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        stockNew.NewsDate = DateTime.Now;//Ultility.ConvertStringToDate(newDate.InnerHtml.Replace("\r", ""));
                        stockNew.NewsDescription = ((HtmlNode)parseDocument1.ParseNewsDescription()).InnerHtml;
                        stockNew.NewsSource = "CafeF";
                        post = parseDocument1.ParseNewsTitle().InnerHtml.IndexOf("<input");
                        stockNew.NewsTitle = parseDocument1.ParseNewsTitle().InnerHtml.Substring(0, post).Normalize();
                        stockNew.NewsTitle = stockNew.NewsTitle.Trim();
                        stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1);
                        stockNew.LinkId = linkId;
                        FeedItem(stockNew);
                    }
                }
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.Message);
            }
        }

        public void FeedVnEconomyNews()
        {
            Source source = SourceService.GetSource(5);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                // Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                //Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedVnEconomyNews(source.URL + lnk.Link, source.URL + lnk.LinkShortDescription, lnk.LinkId);
                //Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedVnEconomyNews(string listUrl, string shortUrl, int linkId)
        {
            ParseVnEconomyDocument parseDocument = new ParseVnEconomyDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();
            HtmlNode spans;
            HtmlNode newDate;
            ParseVnEconomyDocument parseDocument1;
            StockNew stockNew = null;

            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    HtmlNode link = null;

                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
                    newDate = parseDocument.ParseNewsDate((HtmlNode)TableCells[i]);
                    if (link != null)
                    {
                        parseDocument1 = new ParseVnEconomyDocument(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine("\n");
                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = DateTime.Now;
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        stockNew.NewsDate = Ultility.ConvertStringToDate(DateTime.Now.ToString("dd/MM/yyyy"));
                        stockNew.NewsDescription = ((HtmlNode)parseDocument1.ParseNewsDescription()).InnerHtml.Trim();
                        stockNew.NewsSource = "VnEconomy";
                        stockNew.NewsTitle = link.InnerHtml.Trim();
                        stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1);
                        stockNew.LinkId = linkId;
                        FeedItem(stockNew);
                    }
                }
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.Message);
            }
        }

        public void FeedHnxNews()
        {
            Source source = SourceService.GetSource(2);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                // Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                //Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedHnxNews(source.URL + lnk.Link, source.URL, lnk.LinkId);
                //Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedHnxNews(string listUrl, string shortUrl, int linkId)
        {
            ParseHnxDocument parseDocument = new ParseHnxDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();            
            ParseHnxDocument parseDocument1;
            StockNew stockNew = null;

            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    HtmlNode link = null;
                    link = TableCells[i];                    
                    
                    if (link != null)
                    {
                        parseDocument1 = new ParseHnxDocument(shortUrl + link.Attributes["href"].Value.ToString());
                        
                        Console.WriteLine(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine("\n");
                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = DateTime.Now;
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        stockNew.NewsDate = Ultility.ConvertStringToDate(DateTime.Now.ToString("dd/MM/yyyy"));
                        stockNew.NewsDescription = Ultility.ReplaceAsciiToUniCode(link.InnerHtml.Normalize());
                        stockNew.NewsSource = "HNX";
                        stockNew.NewsTitle = Ultility.ReplaceAsciiToUniCode(link.InnerHtml.Normalize());
                        stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString() ;
                        stockNew.LinkId = linkId;
                        FeedItem(stockNew);
                    }
                }
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.Message);
            }
        }

        public void FeedStoxNews()
        {
            Source source = SourceService.GetSource(6);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedStoxNews(source.URL + lnk.Link, source.URL, lnk.LinkId);
                Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedStoxNews(string listUrl, string shortUrl, int linkId)
        {
            ParseStoxDocument parseDocument = new ParseStoxDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();         
            ParseStoxDocument parseDocument1;
            StockNew stockNew = null;
            
            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    HtmlNode link = null;
                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
                   
                    if (link != null)
                    {                   
                        parseDocument1 = new ParseStoxDocument(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));                        
                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        stockNew.NewsDate = DateTime.Now;//Ultility.ConvertStringToDate(newDate.InnerHtml.Replace("\r", ""));
                        stockNew.NewsDescription = link.InnerHtml.Normalize();
                        stockNew.NewsSource = "Stox";
                        stockNew.NewsTitle = link.InnerHtml.Normalize();
                        stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1);
                        stockNew.LinkId = linkId;
                        FeedItem(stockNew);
                    }
                }
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.Message);
            }
        }

        public bool IsFeedAlready()
        {
            return false;
        }

        //public DateTime ConvertStringToDate(string dateString)
        //{
        //    DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
        //    datefomatProvider.DateSeparator = "/";
        //    datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
        //    datefomatProvider.LongDatePattern = "dd/MM/yyyy";
        //    return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)));

        //    //return DateTime.Parse(dateString);
        //    //return Convert.ToDateTime(dateString, datefomatProvider);
        //}

        private void Initialize()
        {
            listError = new List<Exception>();
            numberOfitem = 0;
        }

        private static void WriteReportToDisk(string contents)
        {
            string fileName = ConfigurationManager.AppSettings["logTextFileName"].ToString();
            FileStream fStream = null;
            if (File.Exists(fileName))
            {
                fStream =File.OpenWrite(fileName);
                //fStream = File.AppendText(fileName);
            }
            else
            {
                fStream = File.OpenWrite(fileName);
            }

            using (TextWriter writer = new StreamWriter(fStream))
            {
                writer.WriteLine(contents);
                writer.Flush();
            }
            fStream.Dispose();
        }
    }
}
