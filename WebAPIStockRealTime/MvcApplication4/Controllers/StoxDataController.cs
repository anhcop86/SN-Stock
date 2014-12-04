using PorfolioInvesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PorfolioInvesment.Controllers
{
    public class StoxDataController : ApiController
    {
        StoxDataEntities db = new StoxDataEntities(); 
        // GET api/stoxdata
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/stoxdata/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/stoxdata
        [ActionName("GetCompany")]
        public dynamic Post(ParaStock paraStock)
        {
            if (paraStock != null && paraStock.PI_tickerList == "KEYSECRET")
            {
                using (db = new StoxDataEntities())
                {
                    var companydata = from c in db.stox_tb_Company
                                      select new
                                      {
                                          Code = c.Ticker,
                                          ShortName = c.Name,
                                          LongName = c.ShortName
                                      };
                    return companydata.ToList();
                }

            }
            else
            {
                return null;
            }
        }
        // POST api/StoxData/create
        [ActionName("getHistory")]
        public dynamic PostHistory(ParaStock paraStock)
        {
            if (paraStock != null && paraStock.PI_tickerList == "KEYSECRET")
            {
                using (db = new StoxDataEntities())
                {
                    var history = from h in db.stox_tb_HOSE_Trading
                                  where h.DateReport >= new DateTime(2014, 11, 02) && h.DateReport <= new DateTime(2014, 12, 02)
                                  select new StockHis
                                  {
                                      CeilingPrice = h.Ceiling,
                                      ClosePrice = h.Last,
                                      Code = h.StockSymbol,
                                      DiffPrice = h.PriorClosePrice,
                                      FloorPrice = h.Floor,
                                      HighPrice = h.Highest,
                                      LowPrice = h.Lowest,
                                      OpenPrice = h.OpenPrice,
                                      TradingDate = h.DateReport
                                  };
                    return history.ToList();
                }
            }
            else
            {
                return null;
            }
        }

        // PUT api/stoxdata/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/stoxdata/5
        public void Delete(int id)
        {
        }
    }
}
