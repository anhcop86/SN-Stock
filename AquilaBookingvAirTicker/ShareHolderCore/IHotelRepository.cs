using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace ShareHolderCore
{
    public interface IHotelRepository<T>
    {
        IList<T> getHolteMostview();
        IList<T> searchHolte(string searchValue);
        IList<T> getHotDealHotel();
        IList<T> getHotDealHotel(string searchValue);
        LocationTag getHotelLocationTag(string searchValue);
        T gethotelbyHotelOwer(int hotelowerid);
        IList<T> getHotelFilterByOwner(int OwnerId);
        IList<T> getHotelFilterByOwnerId(int OwnerId);
        IList<T> getListHotelwithPaging(int pageNumber, int pageSize);
        int countAllHotel();
    }
}

