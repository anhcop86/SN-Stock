using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApiStockPriceFromURL.Models
{
    public class StockMarketPrice
    {
        private static StockMarketPrice _instance;
        private static readonly string WebURL = @"http://stockboard.sbsc.com.vn/HO.ashx?FileName=0";
        private static readonly IDictionary<Type, object> _serviceCache;
        // ---------------------------------------------------------------------------------------
        public static List<StockResult> StockRealTimes = new List<StockResult>();        
        private static string dataFromURL = string.Empty;

        private static string[] IndexarrayS;
        private static string[] StockarrayS;

        public static StockMarketPrice Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_instance == null)
            {
                _instance = new StockMarketPrice();
            }

            return _instance;
        }
        /// <summary>
        /// Contructor
        /// </summary>
        static StockMarketPrice()
        {
            // get stock
            _serviceCache = new Dictionary<Type, object>();
            GetStockPriceFromApi();
            
        }
        static void GetStockPriceFromApi()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    dataFromURL = client.DownloadString(WebURL);
                    IndexarrayS = dataFromURL.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)[2].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Index
                    StockarrayS = dataFromURL.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)[3].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Stock

                    foreach (var item in StockarrayS)
                    {
                        string[] itemContain = item.Split(';');
                        StockRealTimes.Add(new StockResult { Name = itemContain[0], Finish = decimal.Parse(itemContain[8]) == 0 ? decimal.Parse(itemContain[1]) : decimal.Parse(itemContain[8]), Diff = decimal.Parse(itemContain[7]), Rate = decimal.Parse(itemContain[8]) == 0 ? 0 : decimal.Round(((decimal.Parse(itemContain[8]) - decimal.Parse(itemContain[1])) / decimal.Parse(itemContain[8])) * 100, 2) });
                    }
                    
                }
            }
            catch (Exception)
            {
                // console.log
                throw;
            }
        }

        public void Register<T>(T service)
        {
            var key = typeof(T);
            if (!_serviceCache.ContainsKey(key))
            {
                _serviceCache.Add(key, service);
            }
            else
            {
                _serviceCache[key] = service;
            }
        }
        public T GetService<T>()
        {
            var key = typeof(T);
            if (!_serviceCache.ContainsKey(key)) {
                throw new ArgumentException(string.Format("Type '{0}' has not been registered.", key.Name));
            }
            return (T)_serviceCache[key];
        }
        
    }  
}