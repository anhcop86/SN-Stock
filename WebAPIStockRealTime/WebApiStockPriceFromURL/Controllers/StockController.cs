using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStockPriceFromURL.Models;

namespace WebApiStockPriceFromURL.Controllers
{
    public class StockController : ApiController
    {
        private static GetStockData _marketModel = GetStockData.GetInstance;
        

        /// <summary>
        /// Get All stock
        /// </summary>
        /// <returns></returns>
        [Route("api/stock")]
        [HttpGet]
        public List<StockDto> GetAll()
        {
            return _marketModel.StockDtos;
        }

        ///// <summary>
        ///// Get index list
        ///// /api/market/getindex
        ///// </summary>
        ///// <returns></returns>
        //[ActionName("api/stock/getindex")]
        //public IEnumerable<StockDto> GetIndex()
        //{
        //    return _marketModel.StockDtos.Where(m => m.Type == "I");
        //}

        /// <summary>
        /// Get specify stocks
        /// api/market/getstock/AAA|HAG
        /// </summary>
        /// <param name="stockName"></param>
        /// <returns></returns>
        [Route("api/stock/filter/{stockNames:length(1,200)}")]
        [HttpGet]
        public IEnumerable<StockDto> GetDetail(string stockNames)
        {
            string[] filters = stockNames.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            return _marketModel.StockDtos.Where(m => filters.Contains(m.Id));
        }
    }
}
