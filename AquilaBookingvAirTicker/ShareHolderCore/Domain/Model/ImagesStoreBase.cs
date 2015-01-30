using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class ImagesStoreBase
    {
        #region Primitive Properties

        public virtual Int64 ImagesStoreId
        {
            
            get;
            set;
        }
        
        public virtual string ImagePath
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

        public virtual IList<Hotel> ListHotel
        {
            get;
            set;
        }
        public virtual IList<HotelImage> ListHotelImage
        {
            get;
            set;
        }
        #endregion
    }
}
