using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using HtmlAgilityPack;

namespace VfsCustomerInformationServices
{
    public class BaseFeed
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

        public BaseFeed()
        {
        }

        public BaseFeed(string url)
        {
            this.url = url;
            this.Initialize();
        }
       
        private void Initialize()
        {
            HtmlWeb hw = new HtmlWeb();
            this.htmlDocument = hw.Load(this.url);
        }
    }
}
