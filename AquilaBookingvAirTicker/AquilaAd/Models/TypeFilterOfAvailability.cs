using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Models
{
    public class TypeFilterOfAvailability
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public TypeFilterOfAvailability(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public TypeFilterOfAvailability()
        {
         
        }

        public static List<TypeFilterOfAvailability> listAllTypeFilterOfAvailability()
        {
            List<TypeFilterOfAvailability> ListDisplay = new List<TypeFilterOfAvailability>();
            ListDisplay.Add(new TypeFilterOfAvailability("1", "Tên khách sạn"));
            ListDisplay.Add(new TypeFilterOfAvailability("2", "Loại phòng"));
            ListDisplay.Add(new TypeFilterOfAvailability("3", "Giá phòng"));
            ListDisplay.Add(new TypeFilterOfAvailability("4", "Ngày đặt phòng"));            

            return ListDisplay;
        }
    }
}