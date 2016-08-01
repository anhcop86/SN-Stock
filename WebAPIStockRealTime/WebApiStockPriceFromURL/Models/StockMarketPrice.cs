//using Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading;
//using System.Web;
//using System.Web.Helpers;

//namespace WebApiStockPriceFromURL.Models
//{
//    public class StockMarketPrice
//    {
//        //private static StockMarketPrice _instance;
//        private static readonly string WebURL = Helper.ApiUrl;
//        //private static readonly IDictionary<Type, object> _serviceCache;
//        // ---------------------------------------------------------------------------------------
//        public static List<StockResult> StockRealTimes = new List<StockResult>();
//        private static string dataFromURL = string.Empty;
//        private static readonly Timer _timer;
//        private static readonly object _updateStockPricesLock = new object();
//        private static string[] IndexarrayS;
//        private static string[] StockarrayS;
//        private static volatile bool _updatingStockPrices = false;
//        private static readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(40000);
//        private static readonly int timeExpireWebCache = 20; // minutes
//        public static List<StockResult> PriceCache
//        {
//            get
//            {
//                if (WebCache.Get("KeyPrice") == null) // Has expired
//                {
//                    GetStockPriceFromApi();
//                }
//                return WebCache.Get("KeyPrice");
//            }
//            private set { }

//        }
//        public static void ValidateCache()
//        {
//            //if (WebCache.Get("KeyPrice") == null) {
//            //    IsCache = false;                
//            //}
//            //if (IsCache == false) {
//            //    WebCache.Set("KeyPrice", StockRealTimes, timeExpireWebCache, false);
//            //}
//        }
//        //public static List<StockResult> RandomStockPrice()
//        //{
//        //    if (true) {

//        //    }
//        //}
//        //public static StockMarketPrice Instance()
//        //{
//        //    // Uses lazy initialization.
//        //    // Note: this is not thread safe.
//        //    if (_instance == null)
//        //    {
//        //        _instance = new StockMarketPrice();
//        //    }

//        //    return _instance;
//        //}
//        /// <summary>
//        /// Contructor
//        /// </summary>
//        static StockMarketPrice()
//        {
//            GetStockPriceFromApi();
//            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
//        }
//        static void GetStockPriceFromApi()
//        {
//            try
//            {
//                if (StockRealTimes.Count > 0 && Helper.IsWeekendUpdatePrice && WebCache.Get("KeyPrice") != null) // Check weekend update and Stock markets do tradding
//                {
//                    return;
//                }
//                using (WebClient client = new WebClient())
//                {
//                    dataFromURL = client.DownloadString(WebURL);
//                    IndexarrayS = dataFromURL.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)[2].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Index
//                    StockarrayS = dataFromURL.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)[3].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Stock
//                    StockRealTimes.Clear();
//                    // Lấy chỉ số danh muc thường
//                    foreach (var item in StockarrayS)
//                    {
//                        string[] itemContain = item.Split(';');
//                        StockRealTimes.Add(new StockResult
//                        {
//                            Type = "S", //Stock
//                            CompanyID = itemContain[0],
//                            // Close price : / Giá đóng cửa
//                            FinishPrice = decimal.Parse(itemContain[8]) == 0 ? decimal.Parse(itemContain[1]) : decimal.Parse(itemContain[8]),
//                            // Giá thay đổi: giá tham chiếu - giá đóng cửa
//                            Diff = decimal.Parse(itemContain[7]),
//                            // DiffRate : Phần trăm thay đổi
//                            DiffRate = (decimal.Parse(itemContain[8]) - decimal.Parse(itemContain[7])) == 0 ? 0 : decimal.Round(((decimal.Parse(itemContain[8]) / (decimal.Parse(itemContain[8]) - decimal.Parse(itemContain[7]))) - 1) * 100, 2)
//                        });
//                    }
//                    //Index price / Lấy các chỉ số
//                    foreach (var item in IndexarrayS)
//                    {
//                        string[] itemContain = item.Split(';');
//                        StockRealTimes.Add(new StockResult
//                        {
//                            Type = "I", //Index
//                            CompanyID = itemContain[0].ToUpper(),
//                            // Close price : / Giá đóng cửa
//                            FinishPrice = decimal.Parse(itemContain[2]),
//                            // Giá thay đổi: giá tham chiếu - giá đóng cửa
//                            Diff = decimal.Parse(itemContain[3]),
//                            // DiffRate : Phần trăm thay đổi
//                            DiffRate = decimal.Parse(itemContain[4])
//                        });
//                    }
//                    // Price MACRO
//                    StockRealTimes.Add(new StockResult
//                    {
//                        Type = "O", //Orther

//                        CompanyID = "MACRO",
//                        // Close price : / Giá đóng cửa
//                        FinishPrice = decimal.Parse("99.99"),
//                        // Giá thay đổi: giá tham chiếu - giá đóng cửa
//                        Diff = 10,
//                        // DiffRate : Phần trăm thay đổi
//                        DiffRate = 100
//                    });
//                    WebCache.Set("KeyPrice", StockRealTimes, timeExpireWebCache, true);

//                }
//            }
//            catch (Exception)
//            {
//                // console.log
//                //throw;
//            }
//        }

//        //public void Register<T>(T service)
//        //{
//        //    var key = typeof(T);
//        //    if (!_serviceCache.ContainsKey(key))
//        //    {
//        //        _serviceCache.Add(key, service);
//        //    }
//        //    else
//        //    {
//        //        _serviceCache[key] = service;
//        //    }
//        //}
//        //public T GetService<T>()
//        //{
//        //    var key = typeof(T);
//        //    if (!_serviceCache.ContainsKey(key)) {
//        //        throw new ArgumentException(string.Format("Type '{0}' has not been registered.", key.Name));
//        //    }
//        //    return (T)_serviceCache[key];
//        //}
//        private static void UpdateStockPrices(object state)
//        {
//            lock (_updateStockPricesLock)
//            {
//                if (!_updatingStockPrices)
//                {
//                    _updatingStockPrices = true;
//                    GetStockPriceFromApi();
//                    _updatingStockPrices = false;
//                }
//            }
//        }

//    }
//}