using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Threading;

namespace WebApiStockPriceFromURL.Models
{
    public class GetMarketData
    {

        private static readonly GetMarketData _instance = new GetMarketData()
        {
        };
        private static string WebURL;
        public List<MarketDto> StockRealTimes { get; set; }
        private static string dataFromURL = string.Empty;
        private static Timer _timer;
        private static readonly object _updateStockPricesLock = new object();
        private static string[] IndexarrayS;
        private static string[] StockarrayS;
        private static volatile bool _updatingStockPrices = false;
        private static TimeSpan _updateInterval;
        private GetMarketData()
        {
            this.StockRealTimes = new List<MarketDto>();
            WebURL = Helper.ApiUrl;
            _updateInterval = TimeSpan.FromMilliseconds(Helper.DueTime);
            //set timmer

            GetStockPriceFromApi();
            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
        }
        public static GetMarketData GetInstance
        {
            get
            {
                return _instance;
            }
        }


        private void GetStockPriceFromApi()
        {
            try
            {
                if (this.StockRealTimes.Count > 0 && Helper.IsWeekendUpdatePrice) // Check weekend update and Stock markets do tradding
                {
                    return;
                }
                using (WebClient client = new WebClient())
                {
                    dataFromURL = client.DownloadString(WebURL);
                    IndexarrayS = dataFromURL.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)[2].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Index
                    StockarrayS = dataFromURL.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)[3].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Stock
                    this.StockRealTimes.Clear();

                    foreach (var item in StockarrayS)
                    {
                        string[] itemContain = item.Split(';');
                        this.StockRealTimes.Add(new MarketDto
                        {
                            Type = "S", //Stock
                            CompanyID = itemContain[0],                         // Close price : / Giá đóng cửa
                            FinishPrice = decimal.Parse(itemContain[8]) == 0 ? decimal.Parse(itemContain[1]) : decimal.Parse(itemContain[8]),// Giá thay đổi: giá tham chiếu - giá đóng cửa (Nếu giá đóng cửa = 0, lấy giá tham chiếu)
                            Diff = decimal.Parse(itemContain[7]),               // Diff : Giá trị thay đổi
                            DiffRate = (decimal.Parse(itemContain[8]) - decimal.Parse(itemContain[7])) == 0 ? 0 : decimal.Round(((decimal.Parse(itemContain[8]) / (decimal.Parse(itemContain[8]) - decimal.Parse(itemContain[7]))) - 1) * 100, 2)// DiffRate : Phần trăm thay đổi
                        });
                    }
                    //Index price / Lấy các chỉ số
                    foreach (var item in IndexarrayS)
                    {
                        string[] itemContain = item.Split(';');
                        this.StockRealTimes.Add(new MarketDto
                        {
                            Type = "I",                                         //Index Type / Loại Index
                            CompanyID = itemContain[0].ToUpper(),               // Close price : / Giá đóng cửa
                            FinishPrice = decimal.Parse(itemContain[2]),        // Giá thay đổi: giá tham chiếu - giá đóng cửa
                            Diff = decimal.Parse(itemContain[3]),               // Diff : Giá trị thay đổi
                            DiffRate = decimal.Parse(itemContain[4])            // DiffRate : Phần trăm thay đổi
                        });
                    }
                    // Price MACRO
                    this.StockRealTimes.Add(new MarketDto
                    {
                        Type = "O",                                             //Orther
                        CompanyID = "MACRO",                                    // Close price : / Giá đóng cửa
                        FinishPrice = decimal.Parse("99.99"),                   // Giá thay đổi: giá tham chiếu - giá đóng cửa
                        Diff = 10,                                              // Diff : Giá trị thay đổi
                        DiffRate = 100                                          // DiffRate : Phần trăm thay đổi
                    });
                }
            }
            catch (Exception)
            {
                // console.log
                //throw;
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