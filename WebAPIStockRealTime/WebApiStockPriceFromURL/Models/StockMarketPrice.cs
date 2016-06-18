using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Helpers;

namespace WebApiStockPriceFromURL.Models
{
    public class StockMarketPrice
    {
        //private static StockMarketPrice _instance;
        private static readonly string WebURL = Helper.ApiUrl;
        //private static readonly IDictionary<Type, object> _serviceCache;
        // ---------------------------------------------------------------------------------------
        public static List<StockResult> StockRealTimes = new List<StockResult>();
        private static string dataFromURL = string.Empty;
        private static readonly Timer _timer;
        private static readonly object _updateStockPricesLock = new object();
        private static string[] IndexarrayS;
        private static string[] StockarrayS;
        private static volatile bool _updatingStockPrices = false;
        private static readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(40000);
        private static readonly int timeExpireWebCache = 1; // minutes
        private static bool IsCache { get; set; }
        public static List<StockResult> PriceCache
        {
            get
            {
                //if (IsCache == false) {
                //    ValidateCache();
                //}
                return WebCache.Get("KeyPrice");
            }
            private set { }
           
        }
        public static void ValidateCache()
        {
            //if (WebCache.Get("KeyPrice") == null) {
            //    IsCache = false;                
            //}
            //if (IsCache == false) {
            //    WebCache.Set("KeyPrice", StockRealTimes, timeExpireWebCache, false);
            //}
        }
        //public static List<StockResult> RandomStockPrice()
        //{
        //    if (true) {
                
        //    }
        //}
        //public static StockMarketPrice Instance()
        //{
        //    // Uses lazy initialization.
        //    // Note: this is not thread safe.
        //    if (_instance == null)
        //    {
        //        _instance = new StockMarketPrice();
        //    }

        //    return _instance;
        //}
        /// <summary>
        /// Contructor
        /// </summary>
        static StockMarketPrice()
        {
            // get stock
            //_serviceCache = new Dictionary<Type, object>();
            IsCache = true;
            GetStockPriceFromApi();
            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
        }
        static void GetStockPriceFromApi()
        {
            try
            {
                if (StockRealTimes.Count > 0 && Helper.CheckTimeUpdatePrice() == false) {
                    return;
                }
                using (WebClient client = new WebClient())
                {
                    dataFromURL = client.DownloadString(WebURL);
                    IndexarrayS = dataFromURL.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)[2].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Index
                    StockarrayS = dataFromURL.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)[3].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Stock
                    StockRealTimes.Clear();
                    foreach (var item in StockarrayS)
                    {
                        string[] itemContain = item.Split(';');
                        StockRealTimes.Add(new StockResult {
                            CompanyID = itemContain[0],
                            FinishPrice = decimal.Parse(itemContain[8]) == 0 ? decimal.Parse(itemContain[1]) : decimal.Parse(itemContain[8]),
                            Diff = decimal.Parse(itemContain[7]),
                            DiffRate = decimal.Parse(itemContain[8]) == 0 ? 0 : decimal.Round(((decimal.Parse(itemContain[8]) - decimal.Parse(itemContain[1])) / decimal.Parse(itemContain[8])) * 100, 2)
                        });
                    }
                    WebCache.Set("KeyPrice", StockRealTimes, timeExpireWebCache, true);
                    
                }
            }
            catch (Exception)
            {
                // console.log
                //throw;
            }
        }

        //public void Register<T>(T service)
        //{
        //    var key = typeof(T);
        //    if (!_serviceCache.ContainsKey(key))
        //    {
        //        _serviceCache.Add(key, service);
        //    }
        //    else
        //    {
        //        _serviceCache[key] = service;
        //    }
        //}
        //public T GetService<T>()
        //{
        //    var key = typeof(T);
        //    if (!_serviceCache.ContainsKey(key)) {
        //        throw new ArgumentException(string.Format("Type '{0}' has not been registered.", key.Name));
        //    }
        //    return (T)_serviceCache[key];
        //}
        private static void UpdateStockPrices (object state)
        {
            lock (_updateStockPricesLock) {
                if (!_updatingStockPrices) {
                    _updatingStockPrices = true;
                    GetStockPriceFromApi();
                    _updatingStockPrices = false;
                }
            }
        }
        
    }  
}