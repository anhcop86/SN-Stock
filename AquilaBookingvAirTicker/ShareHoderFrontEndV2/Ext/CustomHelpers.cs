using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain.Repositories;

namespace ShareHoderFrontEndV2.Ext
{
    public static class CustomHelpers
    {
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
        public static IRepository<Hotel> irp = new HotelRepository();
        public static string getNameHotel(int hoteid)
        {
            return irp.GetById(hoteid).Name;
        }
    }
}