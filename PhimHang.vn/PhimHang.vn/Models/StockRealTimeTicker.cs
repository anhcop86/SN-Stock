using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PhimHang.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace PhimHang.Models
{
    public class StockRealTimeTicker
    {
        // Singleton instance
        private readonly static Lazy<StockRealTimeTicker> _instance = new Lazy<StockRealTimeTicker>(() => new StockRealTimeTicker(GlobalHost.ConnectionManager.GetHubContext<RealTimePriceHub>().Clients));
        private readonly List<StockRealTime> _stocks = new List<StockRealTime>();
        //private string _stockSymbol = "";

        private readonly object _updateStockPricesLock = new object();

        //stock can go up or down by a percentage of this factor on each change        

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(10000);
        private readonly Random _updateOrNotRandom = new Random();

        private readonly Timer _timer;
        private volatile bool _updatingStockPrices = false;
        private StockRealTimeTicker(IHubConnectionContext<dynamic> clients)
        {
            
            Clients = clients;            
            GetStockPriceFromApi();
            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public static StockRealTimeTicker Instance
        {
            get
            {                
                return _instance.Value;
            }
        }

        public async Task<StockRealTime> GetAllStocksTest(string stock)
        {
            var CompanyResult = Task.FromResult(_stocks.FirstOrDefault(s => s.CompanyID.ToUpper() == stock.ToUpper()));
            return await CompanyResult;
        }

        public Task<List<StockRealTime>> GetAllStocksTestList(List<string> stock)
        {
            var CompanyResult = Task.FromResult(_stocks.Where(s => stock.Contains(s.CompanyID) ).ToList());
            return  CompanyResult;
        }

        public async void GetStockPriceFromApi()
        {
            // Returns data from a web service.
            //http://www.vfs.com.vn:6789/api/stocks
            //{PI_tickerList:'KLS|OGC|KBC'}
            //var client = new RestClient("http://www.vfs.com.vn:6789/api/");
            Uri uri = new Uri("http://www.vfs.com.vn:6789/api");
            //Uri uri = new Uri("http://localhost:9999/api");

            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameter = new { PI_tickerList = "KEYSECRET" };

                var response = client.PostAsJsonAsync("/api/StockRealTime", parameter).Result;

                if (response.IsSuccessStatusCode)
                {
                    var list = await response.Content.ReadAsAsync<List<StockRealTime>>();
                    if (list.Count > 0)
                    {
                        _stocks.Clear();
                        list.ForEach(stock => _stocks.Add(stock));
                    }
                    //return list;
                }
                else
                {
                    //return new List<StockPrice>();
                }
            }
        }

        private void UpdateStockPrices(object state)
        {
            lock (_updateStockPricesLock)
            {
                if (!_updatingStockPrices)
                {
                    _updatingStockPrices = true;

                    GetStockPriceFromApi();
                    BroadcastStockPriceGroup();

                    _updatingStockPrices = false;
                }
            }
        }

        private void BroadcastStockPriceGroup()
        {
            foreach (var item in _stocks)
            {
                Clients.Group(item.CompanyID.ToUpper()).updateStockPrice(_stocks.FirstOrDefault(s => s.CompanyID == item.CompanyID));
            }    
        }

    }
}