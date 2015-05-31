using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace VfsInformationFeedService
{  
    public class ParseStoxDocument
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

        public ParseStoxDocument()
        {
        }

        public ParseStoxDocument(string url)
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
            HtmlNode tableNode = HtmlDocument.DocumentNode.SelectSingleNode("//td[@class='e_NewsTitle']");
            HtmlNode ulNode = tableNode.SelectSingleNode("ul");
            HtmlNodeCollection liCollection = ulNode.SelectNodes("li");

            List<HtmlNode> tableCellCollection = new List<HtmlNode>();

            foreach (HtmlNode cell in liCollection)
            {
                tableCellCollection.Add(cell);
            }

            HtmlNodeCollection tableNodeColl = HtmlDocument.DocumentNode.SelectNodes("//td[@class='e_NewsTitle']");
            if(tableNodeColl[1] != null)
            {
                ulNode = tableNodeColl[1].SelectSingleNode("ul");
                liCollection = ulNode.SelectNodes("li");
            }

            if (liCollection != null)
            {
                if (liCollection.Count > 0)
                {
                    foreach (HtmlNode cell in liCollection)
                    {
                        tableCellCollection.Add(cell);
                    }
                }
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
            htmlHref = node.SelectSingleNode("a");
            
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
            HtmlNode divNode = null;
            HtmlNode tdNode = HtmlDocument.DocumentNode.SelectSingleNode("//td[@style='border-top:#e4e4e4 solid 1px;padding-top:10px;']");
            HtmlNode innerTable = tdNode.SelectSingleNode("table");
            HtmlNode innerTableTr = innerTable.SelectSingleNode("tr");
            HtmlNode innerTableTd = innerTableTr.SelectSingleNode("td");

            HtmlNode level2InnerTable = innerTableTd.SelectSingleNode("table");
            HtmlNodeCollection level2InnerTrCollection = level2InnerTable.SelectNodes("tr");
            
            HtmlNode level2InnerTd = null;
            
            if (level2InnerTrCollection[2] != null)
            {
                level2InnerTd = level2InnerTrCollection[2].SelectSingleNode("td");
            }

            if (level2InnerTd != null)
            {
                HtmlNode level3InnerTable = level2InnerTd.SelectSingleNode("table");
                HtmlNode level3InnerTr = level3InnerTable.SelectSingleNode("tr");
                HtmlNode level3InnerTd = level3InnerTr.SelectSingleNode("td");
                divNode = level3InnerTd;
            }
            
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
