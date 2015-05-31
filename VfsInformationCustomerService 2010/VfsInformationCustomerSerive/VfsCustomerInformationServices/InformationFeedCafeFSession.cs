using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

namespace VfsCustomerInformationServices
{
    public class InformationFeedCafeFSession : InformationFeedSessionBase, IInformationFeedHoseSession
    {
        public InformationFeedCafeFSession(int numberOfItem) : base(numberOfItem) { }

        public void FeedNewsList()
        {
             Source source = SourceService.GetSource(4);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedNewsItem(source.URL + lnk.Link, source.URL + lnk.LinkShortDescription, lnk.LinkId);
                Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedNewsItem(string listUrl, string shortUrl, int linkId)
        {

            FeedCafeFDocument parseDocument = new FeedCafeFDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();            
            HtmlNode newDate;
            FeedCafeFDocument parseDocument1;
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
                        parseDocument1 = new FeedCafeFDocument(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine("\n");
                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = DateTime.Now;
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        stockNew.NewsDate = Ultility.ConvertStringToDate(newDate.InnerHtml.Replace("\r", ""));
                        stockNew.NewsDescription = ((HtmlNode)parseDocument1.ParseNewsDescription()).InnerHtml;
                        stockNew.NewsSource = "CafeF";
                        stockNew.NewsTitle = link.InnerHtml.Replace("\r", "");
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
    }
}
