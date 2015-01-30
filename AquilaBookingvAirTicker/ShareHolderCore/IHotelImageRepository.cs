using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace ShareHolderCore
{
    public interface IHotelImageRepository<T>
    {
        HotelImage HotelImageofMain(int hotelId);
        IList<HotelImage> HotelImageofAllMain(int hotelId);
        IList<HotelImage> ImageOfHotelRoom(int hotelId);
        IList<HotelImage> ImageOfHotelRoom(int hotelId, byte roomtypeId);
        IList<HotelImage> GetAllImageOfHotel(int hotelId);
        HotelImage getHotelImageByImagesStoreId(long imagesStoreId);        
    }
}
