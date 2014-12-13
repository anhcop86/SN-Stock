using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PhimHang.Models
{
    public class ModelHelpers
    {
        public static async Task<List<StockRealTime>> getPriceFromAPI()
        {

            // Returns data from a web service.
            //http://www.vfs.com.vn:6789/api/stocks
            //{PI_tickerList:'KLS|OGC|KBC'}
            //var client = new RestClient("http://www.vfs.com.vn:6789/api/");
            Uri uri = new Uri("http://vfs.com.vn:6789/");
            var list = new List<StockRealTime>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameter = new { PI_tickerList = "KEYSECRET" };
                var response = client.PostAsJsonAsync("api/StockRealTime", parameter).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadAsAsync<List<StockRealTime>>();                    
                    return list;
                }
                else
                {
                    return list;
                }
            }

        }

        
    }
}