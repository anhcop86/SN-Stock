using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace ShareHolderCore
{
    public interface IBookingDetailRepository<T>
    {
        IList<SearchBooking> searchBooking(int hotelId, byte roomtypeId, int fromndate, int todate);
        IList<T> getListByBookingid(long bookingid);
        IList<T> getListFilterByHotelOwner(int idOwner);

        IList<T> getBookingByViewCode(string viewcode);
    }
}
