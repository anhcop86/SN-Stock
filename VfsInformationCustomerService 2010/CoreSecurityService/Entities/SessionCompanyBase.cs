using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSecurityService.Entities
{
    [Serializable]
    public class SessionCompanyBase
    {
        #region Variable Declaration
        private string _CompanyID = string.Empty;
        private decimal _CeilingPrice = 0;
        private decimal _FloorPrice = 0;
        private decimal _RefPrice = 0;
        private decimal _BuyPrice1 = 0;
        private int _BuyAmount1 = 0;
        private decimal _BuyPrice2 = 0;
        private int _BuyAmount2 = 0;
        private decimal _BuyPrice3 = 0;
        private int _BuyAmount3 = 0;
        private decimal _SellPrice1 = 0;
        private int _SellAmount1 = 0;
        private decimal _SellPrice2 = 0;
        private int _SellAmount2 = 0;
        private decimal _SellPrice3 = 0;
        private int _SellAmount3 = 0;
        #endregion

        public SessionCompanyBase() { }

        public string CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public decimal CeilingPrice
        {
            get { return _CeilingPrice; }
            set { _CeilingPrice = value; }
        }
        public decimal FloorPrice
        {
            get { return _FloorPrice; }
            set { _FloorPrice = value; }
        }
        public decimal RefPrice
        {
            get { return _RefPrice; }
            set { _RefPrice = value; }
        }
        public decimal BuyPrice1
        {
            get { return _BuyPrice1; }
            set { _BuyPrice1 = value; }
        }
        public int BuyAmount1
        {
            get {return _BuyAmount1; }
            set { _BuyAmount1 = value; }
        }
        public decimal BuyPrice2
        {
            get {return _BuyPrice2; }
            set { _BuyPrice2 = value; }
        }
        public int BuyAmount2
        {
            get {return _BuyAmount2 ; }
            set { _BuyAmount2 = value; }
        }
        public decimal BuyPrice3
        {
            get {return _BuyPrice3 ; }
            set { _BuyPrice3 = value; }
        }
        public int BuyAmount3
        {
            get {return _BuyAmount3; }
            set { _BuyAmount3 = value; }
        }
        public decimal SellPrice1
        {
            get {return _SellPrice1 ; }
            set { _SellPrice1 = value; }
        }
        public int SellAmount1
        {
            get {return _SellAmount1 ; }
            set { _SellAmount1 = value; }
        }
        public decimal SellPrice2
        {
            get {return _SellPrice2 ; }
            set { _SellPrice2 = value; }
        }
        public int SellAmount2
        {
            get {return _SellAmount2; }
            set { _SellAmount2 = value; }
        }
        public decimal SellPrice3
        {
            get {return _SellPrice3; }
            set { _SellPrice3 = value; }
        }
        public int SellAmount3
        {
            get {return _SellAmount3; }
            set { _SellAmount3 = value; }
        }

    }
}
