using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShareHoderFrontEndV2.Ext;

namespace ShareHoderFrontEndV2.Models
{
    public class HotDealModel
    {
        
        public int HotelId { get; set; }
        public string Name { get; set; }
        public decimal MinPrice { get; set; }

        public HotDealModel(int hotelId, string name)
        {
            HotelId = hotelId;
            Name = name;
            MinPrice = ApplicationHelper.GetMinPriceOfHotelHotDeal(hotelId);
        }

    }


}