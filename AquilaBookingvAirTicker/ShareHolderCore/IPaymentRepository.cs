using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface IPaymentRepository<T>
    {
        T GetById(long id);
        T GetByBookingCode(string bookingCodeid);
        int countAllPayment();
        IList<T> getPaymentwithPaging(int pageNumber, int pageSize);
        IList<T> getPaymentFilterFromHotelOwner(int ownerid);
    }
}
