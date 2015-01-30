using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface ITransactionDetailHistory<T>
    {
        IList<T> Search(DateTime formDate, DateTime toDate,Int32 shareHolderID, string symbol, int trancategory, int page, int pageSize, out Int32 rowCount, string orderBy, string orderDirection);
    }
}
