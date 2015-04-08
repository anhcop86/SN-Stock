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
using System.Diagnostics;

namespace PhimHang.Models
{
    public class StockRealTimeTicker
    {
        // Singleton instance
        private readonly static Lazy<StockRealTimeTicker> _instance = new Lazy<StockRealTimeTicker>(() => new StockRealTimeTicker());//=> new StockRealTimeTicker(GlobalHost.ConnectionManager.GetHubContext<RealTimePriceHub>().Clients));
        private readonly List<StockRealTime> _stocks = new List<StockRealTime>();
        //private string _stockSymbol = "";

        private readonly object _updateStockPricesLock = new object();

        //stock can go up or down by a percentage of this factor on each change        

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(60000);
        //private readonly Random _updateOrNotRandom = new Random();

        private readonly Timer _timer;
        private volatile bool _updatingStockPrices = false;
        private StockRealTimeTicker()
        { 
            GetStockPriceFromApi();
            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

        }
              
        public static StockRealTimeTicker Instance
        {
            get
            {                
                return _instance.Value;
            }
        }

        public async Task<StockRealTime> GetStocksByTicker(string stock)
        {
            var CompanyResult = Task.FromResult(_stocks.FirstOrDefault(s => s.CompanyID.ToUpper() == stock.ToUpper()));
            return await CompanyResult;
        }

        public Task<List<StockRealTime>> GetAllStocksList(List<string> stock)
        {
            // Create new stopwatch
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //stopwatch.Stop();
            //var dsafd = stopwatch.Elapsed;
            var CompanyResult = (from s in _stocks                                 
                                 where stock.Contains(s.CompanyID)
                                 select s);

            //var CompanyResult = Task.FromResult(_stocks.Where(s => stock.Any(sl => sl == s.CompanyID)).ToList());
            
            return Task.FromResult(CompanyResult.ToList()); ;
        }
        public Task<List<StockRealTime>> RandomStocksList(List<string> stock)
        {            
            var stockListResult = (from s in _stocks
                                  orderby Guid.NewGuid()
                                  where !stock.Contains(s.CompanyID)
                                  select s).Take(10);            
            return Task.FromResult(stockListResult.ToList());
        }


        public async void GetStockPriceFromApi()
        {
            // Returns data from a web service.
            //http://www.vfs.com.vn:6789/api/stocks
            //{PI_tickerList:'KLS|OGC|KBC'}
            //var client = new RestClient("http://www.vfs.com.vn:6789/api/");
            Uri uri = new Uri("https://congdongchungkhoan.com/api");
            //Uri uri = new Uri("http://localhost:9999/api");
            //Uri uri = new Uri("http://125.234.253.11:6789/api");
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameter = new { PI_tickerList = "KEYSECRET" };

                
                try
                {
                    var response = client.PostAsJsonAsync("/api/StockRealTime", parameter).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var list = await response.Content.ReadAsAsync<List<StockRealTime>>();
                        if (_stocks.Count == 0 || (list.Count > 600 && AppHelper.CheckTimeUpdatePrice()))
                        {
                            _stocks.Clear();
                            list.ForEach(stock => _stocks.Add(stock));
                        }                       
                        
                    }
                    else
                    {
                        //return new List<StockPrice>();
                    }
                }
                catch (Exception)
                {
                    
                    
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
                    _updatingStockPrices = false;
                }
            }
        }

    }
}