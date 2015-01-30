using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquilaAd.Models;
using ShareHolderCore.Domain.Model;
using System.Security.Cryptography;
using System.Text;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;

namespace AquilaAd.Ext
{
    public static class CustomHelpers
    {
        #region main methol
        
        public static MvcHtmlString ImageActionLink(this HtmlHelper html, string imageSource, string url, string width, string height)
        {
            string imageActionLink = string.Format("<a href=\"{0},\"><img width=\"{1}\" height=\"{2}\" src=\"{3}\" /></a>", url, width, height, imageSource);

            return new MvcHtmlString(imageActionLink);
        }
        #endregion

        public static HotelModel MapHotelModel_Hotel(Hotel ht)
        {
            HotelModel hm = new HotelModel();
            if (ht != null)
            {
                hm.CreatedBy = ht.CreatedBy;
                hm.CreatedDate = ht.CreatedDate;
                hm.HotelAddress = ht.HotelAddress;
                hm.HotelId = ht.HotelId;
                hm.Location = ht.Location;
                hm.LongDesc = ht.LongDesc;
                hm.MostView = ht.MostView;
                hm.Name = ht.Name;
                hm.ProvinceId = ht.ProvinceId;
                hm.ShortDesc = ht.ShortDesc;
                hm.Star = ht.Star;
                hm.BookingCondition = ht.BookingCondition;
                hm.Display = ht.Display;
            }

            return hm;
        }

        public static string parstFormatToYYYY_MM_DD(string input) // show interface with format yy-mm-dd in view, this format for input with type = date
        {
            return input.Substring(0, 4) + "-" + input.Substring(4, 2) + "-" + input.Substring(6, 2);
        }
        public static string parstFormatTo_DD_MM_YYYY(string input) // show interface on website dd/mm/yyyy
        {
            //return input.Substring(0, 4) + "-" + input.Substring(4, 2) + "-" + input.Substring(6, 2);
            return input.Substring(6, 2) + "/" + input.Substring(4, 2) + "/" + input.Substring(0, 4);
        }

        public static string parstFormatToYYYYMMDD(string input) // save on database with format by yyyyMMdd
        {
            //input = input.Replace("/", "");
            return input.Substring(6, 4) + input.Substring(3, 2) + input.Substring(0, 2);

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

        public static IRepository<Hotel> irp = new HotelRepository();
        public static string getNameHotel(int hoteid)
        {
            return irp.GetById(hoteid).Name;
        }

        public static DateTime parstFormatToDate(string input) // save on database with format by yyyyMMdd
        {
            //input = input.Replace("/", "");

            return new DateTime( int.Parse(input.Substring(0, 4)), int.Parse(input.Substring(4, 2)), int.Parse(input.Substring(6, 2)));

        }

    }
}