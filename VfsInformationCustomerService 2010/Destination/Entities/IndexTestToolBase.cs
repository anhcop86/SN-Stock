using System;
using System.Collections.Generic;
using System.Text;

namespace Vfs.WebCrawler.Destination.Entities
{
    public class IndexTestToolBase
    {
        private string _Symbol = string.Empty;

        public string Symbol
        {
            get { return _Symbol; }
            set { _Symbol = value; }
        }

        private double _IndexSymbol = 0;

        public double IndexSymbol
        {
            get { return _IndexSymbol; }
            set { _IndexSymbol = value; }
        }

    }
    public enum IndexTestToolEnum
    {
        Symbol,
        IndexSymbol
    }
}
