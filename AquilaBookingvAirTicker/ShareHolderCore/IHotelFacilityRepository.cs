using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface IHotelFacilityRepository
    {
        int CountFacility(int hotelId);
        void InsertFacility(int hotelId, short facilityId);
        void DeleteFacility(int hotelId, short facilityId);
    }
}
