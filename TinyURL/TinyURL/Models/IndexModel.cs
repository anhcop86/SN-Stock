using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyURL.Data;

namespace TinyURL.Models
{
    public class IndexModel
    {
        /// <summary>
        /// url string
        /// </summary>
        //public string URL { get; set; }

        public static TinyURL.Entity.URLTiny GetData(long URL)
        {
            using (var dac = new TinyURLDAO()) {
                dac.Id = URL;

                return dac.GetURLTinyAsync();
            }
        }
    }
}