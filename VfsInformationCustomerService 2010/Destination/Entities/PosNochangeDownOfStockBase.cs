using System;
using System.Collections.Generic;
using System.Text;

namespace Vfs.WebCrawler.Destination.Entities
{
    public class PosNochangeDownOfStockBase
    {
        #region Variable Declarations

        private string _Date = string.Empty;
        private int _Pos = 0;
        private int _Nochange = 0;
        private int _Down = 0;
        private string _Market = string.Empty;

        #endregion
        #region Constructors
        public PosNochangeDownOfStockBase() { }
        public PosNochangeDownOfStockBase(
            string date,
            int Pos,
            int Nochange,
            int Down,
            string Market
            )
        {
            this._Date = date;
            this._Pos = Pos;
            this._Nochange = Nochange;
            this._Down = Down;
            this._Market = Market;
        }
        #endregion
        #region Properties

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        public int Pos
        {
            get { return _Pos; }
            set { _Pos = value; }
        }
        public int Nochange
        {
            get { return _Nochange; }
            set { _Nochange = value; }
        }
        public int Down
        {
            get { return _Down; }
            set { _Down = value; }
        }
        public string Market
        {
            get { return _Market; }
            set { _Market = value; }
        }
        #endregion
    }

    public enum PosNochangeDownOfStockColumns
    {
        Date,
        Pos,
        Nochange,
        Down,
        Market
    }
}
