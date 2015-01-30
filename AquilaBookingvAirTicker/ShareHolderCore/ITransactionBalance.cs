using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface ITransactinBalance<T>
    {
        IList<T> GetTransactionBalances(string shareHolderId, string shareSymbol, int shareHolderGroupId, string orderBy, string orderDirection, Int32 page, Int32 pageSize, out int totalRecord);
    }
}
