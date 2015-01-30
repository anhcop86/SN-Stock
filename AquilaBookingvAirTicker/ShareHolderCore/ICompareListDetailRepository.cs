using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface ICompareListDetailRepository<T>
    {
        IList<T> GetListCompareListDetailFromMember(int idMember);
        //void GetById
    }
}
