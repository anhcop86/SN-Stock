using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface IHotelRatingRepository
    {
        decimal getAverageofRatingWithHotel(int hotelId);
    }
}
