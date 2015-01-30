using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace ShareHolderCore
{
    public interface IBookingRepository
    {
        Booking getBookingFromViewCode(string viewCode);
        Booking getBookingId(long bookingId);
        IList<Booking> getBookingFromOwnerId(int ownerId);
        IList<Booking> getBookingwithPaging(int pageNumber, int pageSize);
        int countAllBooking();
        IList<Booking> getBookingFromMemberId(int memberid);
    }
}
