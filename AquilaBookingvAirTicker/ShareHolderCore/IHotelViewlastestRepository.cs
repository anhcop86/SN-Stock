using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface  IHotelViewlastestRepository<T>
    {
        IList<T> getListHotelMostview();
    }
}
