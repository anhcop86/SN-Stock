using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class FavouritDestinationBase
    {
        #region Primitive Properties

        public virtual Int16 IdFD
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

    
        #endregion
    }
}
