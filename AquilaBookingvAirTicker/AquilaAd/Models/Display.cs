using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Models
{
    public class Display
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Display(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Display()
        {
         
        }

        public static List<Display> listAllDisplay()
        {
            List<Display> ListDisplay = new List<Display>();           
            ListDisplay.Add(new Display("Y", "YES"));
            ListDisplay.Add(new Display("N", "NO"));

            return ListDisplay;
        }
    }
}