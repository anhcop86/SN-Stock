using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Repositories;

namespace ShareHolderCore.Domain.Model
{
    public class HotelExtent: Hotel
    {
        public string ImagePath { get; set; }
        public decimal? price { get; set; }
        public int review { get; set; }
        public int CountComment { get; set; }
        public HotelExtent()
        {
            decimal? price = Decimal.Zero;
            IAvailabilityRepository<Availability> irp = new AvailabilityRepository();
            //price = irp.getPrice(HotelId);
            price = irp.getPrice(HotelId);
        }

        public HotelExtent(int roomType)
        {
            decimal? price = Decimal.Zero;
            IAvailabilityRepository<Availability> irp = new AvailabilityRepository();

            //price = irp.getPrice(HotelId);
            price = irp.getPrice(HotelId, roomType);
        }
    }
 

}
