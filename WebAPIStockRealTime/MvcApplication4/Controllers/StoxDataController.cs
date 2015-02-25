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
        StoxDataEntities db ; 
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
                                          LongName = c.ShortName,
                                          MarketType = c.ExchangeID, // 0: HOSE, 1: HNX
                                          IndexName = c.Index
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
                    var historyHOSE = (from h in db.stox_tb_HOSE_Trading
                                       where h.DateReport >= new DateTime(2011, 01, 01) && h.DateReport <= new DateTime(2015, 01, 01)
                                       && h.StockSymbol == "HAG"
                                   select new 
                                   {
                                       CeilingPrice = h.Ceiling *10,
                                       ClosePrice = h.Last * 10,
                                       Code = h.StockSymbol,
                                       DiffPrice = h.PriorClosePrice * 10,
                                       FloorPrice = h.Floor * 10,
                                       HighPrice = h.Highest * 10,
                                       LowPrice = h.Lowest * 10,
                                       OpenPrice = h.OpenPrice * 10,
                                       TradingDate = h.DateReport,
                                       Totalshare = h.Totalshare * 10
                                   }).ToList();
                    //var historyHNX = (from h in db.stox_tb_StocksInfo
                    //                  where h.trading_date >= new DateTime(2014, 12, 01) && h.trading_date <= new DateTime(2014, 12, 02)
                    //               select new 
                    //               {
                    //                   CeilingPrice = h.ceiling_price,
                    //                   ClosePrice = h.close_price,
                    //                   Code = h.code,
                    //                   DiffPrice = h.basic_price,
                    //                   FloorPrice = h.floor_price,
                    //                   HighPrice = h.highest_price,
                    //                   LowPrice = h.lowest_price,
                    //                   OpenPrice = h.open_price,
                    //                   TradingDate = h.trading_date,
                    //                   Totalshare = h.nm_total_traded_qtty
                    //               }).ToList();

                    //var historyHNXIndex = (from h in db.Stox_tb_MarketInfo
                    //                       where h.TRADING_DATE >= new DateTime(2014, 12, 01) && h.TRADING_DATE <= new DateTime(2014, 12, 02)
                    //                  select new 
                    //                  {
                    //                      CeilingPrice = 0,
                    //                      ClosePrice = h.MARKET_INDEX,
                    //                      Code = "HNXIndex",
                    //                      DiffPrice = h.PRIOR_MARKET_INDEX,
                    //                      FloorPrice = 0,
                    //                      HighPrice = h.HIGHTEST,
                    //                      LowPrice = h.LOWEST,
                    //                      OpenPrice = h.OPEN_INDEX,
                    //                      TradingDate = h.TRADING_DATE,
                    //                      Totalshare = h.TOTAL_QTTY
                    //                  }).ToList();
                    //var historyVNIndex = (from h in db.stox_tb_HOSE_TotalTrading
                    //                      where h.DateReport >= new DateTime(2014, 12, 01) && h.DateReport <= new DateTime(2014, 12, 02)
                    //                       select new 
                    //                       {
                    //                           CeilingPrice = 0,
                    //                           ClosePrice = h.VNIndex,
                    //                           Code = "VNIndex",
                    //                           DiffPrice = h.PreVNIndex,
                    //                           FloorPrice = 0,
                    //                           HighPrice = h.Hightest,
                    //                           LowPrice = h.Lowest,
                    //                           OpenPrice = h.OpenIndex,
                    //                           TradingDate = h.DateReport,
                    //                           Totalshare = h.TotalTrade
                    //                       }).ToList();

                   // var UnionTowList = historyHOSE.Union(historyHNX).Union(historyHNXIndex).Union(historyVNIndex);
                    //var countUnion = test.Count();
                    return historyHOSE;
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
