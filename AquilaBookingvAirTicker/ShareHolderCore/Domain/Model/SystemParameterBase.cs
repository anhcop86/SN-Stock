using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class SystemParameterBase
    {
        #region Primitive Properties

        public virtual System.Guid ParameterId
        {
            get;
            set;
        }

        public virtual string ParameterName
        {
            get;
            set;
        }

        public virtual string ParameterValue
        {
            get;
            set;
        }

        public virtual string ParameterType
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
