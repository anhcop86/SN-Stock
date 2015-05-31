using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace VfsCustomerInformationServices
{
    public class FeedCafeFDocument: BaseFeed, IFeedBehaviour
    {
        public FeedCafeFDocument() : base() { }

        public FeedCafeFDocument(string url) : base(url) { }

        public List<HtmlNode> ParseTableCell()
        {
            HtmlNode tableNode = HtmlDocument.DocumentNode.SelectSingleNode("//div[@class='ListCateDiv1']");
            HtmlNodeCollection tableCells = tableNode.SelectNodes("//div[@style='overflow: hidden; width: 580px']");

            List<HtmlNode> tableCellCollection = new List<HtmlNode>();

            foreach (HtmlNode cell in tableCells)
            {
                tableCellCollection.Add(cell);
            }
            return tableCellCollection;
        }

        public List<HtmlNode> ParseSpan(HtmlNode node)
        {
            HtmlNode htmlSpan;
            List<HtmlNode> htmlSpans = new List<HtmlNode>();
            htmlSpan = node.SelectSingleNode("table//span[@class]");
            htmlSpans.Add(htmlSpan);
            return htmlSpans;
        }

        public HtmlNode ParseLink(HtmlNode node)
        {
            HtmlNode htmlHref = null;
            HtmlNode htmlDiv = null;
            HtmlNode htmlDivTemp = null;
            Console.WriteLine(node.InnerHtml);
            Console.WriteLine("\n");
            htmlDivTemp = node.SelectSingleNode("div[@style='overflow: hidden;']");
            htmlDiv = htmlDivTemp.SelectSingleNode("div[@class='ListCateDiv3']");
            htmlHref = htmlDiv.SelectSingleNode("a[@class='dxncItemHeadernews']");
            return htmlHref;
        }

        public HtmlNode ParseNewsDate(HtmlNode node)
        {
            HtmlNode htmlHref = null;
            htmlHref = node.SelectSingleNode("div[@class='dxncItemDate_news']");
            return htmlHref;
        }

        public HtmlNode ParseContent()
        {
            HtmlNode divNode = HtmlDocument.DocumentNode.SelectSingleNode("//div[@class='KenhF_Content_News3']");
            return divNode;
        }

        public HtmlNode ParseNewsDescription()
        {
            HtmlNode divNode = HtmlDocument.DocumentNode.SelectSingleNode("//h1[@class='DetailSapo']");
            return divNode;
        }       
    }
}
