using System;
using System.Collections.Generic;
using System.Text;

namespace Vfs.WebCrawler.Destination.Entities
{
    public class ExportDataForMetaStoxBase
    {
        private string _Symbol = string.Empty;

        public string Symbol
        {
            get { return _Symbol; }
            set { _Symbol = value; }
        }
        private string _PermDate = string.Empty;

        public string PermDate
        {
            get { return _PermDate; }
            set { _PermDate = value; }
        }

        private double _PriceOpen = 0;

        public double PriceOpen
        {
            get { return _PriceOpen; }
            set { _PriceOpen = value; }
        }

        private double _PriceClose = 0;

        public double PriceClose
        {
            get { return _PriceClose; }
            set { _PriceClose = value; }
        }

        private double _PriceHigh = 0;

        public double PriceHigh
        {
            get { return _PriceHigh; }
            set { _PriceHigh = value; }
        }

        private double _PriceLow = 0;

        public double PriceLow
        {
            get { return _PriceLow; }
            set { _PriceLow = value; }
        }
        
        private double _Volume = 0;

        public double Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }


    }
    public enum ExportDataForMetaStoxBaseColumns
    {
        Symbol,
        PermDate,
        PriceOpen,
        PriceClose,
        PriceHigh,
        PriceLow,       
        Volume,            }//End enum
}
