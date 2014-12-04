using Business;
using Entities;
using PorfolioInvesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PorfolioInvesment.Controllers
{
    public class StockRealTimeController : ApiController
    {
        // GET api/stockrealtime
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/stockrealtime/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/stockrealtime
        public List<StockRealTime> Post(ParaStock paraStock)
        {
            if (paraStock != null && paraStock.PI_tickerList == "KEYSECRET")
            {
                return StockRealTimeServices.GetGetALLTwoMarket();
            }
            else
            {
                return null;
            }
        }

        // PUT api/stockrealtime/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/stockrealtime/5
        public void Delete(int id)
        {
        }
    }
}
