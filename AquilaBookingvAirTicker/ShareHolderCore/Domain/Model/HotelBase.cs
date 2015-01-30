using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class HotelBase
    {
        #region Primitive Properties

        public virtual int HotelId
        {
            get;
            set;
        }

        
        public virtual string Name
        {
            get;
            set;
        }

        public virtual Nullable<int> Star
        {
            get;
            set;
        }

        public virtual string HotelAddress
        {
            get;
            set;
        }

        public virtual string ShortDesc
        {
            get;
            set;
        }
        public virtual string HotelCode
        {
            get;
            set;
        }

        public virtual string LongDesc
        {
            get;
            set;
        }

        public virtual string Location
        {
            get;
            set;
        }

        public virtual string BookingCondition
        {
            get;
            set;
        }

        public virtual string Display
        {
            get;
            set;
        }

        public virtual Nullable<int> ProvinceId
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
        public virtual string MostView
        {
            get;
            set;
        }

        public virtual Province Province
        {
            get;
            set;
        }



        public virtual IList<HotelFacility> ListHotelFacility
        {
            get;
            set;
        }

        public virtual IList<HotelImage> ListHotelImage
        {
            get;
            set;
        }

        public virtual IList<Availability> ListAvailability
        {
            get;
            set;
        }

        public virtual IList<AvailabilityHist> ListAvailabilityHist
        {
            get;
            set;

        }

        public virtual IList<CompareListDetail> ListCompareListDetail
        {
            get;
            set;
        }

        public virtual int? HotelOwnerId
        {
            get;
            set;
        }

        #endregion
    }
}
