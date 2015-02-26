using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StockPriceSynEverydate
{
    class Program
    {
        static void Main(string[] args)
        {
            // get max date to syn

            while (true)
            {
                try
                {
                    if (DateTime.Now.Hour == 7)
                    {

                        var maxdate = new DateTime?();
                        using (StockChart_HieuEntities dbtarget = new StockChart_HieuEntities())
                        {
                            maxdate = (from mindate in dbtarget.StockPrices select mindate.TradingDate).Max();
                        }
                        if (maxdate != new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1))
                        {
                            SyncPrice(new DateTime(((DateTime)maxdate).Year, ((DateTime)maxdate).Month, ((DateTime)maxdate).Day + 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1));
                            Console.WriteLine("updated {0}", DateTime.Now.ToString());
                        }
                        else
                        {
                            Console.WriteLine("no update {0}", DateTime.Now.ToString());
                        }
                        Thread.Sleep(30 * 1000 * 60);
                    }
                    else
                    {
                        Console.WriteLine("start khong dung gio {0}", DateTime.Now.ToString());
                        Thread.Sleep(30 * 1000 * 60);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("no update {0}", ex);
                }
            }

        }

        static void SyncPrice(DateTime fromdate, DateTime todate)
        {
            #region syndata hose
            
            using (StoxDataEntities dbstox = new StoxDataEntities())
            {       
                    var historyHOSE = (from h in dbstox.stox_tb_HOSE_Trading
                                       where h.DateReport >= fromdate && h.DateReport <= todate                                       
                                       select new
                                       {
                                           CeilingPrice = h.Ceiling * 10,
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
                    using (StockChart_HieuEntities dbtarget = new StockChart_HieuEntities())
                    {
                        foreach (var item in historyHOSE)
                        {
                            StockPrice sp = new StockPrice
                            {
                                CeilingPrice = item.CeilingPrice,
                                ClosePrice = item.ClosePrice,
                                Code = item.Code,
                                DiffPrice = item.DiffPrice,
                                FloorPrice = item.FloorPrice,
                                HighPrice = item.HighPrice == 0 ? item.ClosePrice : item.HighPrice,
                                LowPrice = item.LowPrice == 0 ? item.ClosePrice : item.LowPrice,
                                OpenPrice = item.OpenPrice == 0 ? item.ClosePrice : item.OpenPrice,
                                Totalshare = item.Totalshare,
                                TradingDate = item.TradingDate
                            };
                            dbtarget.StockPrices.Add(sp);

                        }
                        dbtarget.SaveChanges();
                    }
                
            }
            
            #endregion

            #region syndata hnx from 2009
            
            using (StoxDataEntities dbstox = new StoxDataEntities())
            {               
                    
                    var historyHNX = (from h in dbstox.stox_tb_StocksInfo
                                      where h.trading_date >= fromdate && h.trading_date <= todate                                    
                                      select new
                                      {
                                          CeilingPrice = h.ceiling_price,
                                          ClosePrice = h.close_price,
                                          Code = h.code,
                                          DiffPrice = h.basic_price,
                                          FloorPrice = h.floor_price,
                                          HighPrice = h.highest_price,
                                          LowPrice = h.lowest_price,
                                          OpenPrice = h.open_price,
                                          TradingDate = h.trading_date,
                                          Totalshare = h.nm_total_traded_qtty
                                      }).ToList();

                    using (StockChart_HieuEntities dbtarget = new StockChart_HieuEntities())
                    {
                        foreach (var item in historyHNX)
                        {
                            StockPrice sp = new StockPrice
                            {
                                CeilingPrice = item.CeilingPrice,
                                Code = item.Code,
                                DiffPrice = item.DiffPrice,
                                FloorPrice = item.FloorPrice,
                                ClosePrice = item.ClosePrice,
                                HighPrice = item.HighPrice == 0 ? item.ClosePrice : item.HighPrice,
                                LowPrice = item.LowPrice == 0 ? item.ClosePrice : item.LowPrice,
                                OpenPrice = item.OpenPrice == 0 ? item.ClosePrice : item.OpenPrice,
                                Totalshare = (double)item.Totalshare,
                                TradingDate = item.TradingDate
                            };
                            dbtarget.StockPrices.Add(sp);

                        }
                        dbtarget.SaveChanges();
                    
                }
            } 
            #endregion

            #region syndata index-hnx from 2006
            
            using (StoxDataEntities dbstox = new StoxDataEntities())
            {
                    var historyHNXIndex = (from h in dbstox.Stox_tb_MarketInfo
                                           where h.TRADING_DATE >= fromdate && h.TRADING_DATE <= todate
                                           select new
                                           {
                                               CeilingPrice = 0,
                                               ClosePrice = h.MARKET_INDEX,
                                               Code = "HNXIndex",
                                               DiffPrice = h.PRIOR_MARKET_INDEX,
                                               FloorPrice = 0,
                                               HighPrice = h.HIGHTEST,
                                               LowPrice = h.LOWEST,
                                               OpenPrice = h.OPEN_INDEX,
                                               TradingDate = h.TRADING_DATE,
                                               Totalshare = h.TOTAL_QTTY
                                           }).ToList();
                    using (StockChart_HieuEntities dbtarget = new StockChart_HieuEntities())
                    {
                        foreach (var item in historyHNXIndex)
                        {
                            StockPrice sp = new StockPrice
                            {
                                CeilingPrice = item.CeilingPrice,
                                Code = item.Code,
                                DiffPrice = item.DiffPrice,
                                FloorPrice = item.FloorPrice,
                                ClosePrice = item.ClosePrice,
                                HighPrice = item.HighPrice == 0 ? item.ClosePrice : item.HighPrice,
                                LowPrice = item.LowPrice == 0 ? item.ClosePrice : item.LowPrice,
                                OpenPrice = item.OpenPrice == 0 ? item.ClosePrice : item.OpenPrice,
                                Totalshare = (double)item.Totalshare,
                                TradingDate = item.TradingDate
                            };
                            dbtarget.StockPrices.Add(sp);

                        }
                        dbtarget.SaveChanges();
                    }                
            } 
            #endregion

            #region syndata index-hsx from 2006

            using (StoxDataEntities dbstox = new StoxDataEntities())
            {
                var historyVNIndex = (from h in dbstox.stox_tb_HOSE_TotalTrading
                                      where h.DateReport >= fromdate && h.DateReport <= todate
                                      select new
                                      {
                                          CeilingPrice = 0,
                                          ClosePrice = h.VNIndex,
                                          Code = "VNIndex",
                                          DiffPrice = h.PreVNIndex,
                                          FloorPrice = 0,
                                          HighPrice = h.Hightest,
                                          LowPrice = h.Lowest,
                                          OpenPrice = h.OpenIndex,
                                          TradingDate = h.DateReport,
                                          Totalshare = h.TotalShares
                                      }).ToList();

                using (StockChart_HieuEntities dbtarget = new StockChart_HieuEntities())
                {
                    foreach (var item in historyVNIndex)
                    {
                        StockPrice sp = new StockPrice
                        {
                            CeilingPrice = item.CeilingPrice,
                            Code = item.Code,
                            DiffPrice = item.DiffPrice,
                            FloorPrice = item.FloorPrice,
                            ClosePrice = item.ClosePrice / 100,
                            HighPrice = item.HighPrice == 0 ? item.ClosePrice : item.HighPrice,
                            LowPrice = item.LowPrice == 0 ? item.ClosePrice : item.LowPrice,
                            OpenPrice = item.OpenPrice == 0 ? item.ClosePrice : item.OpenPrice,
                            Totalshare = (double)item.Totalshare,
                            TradingDate = item.TradingDate
                        };
                        dbtarget.StockPrices.Add(sp);

                    }
                    dbtarget.SaveChanges();
                }
            }
            #endregion
        }
    }
}
