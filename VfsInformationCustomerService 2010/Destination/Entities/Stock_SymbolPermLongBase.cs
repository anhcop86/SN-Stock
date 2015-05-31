using System;
using System.Collections.Generic;
using System.Text;

namespace Vfs.WebCrawler.Destination.Entities
{
    [Serializable]
    public class Stock_SymbolPermLongBase
    {
        #region Variable Declarations
        private int _SymbolID = 0;

        public int SymbolID
        {
            get { return _SymbolID; }
            set { _SymbolID = value;}
        }

        private DateTime _PermDate = new DateTime(1900,1,1,0,0,0,0);

        public DateTime PermDate
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

        private double _PriceAverage = 0;

        public double PriceAverage
        {
            get { return _PriceAverage; }
            set { _PriceAverage = value; }
        }

        private double _PricePreviousClose = 0;

        public double PricePreviousClose
        {
            get { return _PricePreviousClose; }
            set { _PricePreviousClose = value; }
        }

        private double _Volume = 0;

        public double Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }

        private double _TotalTrade = 0;

        public double TotalTrade
        {
            get { return _TotalTrade; }
            set { _TotalTrade = value; }
        }

        private double _TotalValue = 0;

        public double TotalValue
        {
            get { return _TotalValue; }
            set { _TotalValue = value; }
        }

        private double _AdjRatio = 0;

        public double AdjRatio
        {
            get { return _AdjRatio; }
            set { _AdjRatio = value; }
        }

        private DateTime _LastUpdated = new DateTime(1900,1,1,0,0,0,0);

        public DateTime LastUpdated
        {
            get { return _LastUpdated; }
            set { _LastUpdated = value; }
        }

        private double  _CurrentForeignRoom;

        public double CurrentForeignRoom
        {
            get { return _CurrentForeignRoom; }
            set { _CurrentForeignRoom = value; }
        }

        private double _BuyCount = 0;

        public double BuyCount
        {
            get { return _BuyCount; }
            set { _BuyCount = value; }
        }

        private double _BuyQuantity = 0;

        public double BuyQuantity
        {
            get { return _BuyQuantity; }
            set { _BuyQuantity = value; }
        }

        private double _SellCount = 0;

        public double SellCount
        {
            get { return _SellCount; }
            set { _SellCount = value; }
        }

        private double _SellQuantity = 0;

        public double SellQuantity
        {
            get { return _SellQuantity; }
            set { _SellQuantity = value; }
        }

        private double _BuyForeignCount = 0;

        public double BuyForeignCount
        {
            get { return _BuyForeignCount; }
            set { _BuyForeignCount = value; }
        }

        private double _BuyForeignQuantity = 0;

        public double BuyForeignQuantity
        {
            get { return _BuyForeignQuantity; }
            set { _BuyForeignQuantity = value; }
        }

        private double _BuyForeignValue = 0;

        public double BuyForeignValue
        {
            get { return _BuyForeignValue; }
            set { _BuyForeignValue = value; }
        }

        private double _SellForeignCount = 0;

        public double SellForeignCount
        {
            get { return _SellForeignCount; }
            set { _SellForeignCount = value; }
        }

        private double _SellForeignQuantity = 0;

        public double SellForeignQuantity
        {
            get { return _SellForeignQuantity; }
            set { _SellForeignQuantity = value; }
        }

        private double _SellForeignValue = 0;

        public double SellForeignValue
        {
            get { return _SellForeignValue; }
            set { _SellForeignValue = value; }
        }
        
        #endregion

        
    }

    public enum stock_SymbolPermLongColumns
    {
        SymbolID,
        PermDate,
        PriceOpen,
        PriceClose,
        PriceHigh,
        PriceLow,
        PriceAverage,
        PricePreviousClose,
        Volume,
        TotalTrade,
        TotalValue,
        AdjRatio,
        LastUpdated,
        CurrentForeignRoom,
        BuyCount,
        BuyQuantity,
        SellCount,
        SellQuantity,
        BuyForeignCount,
        BuyForeignQuantity,
        BuyForeignValue,
        SellForeignCount,
        SellForeignQuantity,
        SellForeignValue
    }//End enum
}
