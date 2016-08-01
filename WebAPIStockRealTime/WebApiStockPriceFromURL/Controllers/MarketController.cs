using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApiStockPriceFromURL.Models;

namespace WebApiStockPriceFromURL.Controllers
{
    public class MarketController : ApiController
    {
        // GET api/market
        private static MarketModel _marketModel = MarketModel.GetInstance;
        public List<StockResult> Get()
        {
            var stockPrice = _marketModel.StockRealTimes;
            return stockPrice;
        }

        // GET api/market/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/market
        public void Post([FromBody]string value)
        {
        }

        // PUT api/market/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/market/5
        public void Delete(int id)
        {
        }
    }
}
