using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyURL.Models
{
    public class AppHelper
    {
        public static bool CheckDayOfWeekend()
        {
            if (DateTime.Now.DayOfWeek  == DayOfWeek.Saturday)
            {
                return true;    
            }
            else
            {
                return false;
            }
            
        }
    }
}