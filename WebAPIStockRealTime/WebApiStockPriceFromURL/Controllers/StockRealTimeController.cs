using GetDataFromMetaStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
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
        public MetaStockReader MR { get; set; }
        
        //MetaStockReader MR;
        public dynamic Get()
        {
            //WebCache.Set("KeyPrice", MR, 10, true);
            if (WebCache.Get("KeyAdjusted") == null ){
                if (MR == null) {
                    MR = new MetaStockReader();
                    MR.AddPath(@"E:\datafeed");
                    //MR.ReadSymbol("BTH");
                    foreach (var item in MR.MainDir) {
                        MR.ReadSymbol(item.stockname);
                    }
                    //MR.ReadSymbol("HAG");

                }


                //var test = MR.StockCache;
                List<string> listStockFilter = new List<string> { "HAG", "KLS", "KBC", "HAG", "KLS", "KBC", "HAG", "KLS", "KBC" };
                var testBTHJson = MR.StockCache.Where(m => listStockFilter.Contains(m.Key));
                WebCache.Set("KeyAdjusted", testBTHJson, 10, false);
            }

            return WebCache.Get("KeyAdjusted");
        }
        ///
        ///Return all stockRealTime
        // POST api/stockRealTime
        public List<StockResult> Post (ParaStock paraStock)
        {
            return StockMarketPrice.PriceCache;
        }

        //public List<StockResult> GetAjsutPrice(string ssss)
        //{
            
        //}
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