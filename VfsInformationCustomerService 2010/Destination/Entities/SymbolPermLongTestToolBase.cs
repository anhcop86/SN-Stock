using System;
using System.Collections.Generic;
using System.Text;

namespace Vfs.WebCrawler.Destination.Entities
{
    public class SymbolPermLongTestToolBase
    {
        private string _SymbolTo = string.Empty;

        public string SymbolTo
        {
            get { return _SymbolTo; }
            set { _SymbolTo = value; }
        }

        private double? _PriceCloseTo = 0;

        public double? PriceCloseTo
        {
            get { return _PriceCloseTo; }
            set { _PriceCloseTo = value; }
        }

        private double? _PriceHighTo = 0;

        public double? PriceHighTo
        {
            get { return _PriceHighTo; }
            set { _PriceHighTo = value; }
        }

        private string _SymbolFrom;

        public string SymbolFrom
        {
            get { return _SymbolFrom; }
            set { _SymbolFrom = value; }
        }

        private double? _PriceLowFrom;

        public double? PriceLowFrom
        {
            get { return _PriceLowFrom; }
            set { _PriceLowFrom = value; }
        }

        private double? _PriceCloseFrom;

        public double? PriceCloseFrom
        {
            get { return _PriceCloseFrom; }
            set { _PriceCloseFrom = value; }
        }
        private double? _AVGVolume;

        public double? AVGVolume
        {
            get { return _AVGVolume; }
            set { _AVGVolume = value; }
        }
	

    }
    public enum SymbolPermLongTestToolEnum
    {
        SymbolTo,
        PriceCloseTo,
        PriceHighTo,
        SymbolFrom,
        PriceLowFrom,
        PriceCloseFrom,
        AVGVolume
    }
}