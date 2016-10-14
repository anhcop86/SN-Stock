using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStockPriceFromURL.Models;

namespace WebApiStockPriceFromURL.Controllers
{
    //[RoutePrefix("api/market")]
    /// <summary>
    /// Market stock nè
    /// </summary>
    public class MarketController : ApiController
    {
        // GET api/market
        private static GetMarketData _marketModel = GetMarketData.GetInstance;

        /// <summary>
        /// Get All stock
        /// </summary>
        /// <returns></returns>
        [Route("api/market")]
        [HttpGet]
        public List<MarketDto> GetAll()
        {
            return _marketModel.StockRealTimes;
        }


        /// <summary>
        /// Get index list
        /// /api/market/getindex
        /// </summary>
        /// <returns></returns>
        [ActionName("api/market/getindex")]
        [HttpGet]
        public IEnumerable<MarketDto> GetIndex()
        {
            return _marketModel.StockRealTimes.Where(m => m.Type == "I");
        }

        /// <summary>
        /// Get specify stocks
        /// api/market/getstock/AAA|HAG
        /// </summary>
        /// <param name="stockName"></param>
        /// <returns></returns>
        [Route("api/market/filter/{stockNames:length(1,200)}")]
        [HttpGet]
        public IEnumerable<MarketDto> GetDetail(string stockNames)
        {
            string[] filters = stockNames.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            return _marketModel.StockRealTimes.Where(m => filters.Contains(m.CompanyID));
        }

    }
}
