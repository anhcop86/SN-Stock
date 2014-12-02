using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PorfolioInvesment.Models
{
    public class ApplicationHelper
    {
        public ApplicationHelper()
        {

        }


        public static string Username
        {
            get
            {
                return ConfigurationManager.AppSettings["username"].ToString();
            }
        }
        public static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["password"].ToString();
            }
        }

        public static string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string ParstToFormat_IN_SQL(string tickerList)
        {
            string[] tickerArray = tickerList.Split('|');
            string tickerItem = string.Empty;

            foreach (var item in tickerArray)
            {
                tickerItem = tickerItem + item + "','";
            }
            tickerItem = tickerItem.Substring(0, tickerItem.Length - 3);
            return tickerItem; // return format sample is same //"TV3','KCE"
        }
    }
}