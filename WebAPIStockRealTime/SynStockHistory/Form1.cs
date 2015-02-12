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
                        db.StockPrices.Add(item);
                    //}

                }

                await db.SaveChangesAsync();
            }
           
            

        }

    }
}
