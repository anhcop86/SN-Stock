using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface ITradingTransactionRepository<T> 
    {
        IList<T> Search(Int32 shareHolderID, DateTime formDate, DateTime toDate, string symbol, int trancategory, int page, int pageSize, out Int32 rowCount, string orderBy, string orderDirection);

        IList<T> SearchFromStore(Int32 shareHolderID, DateTime formDate, DateTime toDate, string symbol, int trancategory, int page, int pageSize, out Int32 rowCount, string orderBy, string orderDirection);
    }
}
