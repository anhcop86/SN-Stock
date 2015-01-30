using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class AvailabilityHistBase
    {
        #region Primitive Properties

        public virtual long AvailabilityHistId
        {
            get;
            set;
        }

        public virtual Nullable<int> AvailabilityId
        {
            get;
            set;
        }

        public virtual int HotelId
        {
            get;
            set;

        }
        

        public virtual byte RoomTypeId
        {
            get;
            set;

        }
        

        public virtual string FromDate
        {
            get;
            set;
        }

        public virtual string ToDate
        {
            get;
            set;
        }

        public virtual Nullable<int> Quantity
        {
            get;
            set;
        }
        public virtual string IsHotDeal
        {
            get;
            set;
        }
        public virtual Nullable<byte> CurrencyTypeId
        {
            get;
            set;
            
        }
        

        public virtual Nullable<decimal> Price
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

        public virtual RoomType RoomType
        {
            get;
            set;
        }
        public virtual CurrencyType CurrencyType
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
