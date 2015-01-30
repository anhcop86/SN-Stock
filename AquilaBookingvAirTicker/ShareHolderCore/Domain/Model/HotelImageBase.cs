using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]

    public class HotelImageBase
    {
        #region Primitive Properties

        public virtual int HotelImageID
        {
            get;
            set;
        }

        //public virtual Nullable<Int64> ImagesStoreId
        //{
        //    get;
        //    set;
        //}


        //public virtual Nullable<int> HotelId
        //{
            
        //    get;
        //    set;
        //}
            

        public virtual Nullable<byte> RoomTypeId
        {
            get;
            set;
        }

        public virtual decimal SortOrder
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

        public virtual ImagesStore ImagesStore
        {
            get;
            set;
        }
        public virtual Hotel Hotel
        {
            get;
            set;
        }

        #endregion
    }
}
