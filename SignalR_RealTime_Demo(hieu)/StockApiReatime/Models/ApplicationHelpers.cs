using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StockApiReatime.Models
{
    public class ApplicationHelpers
    {
        public static string URLApi
        {
            get
            {
                return ConfigurationManager.AppSettings["URLApi"].ToString();
            }
        }
    }
}