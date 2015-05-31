using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace VfsCustomerInformationServices
{
    public class FeedVsdDocument : BaseFeed, IFeedBehaviour
    {

        public FeedVsdDocument() : base() { }

        public FeedVsdDocument(string url) : base(url) { }

        public List<HtmlNode> ParseTableCell()
        {
            HtmlNode tableNode = HtmlDocument.DocumentNode.SelectSingleNode("//table[@id='table13']");
            HtmlNodeCollection tableCells = tableNode.SelectNodes("tr//td");

            List<HtmlNode> tableCellCollection = new List<HtmlNode>();

            //if (tableCells == null)
            //    return tableCellCollection;

            foreach (HtmlNode cell in tableCells)
            {
                if (cell.InnerHtml.IndexOf("Sau</span></a>", 0) < 0)
                {
                    tableCellCollection.Add(cell);
                }
            }
            return tableCellCollection;
        }

        public List<HtmlNode> ParseSpan(HtmlNode node)
        {
            List<HtmlNode> parseSpans = new List<HtmlNode>();
            return parseSpans;
        }

        public HtmlNode ParseLink(HtmlNode node)
        {
            HtmlNode htmlHref = null;
            //HtmlNodeCollection tableCells = node.SelectNodes("//a[@class]");
            //foreach (HtmlNode cell in tableCells)
            //{
            //    if (cell.Attributes["class"].Value == "title")
            //    {
            //        htmlHref = cell;
            //    }
            //}
            htmlHref = node.SelectSingleNode("a[@class='link1']");
            return htmlHref;
        }

        public HtmlNode ParseNewsDate(HtmlNode node)
        {
            HtmlNode newsDate = null;
            return newsDate;
        }

        public HtmlNode ParseContent()
        {
            HtmlNode tableNode = HtmlDocument.DocumentNode.SelectSingleNode("//table[@id='table13']");
            HtmlNodeCollection HtmlNodeTrCollection = tableNode.SelectNodes("tr");
            HtmlNode HtmlNodeTr = HtmlNodeTrCollection[3];
            HtmlNodeCollection HtmlNodeTdCollection = HtmlNodeTr.SelectNodes("td");
            HtmlNode htmltd = HtmlNodeTdCollection[1];
            //htmlSpan = htmlDocument.DocumentNode.SelectSingleNode("//span[@id='ctl00_mainContent_lblSumary']");
            return htmltd;
        }

        public HtmlNode ParseNewsDescription()
        {
            HtmlNode parseNewsDescription = null;
            return parseNewsDescription;
        }
    }
}
