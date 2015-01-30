using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface IShareHolderSummary<T>
    {
        IList<T> GetCurrentBalance(DateTime toDate, string shareHolderCode, string shareSymbol, string orderBy, string orderDirection, Int32 page, Int32 pageSize);
    }
}
