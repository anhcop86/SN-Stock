using System;
using System.Collections.Generic;
using System.Text;

namespace Vfs.WebCrawler.Destination.Entities
{
    public class stock_SymbolPermLong : Stock_SymbolPermLongBase
    {
    }

    public class stock_SymbolPermLongExtension : Stock_SymbolPermLongBase
    {
        private string _Symbol = string.Empty;
        public string Symbol
        {
            get { return _Symbol; }
            set { _Symbol = value; }
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

        private double _ChenhLechGia = 0;        
        public double ChenhLechGia
        {
            get { return _ChenhLechGia; }
            set { _ChenhLechGia = value; }
        }

        private double _KLGDTBTrenPhien = 0;
        public double KLGDTBTrenPhien
        {
            get { return _KLGDTBTrenPhien; }
            set { _KLGDTBTrenPhien = value; }
        }

        private double _GTGDTrungBinhTrenPhien = 0;
        public double GTGDTrungBinhTrenPhien
        {
            get { return _GTGDTrungBinhTrenPhien; }
            set { _GTGDTrungBinhTrenPhien = value; }
        }

        private double _KLDMBaMuoiPhien = 0;
        public double KLDMBaMuoiPhien
        {
            get { return _KLDMBaMuoiPhien; }
            set { _KLDMBaMuoiPhien = value; }
        }

        private double _KLDBBaMuoiPhien = 0;
        public double KLDBBaMuoiPhien
        {
            get { return _KLDBBaMuoiPhien; }
            set { _KLDBBaMuoiPhien = value; }
        }

        private double _KLDMTruKLDBBaMuoiPhien = 0;
        public double KLDMTruKLDBBaMuoiPhien
        {
            get { return _KLDMTruKLDBBaMuoiPhien; }
            set { _KLDMTruKLDBBaMuoiPhien = value; }
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

        private double _NDTNNKLMuaBaMuoiPhien = 0;
        public double NDTNNKLMuaBaMuoiPhien
        {
            get { return _NDTNNKLMuaBaMuoiPhien; }
            set { _NDTNNKLMuaBaMuoiPhien = value; }
        }

        private double _NDTNNGTMuaBaMuoiPhien = 0;
        public double NDTNNGTMuaBaMuoiPhien
        {
            get { return _NDTNNGTMuaBaMuoiPhien; }
            set { _NDTNNGTMuaBaMuoiPhien = value; }
        }

        private double _NDTNNKLBanBaMuoiPhien = 0;
        public double NDTNNKLBanBaMuoiPhien
        {
            get { return _NDTNNKLBanBaMuoiPhien; }
            set { _NDTNNKLBanBaMuoiPhien = value; }
        }

        private double _NDTNNGTBanBaMuoiPhien = 0;
        public double NDTNNGTBanBaMuoiPhien
        {
            get { return _NDTNNGTBanBaMuoiPhien; }
            set { _NDTNNGTBanBaMuoiPhien = value; }
        }

        private double _NDTNNKLMuaTruBanBaMuoiPhien = 0;
        public double NDTNNKLMuaTruBanBaMuoiPhien
        {
            get { return _NDTNNKLMuaTruBanBaMuoiPhien; }
            set { _NDTNNKLMuaTruBanBaMuoiPhien = value; }
        }
    }
    public enum stock_SymbolPermLongExtensionColumns
    {
        Symbol,
        PriceClose,
        PriceHigh,
        PriceLow,
        ChenhLechGia,
        KLGDTBTrenPhien,
        GTGDTrungBinhTrenPhien,
        KLDMBaMuoiPhien,
        KLDBBaMuoiPhien,
        KLDMTruKLDBBaMuoiPhien,
        DVDMTrungBinhTrenLenh,
        DVDBTrungBinhTrenLenh,
        NDTNNKLMuaBaMuoiPhien,
        NDTNNGTMuaBaMuoiPhien,
        NDTNNKLBanBaMuoiPhien,
        NDTNNGTBanBaMuoiPhien,
        NDTNNKLMuaTruBanBaMuoiPhien
    }
}
