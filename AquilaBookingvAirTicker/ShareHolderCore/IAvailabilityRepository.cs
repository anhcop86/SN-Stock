using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace ShareHolderCore
{
    public interface IAvailabilityRepository<T>
    {
        decimal getPrice(int idhotel);
        decimal getPrice(int idhotel, int roomType);
        IList<MinHotelPrice> getListPrice(int idhotel);
        IList<T> getAvailabilityfromHotelid(int idhotel);
        T getAvailabilityById(Int64 idAvailability);
        IList<HotelIdAndRoomtypeId> searchHotel(string name, int fromndate, int todate);

        IList<HotelIdAndRoomtypeId> searchHotel(int hotelid, int fromndate, int todate);

        IList<T> getAvailabilityFilterFromHotelOwner(int ownerid);
        IList<T> getAvailabilitywithPaging(int pageNumber, int pageSize);
        int countAllAvailability();

        decimal GetMinPriceOfHotelHotDeal(int hotelId);
        decimal GetMaxPriceOfHotelHotDeal(int hotelId);
    }
}
