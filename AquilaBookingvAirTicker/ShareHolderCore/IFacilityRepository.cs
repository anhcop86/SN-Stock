using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace ShareHolderCore
{
    public interface IFacilityRepository
    {
        string CheckFacility(int hotelId, int facilityid);
        Facility GetById(Int16 id);
    }
}
