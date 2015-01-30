using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    public class MinHotelPrice
    {
        private int _HotelId = 0;
        private byte _RoomTypeId = 0;
        private decimal _Price = 0;

        public int HotelId
        {
            get { return _HotelId; }
            set { _HotelId = value; } 
        }
        public byte RoomTypeId
        {
            get { return _RoomTypeId; }
            set { _RoomTypeId = value; } 
        }
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; } 
        }
    }
}
