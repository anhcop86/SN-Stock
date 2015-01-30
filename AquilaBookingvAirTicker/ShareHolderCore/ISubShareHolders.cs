using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface ISubShareHolders <T>
    {
        IList<T> GetSubShareHolders(Int32 shareHolderId);
        IList<T> GetSubShareHolders(Int32 shareHolderId, string addSelectAllOptionValue, string selectAllOptionText);
    }
}
