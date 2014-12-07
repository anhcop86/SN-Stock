using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using StockApiReatime.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace StockApiReatime.Models
{
    public class StockPriceTicker
    {
         // Singleton instance
        private readonly static Lazy<StockPriceTicker> _instance = new Lazy<StockPriceTicker>(() => new StockPriceTicker(GlobalHost.ConnectionManager.GetHubContext<StockPriceHub>().Clients));

        private readonly ConcurrentDictionary<string, StockPrice> _stocks = new ConcurrentDictionary<string, StockPrice>();
        private string _stockSymbol = "";

        private readonly object _updateStockPricesLock = new object();

        //stock can go up or down by a percentage of this factor on each change
        private readonly double _rangePercent = .002;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(50000);
        private readonly Random _updateOrNotRandom = new Random();

        private readonly Timer _timer;
        private volatile bool _updatingStockPrices = false;

        private StockPriceTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;            
            GetStockPriceFromApi();
            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

        }

        public static StockPriceTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public StockPrice GetAllStocksTest(string stock)
        {
            _stockSymbol = stock;
            return _stocks.Values.FirstOrDefault(s=>s.CompanyID == stock);
        }



        public async void GetStockPriceFromApi()
        {
            // Returns data from a web service.
            //http://www.vfs.com.vn:6789/api/stocks
            //{PI_tickerList:'KLS|OGC|KBC'}
            //var client = new RestClient("http://www.vfs.com.vn:6789/");
            Uri uri = new Uri(ApplicationHelpers.URLApi);

            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameter = new { PI_tickerList = "KEYSECRET" };

                var response = client.PostAsJsonAsync("api/StockRealTime", parameter).Result;

                if (response.IsSuccessStatusCode)
                {
                    var list = await response.Content.ReadAsAsync<List<StockPrice>>();
                    if (list.Count > 0)
                    {
                        _stocks.Clear();
                        list.ForEach(stock => _stocks.TryAdd(stock.CompanyID, stock));
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

                    TryUpdateStockPrice();
                    BroadcastStockPriceGroup();

                    _updatingStockPrices = false;
                }
            }
        }

        private void TryUpdateStockPrice()
        {
            GetStockPriceFromApi();
        }

        private void BroadcastStockPrice()
        {
            Clients.All.updateStockPrice(_stocks.Values.FirstOrDefault(s => s.CompanyID == _stockSymbol));
        }
        private void BroadcastStockPriceGroup()
        {
            foreach (var item in _stocks)
            {
                Clients.Group(item.Value.CompanyID).updateStockPrice(_stocks.Values.FirstOrDefault(s => s.CompanyID == item.Value.CompanyID));   
            }           
        }
        
    }
}