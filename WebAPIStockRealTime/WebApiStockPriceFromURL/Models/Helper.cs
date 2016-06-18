using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApiStockPriceFromURL.Models
{
    public static class Helper
    {
        public static string ApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiUrl"].ToString();
            }
        }
        public static bool CheckTimeUpdatePrice()
        {
            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 15 && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}