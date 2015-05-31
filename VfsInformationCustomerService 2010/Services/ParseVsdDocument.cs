using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace VfsInformationFeedService
{
    public class ParseVsdDocument
    {
        private HtmlDocument htmlDocument;

        public HtmlDocument HtmlDocument
        {
            set { value = htmlDocument; }
            get { return htmlDocument; }
        }

        private string url;

        public string Url
        {
            set { value = url; }
            get { return url; }
        }

        public ParseVsdDocument()
        {
        }

        public ParseVsdDocument(string url)
        {
            this.url = url;
            this.Initialize();
        }

        private void Initialize()
        {
            HtmlWeb hw = new HtmlWeb();
            this.htmlDocument = hw.Load(this.url);
        }
        public List<HtmlNode> ParseTableCell()
        {
            HtmlNode tableNode = htmlDocument.DocumentNode.SelectSingleNode("//table[@id='table13']");
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

        public HtmlNode ParseSpan(HtmlNode node)
        {
            HtmlNode htmlSpan;
            htmlSpan = node.SelectSingleNode("table//span[@class]");
            return htmlSpan;
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

        public HtmlNode ParseContent()
        {
            HtmlNode tableNode = htmlDocument.DocumentNode.SelectSingleNode("//table[@id='table13']");
            HtmlNodeCollection HtmlNodeTrCollection = tableNode.SelectNodes("tr");
            HtmlNode HtmlNodeTr = HtmlNodeTrCollection[3];
            HtmlNodeCollection HtmlNodeTdCollection = HtmlNodeTr.SelectNodes("td");
            HtmlNode htmltd = HtmlNodeTdCollection[1];
            //htmlSpan = htmlDocument.DocumentNode.SelectSingleNode("//span[@id='ctl00_mainContent_lblSumary']");
            return htmltd;
        }
    }
}
