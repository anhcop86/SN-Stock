using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Xml;
using System.Globalization;
using System.Text;
using System.Net.Mail;
using System.Reflection;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Net.Sockets;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain.Repositories;

namespace AquilaAd
{
    public static class Utils
    {
        #region Constants and Fields

      

        #endregion

        #region Properties

     
    
        #endregion

        #region Methol
        public static string TinyAddress(string address)
        {
            string result = string.Empty;
            if (address.Length > 60)
            {
                result = address.Substring(0, 60) + "...";
            }
            else
            {
                result = address;
            }

            return result;
        }

        public static int CountFacilityOfHotel(int hotelId)
        {
            IHotelFacilityRepository fc = new HotelFacilityRepository();

            return fc.CountFacility(hotelId);
        }

        public static int ToInt32(this string content)
        {
            return int.Parse(content);
        }

        #endregion

    }

    
}