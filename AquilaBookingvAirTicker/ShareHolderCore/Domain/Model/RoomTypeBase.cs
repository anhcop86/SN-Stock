using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class RoomTypeBase
    {
        #region Primitive Properties

        public virtual byte RoomTypeId
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string EnglishName
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

        public virtual IList<AvailabilityHist> ListAvailabilityHist
        {
            get;
            set;
        }

        public virtual IList<Availability> ListAvailability
        {
            get;
            set;
        }

        public virtual IList<BookingDetail> ListBookingDetail
        {
            get;
            set;
        }
        #endregion
    }
}
