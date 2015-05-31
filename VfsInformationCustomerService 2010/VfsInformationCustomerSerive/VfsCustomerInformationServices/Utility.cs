using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using log4net;

namespace VfsCustomerInformationServices
{
    public static class Ultility
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static DateTime ConvertStringToDate(string dateString)
        {
            DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
            datefomatProvider.DateSeparator = "/";
            datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
            datefomatProvider.LongDatePattern = "dd/MM/yyyy";
            return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)));

            //return DateTime.Parse(dateString);
            //return Convert.ToDateTime(dateString, datefomatProvider);
        }

        public static void Info(object message)
        {
            if (log.IsErrorEnabled)
            {
                log.Info(message);
            }
        }

        public static void Info(object message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Info(message, ex);
            }
        }

        public static void Error(object message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public static void Error(object message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }

        public static string CutAddressHead(string source)
        {
            string s = source;
            if (s.Length <= 160) return s;
            string temp = s.Substring(0, 160);
            int pos = temp.LastIndexOf(" ");
            s = temp.Substring(0, pos);
            return s;
        }

        public static string CutAddressEnd(string source)
        {
            string s = source;
            if (s.Length <= 160) return string.Empty;
            string temp = s.Substring(0, 160);
            int pos = temp.LastIndexOf(" ");
            s = s.Substring(pos + 1, ((s.Length - 1) - pos));
            return s;
        }
    }
}
