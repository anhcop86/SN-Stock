using System;
using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;
using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

namespace VfsCustomerInformationServices
{
    public class InformationFeedHoseSession : InformationFeedSessionBase, IInformationFeedHoseSession 
    {
        public InformationFeedHoseSession(int numberOfItem) : base(numberOfItem) { }

        public void FeedNewsList()
        {
            Source source = SourceService.GetSource(1);
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

            //testing listUrl = @"http://www.hsx.vn/hsx/Modules/News/News.aspx?type=TTGD";
            FeedHoseDocument parseDocument = new FeedHoseDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();
            List<HtmlNode> spans;
            HtmlNode link;
            FeedHoseDocument parseDocument1;
            StockNew stockNew = null;

            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    spans = parseDocument.ParseSpan((HtmlNode)TableCells[i]);
                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
                    //parseDocument1 = new ParseDocument("http://www.hsx.vn/hsx/Modules/News/" + link.Attributes["href"].Value.ToString());
                    parseDocument1 = new FeedHoseDocument(shortUrl + link.Attributes["href"].Value.ToString());

                    HtmlNode content = parseDocument1.ParseContent();
                    stockNew = new StockNew();
                    stockNew.FeedDate = DateTime.Now;
                    stockNew.LanguageID = 2;
                    stockNew.NewsContent = content.InnerHtml;
                    stockNew.NewsDate = Ultility.ConvertStringToDate(spans[0].InnerHtml.ToString());
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
    }
}
