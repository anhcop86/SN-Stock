using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface IShareHolderHoldSymbol <T>
    {
        IList<T> GetShareHolderHoldSymbol(string userId);
        T GetShareHolderHoldSymbolByShareSymbol(string userId, string shareSymbol);
    }
}
