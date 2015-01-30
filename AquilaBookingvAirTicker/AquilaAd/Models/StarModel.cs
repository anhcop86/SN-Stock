using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Models
{
    public class StarModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StarModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public StarModel()
        {
         
        }

        public List<StarModel> listAllStar()
        {
            List<StarModel> ListStar = new List<StarModel>();
            ListStar.Add(new StarModel(1,"Một"));
            ListStar.Add(new StarModel(2,"Hai"));
            ListStar.Add(new StarModel(3,"Ba"));
            ListStar.Add(new StarModel(4,"Bốn"));
            ListStar.Add(new StarModel(5,"Năm"));

            return ListStar;
        }
    }
}