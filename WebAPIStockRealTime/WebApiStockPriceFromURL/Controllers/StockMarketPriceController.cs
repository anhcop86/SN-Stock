using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using WebApiStockPriceFromURL.Models;
using JsonDefine = Newtonsoft.Json.JsonConvert;

namespace WebApiStockPriceFromURL.Controllers
{
    public class StockMarketPriceController : ApiController
    {
        // GET api/stockmarketprice
        //public List<StockResult> Get()
        //{
        //    var stockList = StockMarketPrice.PriceCache;
        //    //return StockMarketPrice.Instance().StockRealTimes;
        //    return stockList;

        //}

        // GET api/stockmarketprice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/stockmarketprice
        public List<StockResult> Post()
        {
            try 
            {
                return StockMarketPrice.PriceCache;
            }
            catch (Exception)
            {
                return new List<StockResult>();
            }


            //return StockMarketPrice.Instance().StockRealTimes;
            //return stockList;
        }

        // PUT api/stockmarketprice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/stockmarketprice/5
        public void Delete(int id)
        {
        }
    }
}
