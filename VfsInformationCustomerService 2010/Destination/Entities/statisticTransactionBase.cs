using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Vfs.WebCrawler.Destination.Entities
{
    [Serializable]    
    public class statisticTransactionBase
    {
        private int _No = 0;
        public int No
        {
            get { return _No; }
            set { _No = value; }
        }

        private string _Symbol = string.Empty;
        public string Symbol
        {
            get { return _Symbol; }
            set { _Symbol = value; }
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

        private double _Change = 0;
        public double Change
        {
            get { return _Change; }
            set { _Change = value; }
        }

        private double _DVDMTrungBinhTrenLenh = 0;
        public double DVDMTrungBinhTrenLenh
        {
            get { return _DVDMTrungBinhTrenLenh; }
            set { _DVDMTrungBinhTrenLenh = value; }
        }

        private double _DVDBTrungBinhTrenLenh = 0;
        public double DVDBTrungBinhTrenLenh
        {
            get { return _DVDBTrungBinhTrenLenh; }
            set { _DVDBTrungBinhTrenLenh = value; }
        }

        private double _Volume = 0;
        public double Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }

        private double _TotalValue = 0;
        public double TotalValue
        {
            get { return _TotalValue; }
            set { _TotalValue = value; }
        }
    }
    public enum StatisticTransactionBaseColumn
    {
        No,
        Symbol,
        BuyCount,
        BuyQuantity,
        SellCount,
        SellQuantity,
        Change,
        DVDMTrungBinhTrenLenh,
        DVDBTrungBinhTrenLenh,
        Volume,
        TotalValue
    }
}
