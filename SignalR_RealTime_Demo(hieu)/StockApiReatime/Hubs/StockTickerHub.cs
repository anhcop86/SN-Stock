using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using StockApiReatime.Models;
using System.Threading.Tasks;

namespace StockApiReatime.Hubs
{
     [HubName("StockTickerMini")]
    public class StockTickerHub : Hub
    {
        private readonly StockTicker _stockticker;

        public StockTickerHub()
            : this(StockTicker.Instance)
        {
        }

        public StockTickerHub(StockTicker stockticker)
        {
            _stockticker = stockticker;
        }

        public List<StockPrice> GetAllStocks()
        {
            return _stockticker.GetAllStocksPrice();
        }
    }
}