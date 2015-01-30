using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface IHotelCommentRepository<T>
    {
        IList<T> getListHotelCommentFromHotelId(int hotelId);
        void deleteCommnet(long commentid);
        int countComment(int hotelId);
    }
}
