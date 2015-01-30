using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class SystemVersionBase
    {
        #region Primitive Properties

        public virtual int SysVersionId
        {
            get;
            set;
        }
        

        public virtual string SysVersion
        {
            get;
            set;
        }

        public virtual string VersionName
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> UpdateDate
        {
            get;
            set;
        }

        public virtual string VersionDescription
        {
            get;
            set;
        }

        #endregion
    }
}
