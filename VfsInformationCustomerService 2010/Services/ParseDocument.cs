using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace VfsInformationFeedService
{
    public class ParseDocument
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

        public ParseDocument()
        {
        }

        public ParseDocument(string url)
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
            HtmlNodeCollection tableCells = htmlDocument.DocumentNode.SelectNodes("//td[@class]");
            List<HtmlNode> tableCellCollection = new List<HtmlNode>();

            //if (tableCells == null)
            //    return tableCellCollection;

            foreach (HtmlNode cell in tableCells)
            {
                if (cell.Attributes["class"].Value == "news_padding")
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
            htmlHref = node.SelectSingleNode("table//a[@class]");
            return htmlHref;
        }

        public HtmlNode ParseContent()
        {
            HtmlNode htmlSpan;
            HtmlNode htmlAttachedFiles = null;
            htmlSpan = htmlDocument.DocumentNode.SelectSingleNode("//span[@id='ctl00_mainContent_lblSumary']");
            htmlSpan.InnerHtml = "<span>" + htmlSpan.InnerHtml + htmlDocument.DocumentNode.SelectSingleNode("//span[@id='ctl00_mainContent_lblContent']").InnerHtml + "</span>";
            
            htmlAttachedFiles = htmlDocument.DocumentNode.SelectSingleNode("//span[@id='ctl00_mainContent_dtlAttachedFile']");
            HtmlNode nodeTable = htmlDocument.DocumentNode.SelectSingleNode("//table[@id='ctl00_mainContent_dtlAttachedFile']");            

            if(nodeTable !=null)
            {
                HtmlNodeCollection rows = nodeTable.SelectNodes("tr");
                if (nodeTable.HasChildNodes)
                {
                    if (rows[1] != null)
                    {
                        HtmlNode cell = ((HtmlNode)rows[1]).SelectSingleNode("td");
                        htmlSpan.InnerHtml = htmlSpan.InnerHtml + "<br/>" + cell.InnerHtml;
                    }
                }
            }
            return htmlSpan;
        }
    }
}
