using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    public class HotelIdAndRoomtypeId
    {
        public int HotelId { get; set; }
        public byte RoomTypeId { get; set; }
        public int? Quantity { get; set; }

        public decimal Price { get; set; }

        public Int64 AvailabilityId { get; set; }

    }
}
