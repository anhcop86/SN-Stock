using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace VfsInformationFeedService
{
    public class ParseCafefDocument
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

        public ParseCafefDocument()
        {
        }

        public ParseCafefDocument(string url)
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
            HtmlNode tableNode = HtmlDocument.DocumentNode.SelectSingleNode("//div[@class='ListCateDiv1']");
            HtmlNodeCollection tableCells = tableNode.SelectNodes("//div[@style='overflow: hidden; width: 540px']");

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
            HtmlNode divNode = null;
            HtmlNode innerdivNode = null;

            divNode = node.SelectSingleNode("div[@align='right']");
            //innerdivNode = divNode.SelectSingleNode("//div[@style='overflow: hidden;']");

            htmlHref = divNode.SelectSingleNode("a");
            return htmlHref;
        }

        public HtmlNode ParseNewsDate(HtmlNode node)
        {
            //HtmlNode divNewsDate = null;
            HtmlNode divNode = node.SelectSingleNode("//div[@style='overflow: hidden; width: 540px']");
            HtmlNode innerdivNode = divNode.SelectSingleNode("//div[@style='float:left; width:400px']");
            HtmlNodeCollection divNewsDate = innerdivNode.SelectNodes("div");
            return divNewsDate[0];
        }

        public HtmlNode ParseContent()
        {
            HtmlNode divNode = HtmlDocument.DocumentNode.SelectSingleNode("//div[@class='news_detail_div']");
            return divNode;
        }

        public HtmlNode ParseNewsDescription()
        {
            HtmlNode divNode = HtmlDocument.DocumentNode.SelectSingleNode("//div[@class='news_detail_div']");
            HtmlNode innerDivNode = divNode.SelectSingleNode("//h1[@class='DetailSapo']");
            return innerDivNode;
        }

        public HtmlNode ParseNewsTitle()
        {
            HtmlNode divNode = HtmlDocument.DocumentNode.SelectSingleNode("//div[@class='div_subtitle']");
            HtmlNode innerDivNode = divNode.SelectSingleNode("//div[@class='DetailTitleBlud']");
            return innerDivNode;
        }     
    }
}
