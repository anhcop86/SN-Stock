using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
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
        /// <summary>
        /// Due time to update function / thời gian cập nhật giá / tính bằng giây
        /// </summary>
        public static double DueTime
        {
            get
            {
                return double.Parse(ConfigurationManager.AppSettings["DueTime"].ToString())*1000;
            }
        }
        //public static bool IsWeekendUpdatePrice()
        //{
        //    if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 15 && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday) {
        //        return true;
        //    }
        //    else {
        //        return false;
        //    }
        //}
        public static bool IsWeekendUpdatePrice
        {
            get
            {
                if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 15 && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    return false; // không update nếu là ngày thứ 7 & CN
                }
                else
                {
                    return true; // has updated
                }
            }
        }
        /// <summary>
        /// JSON Serialization
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }
}