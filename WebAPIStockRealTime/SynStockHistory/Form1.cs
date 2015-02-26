using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynStockHistory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private testEntities db;
        private async void button1_Click(object sender, EventArgs e)
        {

            Uri uri = new Uri("http://www.vfs.com.vn:6789/api/stoxdata/GetCompany");
            //Uri uri = new Uri("http://localhost:9999/api/stoxdata/GetCompany");
        

            ParaStock para = new ParaStock { PI_tickerList = "KEYSECRET" };
            var company = new List<StockCode>();
            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(para);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

                var respone = await client.PostAsync(uri, content);
                if (respone.IsSuccessStatusCode)
                {
                    var productJsonString = await respone.Content.ReadAsStringAsync();
                    company = JsonConvert.DeserializeObject<List<StockCode>>(productJsonString).ToList();
                }

            }

            company.Add(new StockCode { Code = "HNXIndex", LongName = "Chỉ số Index của Hà Nội", ShortName = "Chỉ số Index của Hà Nội", MarketType = 10, IndexName = "HNXIndex" });
            company.Add(new StockCode { Code = "VnIndex", LongName = "Chỉ số Index của Hồ Chí Minh", ShortName = "Chỉ số Index của Hồ Chí Minh", MarketType = 11, IndexName = "VnIndex" });

            using (db = new testEntities())
            {
                foreach (var item in company)
                {

                    bool stockExists = db.StockCodes.Any(m => m.Code == item.Code);
                    if (!stockExists)
                    {
                        db.StockCodes.Add(item);
                    }

                }

                await db.SaveChangesAsync();
            }
           
            


        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //Uri uri = new Uri("http://www.vfs.com.vn:6789/api/stoxdata/getHistory");
            Uri uri = new Uri("http://localhost:9999/api/stoxdata/getHistory");
            ParaStock para = new ParaStock { PI_tickerList = "KEYSECRET" };
            var company = new List<StockPrice>();
            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(para);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

                var respone = await client.PostAsync(uri, content);
                if (respone.IsSuccessStatusCode)
                {
                    var productJsonString = await respone.Content.ReadAsStringAsync();
                    company = JsonConvert.DeserializeObject<List<StockPrice>>(productJsonString).ToList();
                }

            }

            using (db = new testEntities())
            {
                //var AllListStockPrice = db.StockPrices;
                foreach (var item in company)
                {

                    //bool stockExists = db.StockPrices.Any(m => m.Code == item.Code && m.TradingDate == item.TradingDate);
                    //if (!stockExists)
                    //{

                    //item.StringDate = item.TradingDate.
                    db.StockPrices.Add(item);
                    //}

                }

                await db.SaveChangesAsync();
            }
           
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region syndata hose
            /*
            using (Source_StoxDataEntities dbstox = new Source_StoxDataEntities())
            {
                var listHose = (from lh in dbstox.stox_tb_Company
                                where lh.ExchangeID == 0
                                select new
                                {
                                    ticker = lh.Ticker
                                }).ToList();
                foreach (var itemticker in listHose)
                {
                    string ticker = itemticker.ticker.ToString().Trim();
                    var historyHOSE = (from h in dbstox.stox_tb_HOSE_Trading
                                       where h.DateReport >= new DateTime(2009, 01, 01) && h.DateReport <= DateTime(2015, 02, 24)
                                       && h.StockSymbol == ticker
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
                    using (Target_StockChart_Hieu_SVR_17 dbtarget = new Target_StockChart_Hieu_SVR_17())
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
                                HighPrice = item.HighPrice == 0 ? item.ClosePrice : item.HighPrice,,
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
            }
            */
            #endregion

            #region syndata hnx from 2009
            /*
            using (Source_StoxDataEntities dbstox = new Source_StoxDataEntities())
            {
                var listHose = (from lh in dbstox.stox_tb_Company
                                where lh.ExchangeID == 1
                                select new
                                {
                                    ticker = lh.Ticker
                                }).ToList();
                foreach (var itemticker in listHose)
                {
                    string ticker = itemticker.ticker.ToString().Trim();
                    var historyHNX = (from h in dbstox.stox_tb_StocksInfo
                                      where h.trading_date >= new DateTime(2009, 01, 01) && h.trading_date <= new DateTime(2015, 02, 24)
                                      && h.code == ticker
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

                    using (Target_StockChart_Hieu_SVR_17 dbtarget = new Target_StockChart_Hieu_SVR_17())
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
            } */
            #endregion

            #region syndata index-hnx from 2006
            /*
            using (Source_StoxDataEntities dbstox = new Source_StoxDataEntities())
            {
                    var historyHNXIndex = (from h in dbstox.Stox_tb_MarketInfo
                                           where h.TRADING_DATE >= new DateTime(2006, 01, 01) && h.TRADING_DATE <= new DateTime(2015, 02, 24)
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
                    using (Target_StockChart_Hieu_SVR_17 dbtarget = new Target_StockChart_Hieu_SVR_17())
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
            } */
            #endregion

            #region syndata index-hsx from 2006

            using (Source_StoxDataEntities dbstox = new Source_StoxDataEntities())
            {
                var historyVNIndex = (from h in dbstox.stox_tb_HOSE_TotalTrading
                                      where h.DateReport >= new DateTime(2006, 01, 01) && h.DateReport <= new DateTime(2015, 02, 24)
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

                using (Target_StockChart_Hieu_SVR_17 dbtarget = new Target_StockChart_Hieu_SVR_17())
                {
                    foreach (var item in historyVNIndex)
                    {
                        StockPrice sp = new StockPrice
                        {
                            CeilingPrice = item.CeilingPrice,
                            Code = item.Code,
                            DiffPrice = item.DiffPrice,
                            FloorPrice = item.FloorPrice,
                            ClosePrice = item.ClosePrice/100,
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
