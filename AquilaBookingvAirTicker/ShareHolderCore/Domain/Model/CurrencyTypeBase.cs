using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class CurrencyTypeBase
    {
        #region Primitive Properties

        public virtual byte CurrencyTypeId
        {
            get;
            set;
        }

        public virtual string CurrencyCode
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
            set;
            get;
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
