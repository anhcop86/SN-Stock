using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using HtmlAgilityPack;
using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

namespace VfsInformationFeedService
{
    class ParseVnEconomyDocument
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

        public ParseVnEconomyDocument()
        {
        }

        public ParseVnEconomyDocument(string url)
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
            HtmlNode DivNodeIn = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='divListQ_CM']");
            HtmlNode TableNode = DivNodeIn.SelectSingleNode("table");
            HtmlNodeCollection tableCells = TableNode.SelectNodes("tr");
            List<HtmlNode> tableCellCollection = new List<HtmlNode>();
            int index = 0;
            foreach (HtmlNode cell in tableCells)
            {
                if (index > 0 && index < 16) tableCellCollection.Add(cell);
                index++;
            }
            return tableCellCollection;
        }

        public HtmlNode ParseSpan(HtmlNode node)
        {
            HtmlNode htmlSpan;
            HtmlNode htmlTd;
            htmlTd = node.SelectSingleNode("td");
            HtmlNode htmlDiv = htmlTd.SelectSingleNode("div[class='width_310_f_l_p_b_7']");
            htmlSpan = htmlDiv.SelectSingleNode("span[@class='title_danhsachtin']");
            return htmlSpan;
        }

        public HtmlNode ParseLink(HtmlNode node)
        {
            HtmlNode htmlLink;
            HtmlNode htmlSpan;
            HtmlNode htmlTd;
            htmlTd = node.SelectSingleNode("td");
            HtmlNode htmlDiv = htmlTd.SelectSingleNode("div");
            htmlSpan = htmlDiv.SelectSingleNode("span[@class='title_danhsachtin']");
            htmlLink = htmlSpan.SelectSingleNode("a");
            return htmlLink;
        }

        public HtmlNode ParseNewsDate(HtmlNode node)
        {
            HtmlNode htmlHref = null;
            htmlHref = node.SelectSingleNode("div[@class='dxncItemDate_news']");
            return htmlHref;
        }

        public HtmlNode ParseContent()
        {
            HtmlNode divNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='divBody']");
            return divNode;
        }

        public HtmlNode ParseNewsDescription()
        {
            HtmlNode divNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='divBody']");
            HtmlNode divSpan = divNode.SelectSingleNode("span[@class='ff_time_news_roman_fs_12pt']");
            HtmlNode divSpan2 = divSpan.SelectSingleNode("span[@style='font-weight: bold;']");
            return divSpan2;
        }   
    }
}
