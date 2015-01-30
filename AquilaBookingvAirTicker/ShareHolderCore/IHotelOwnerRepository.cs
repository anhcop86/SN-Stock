using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderCore.Domain.Model;

namespace ShareHolderCore
{
    public interface IHotelOwnerRepository
    {
        HotelOwner getHotelOwnerByMemberShipId(int membershipid);
    }
}
