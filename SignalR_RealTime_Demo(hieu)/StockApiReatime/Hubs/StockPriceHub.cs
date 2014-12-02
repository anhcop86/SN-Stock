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
    [HubName("StockPriceTicker")]
    public class StockPriceHub : Hub
    {
       private readonly StockPriceTicker _stockticker;

        public StockPriceHub()
           : this(StockPriceTicker.Instance)
        {
        }

        public StockPriceHub(StockPriceTicker stockticker)
        {
            
            _stockticker = stockticker;
        }

        public StockPrice GetAllStocks(string stock)
        {
            
            return _stockticker.GetAllStocksTest(stock);
        }

        public Task JoinRoom(string groupStock)
        {
            return Groups.Add(Context.ConnectionId, groupStock);
            
        }

        public Task LeaveRoom(string groupStock)
        {
            return Groups.Remove(Context.ConnectionId, groupStock);
        }
    }
}