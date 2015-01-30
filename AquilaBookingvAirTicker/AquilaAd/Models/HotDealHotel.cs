using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Models
{
    public class HotDealHotel
    {
          public string Id { get; set; }
        public string Name { get; set; }

        public HotDealHotel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public HotDealHotel()
        {
         
        }

        public List<HotDealHotel> listAllHotDealHotel()
        {
            List<HotDealHotel> list = new List<HotDealHotel>();
            list.Add(new HotDealHotel("N", "NO"));
            list.Add(new HotDealHotel("Y", "YES"));


            return list;
        }
    }
}