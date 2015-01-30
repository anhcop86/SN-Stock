using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class LoggingBase
    {
        #region Primitive Properties

        public virtual System.Guid LoggingId
        {
            get;
            set;
        }

        public virtual System.DateTime LoggingDate
        {
            get;
            set;
        }

        public virtual int MemberId
        {
            get;
            set;
        }

        public virtual string LoggingDescription
        {
            get;
            set;
        }

        public virtual string IpAddress
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

        #endregion
    }
}
