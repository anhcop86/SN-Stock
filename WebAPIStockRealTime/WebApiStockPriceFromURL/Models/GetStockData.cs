using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Threading;
using System.Text;

namespace WebApiStockPriceFromURL.Models
{
    public class GetStockData
    {

        private static readonly GetStockData _instance = new GetStockData();
        private static string WebURL;
        public List<StockDto> StockDtos { get; set; }
        private static string dataFromURL = string.Empty;
        private static Timer _timer;
        private static readonly object _updateStockPricesLock = new object();
        //private static string[] IndexarrayS;
        private static string[] StockarrayS;
        private static volatile bool _updatingStockPrices = false;
        private static TimeSpan _updateInterval;
        private GetStockData()
        {
            this.StockDtos = new List<StockDto>();
            WebURL = Helper.ApiUrlBasic;
            _updateInterval = TimeSpan.FromMilliseconds(Helper.DueTimeBasic);
            //set timmer

            GetStockPriceFromApi();
            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
        }
        public static GetStockData GetInstance
        {
            get
            {
                return _instance;
            }
        }


        private void GetStockPriceFromApi()
        {
            try {
                if (this.StockDtos.Count > 0 && Helper.IsWeekendUpdatePrice) // Check weekend update and Stock markets do tradding
                {
                    return;
                }
                using (WebClient client = new WebClient()) {
                    client.Encoding = Encoding.UTF8;
                    dataFromURL = client.DownloadString(WebURL);
                    StockarrayS = dataFromURL.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);//.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); // get Stock
                    this.StockDtos.Clear();

                    foreach (var item in StockarrayS) {
                        string[] itemContain = item.Split(';');
                        this.StockDtos.Add(new StockDto {
                            Id = itemContain[0],
                            Name = itemContain[4],
                            Basic = decimal.Parse(itemContain[3]),
                            Floor = decimal.Parse(itemContain[2]),
                            Ceiling = decimal.Parse(itemContain[1])
                        });
                    }
                    
                }
            }
            catch (Exception) {
                // console.log
                //throw;
            }
        }

        private void UpdateStockPrices(object state)
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