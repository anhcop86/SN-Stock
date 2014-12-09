using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using PhimHang.Models;
using System.Web.Mvc;

namespace PhimHang.Hubs
{
    [HubName("StockRealTimeHub")]
    public class RealTimePriceHub : Hub
    {

        private readonly StockRealTimeTicker _stockRealtime;

        public RealTimePriceHub()
            : this(StockRealTimeTicker.Instance)
        {
        }
        public RealTimePriceHub(StockRealTimeTicker stockTicker)
        {
            _stockRealtime = stockTicker;
        }


        public StockRealTime GetStock(string stock)
        {            
            return _stockRealtime.GetAllStocksTest(stock).Result;
        }

        public List<StockRealTime> GetStockList(List<string> stock)
        {            
            return _stockRealtime.GetAllStocksTestList(stock).Result;
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