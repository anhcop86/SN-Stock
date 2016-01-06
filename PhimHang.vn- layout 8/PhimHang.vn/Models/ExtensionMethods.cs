using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Loai bo cac ky tu dac biet trong chuoi
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveSpecialString(this string value)
        {
            return string.Join("", value.Split('$', '!', '@', '#', '%', '^', '&', '*', '(', ')', '-', '+', '[', ']', '?', '<', '>', ':', ';', '|', '/')).ToUpper(); 
        }
    }
}