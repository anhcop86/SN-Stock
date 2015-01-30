using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Models
{
    public class MostViewHotel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public MostViewHotel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public MostViewHotel()
        {
         
        }

        public List<MostViewHotel> listAllMostViewHotel()
        {
            List<MostViewHotel> ListMostViewHotel = new List<MostViewHotel>();
            ListMostViewHotel.Add(new MostViewHotel("N", "NO"));
            ListMostViewHotel.Add(new MostViewHotel("Y", "YES"));
           

            return ListMostViewHotel;
        }
    }
}