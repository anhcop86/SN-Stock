using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class CompareListBase
    {
        #region Primitive Properties

        public virtual long CompareListId
        {
            get;
            set;
        }

         

        public virtual string CreatedDate
        {
            get;
            set;
        }

        public virtual string CreatedBy
        {
            get;
            set;
        }

        public virtual IList<CompareListDetail> ListCompareListDetail
        {
            get;
            set;
        }

        public virtual Membership Membership
        {
            get;
            set;
        }
        #endregion
    }
}
