using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace StockPriceSynEverydate
{
    public static class ApplicationHelper
    {
        public static int TimeStart
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["TimeStart"].ToString());
            }
        }

        public static int IntervalMinutes
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["IntervalMinutes"].ToString());
            }
        }
    }
}
