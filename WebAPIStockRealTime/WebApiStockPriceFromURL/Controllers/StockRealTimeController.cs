using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStockPriceFromURL.Models;

namespace WebApiStockPriceFromURL.Controllers
{
    public class StockRealTimeController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<string> Get ()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public string Get (int id)
        {
            return "value";
        }
        ///
        ///Return all stockRealTime
        // POST api/stockRealTime
        public List<StockResult> Post (ParaStock paraStock)
        {
            return StockMarketPrice.PriceCache;
        }


        // PUT api/<controller>/5
        public void Put (int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete (int id)
        {
        }
    }
}