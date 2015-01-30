using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class BookingBase
    {
        #region Primitive Properties

        public virtual long BookingId
        {
            get;
            set;
        }

        public virtual string BookingDate
        {
            get;
            set;
        }

   
        public virtual string FullName
        {
            get;
            set;
        }

        public virtual string PaymentType
        {
            get;
            set;
        }

        public virtual string Email
        {
            get;
            set;
        }

        public virtual string PhoneNumber
        {
            get;
            set;
        }

        public virtual string ArrivalDate
        {
            get;
            set;
        }

        public virtual string ArrivalTime
        {
            get;
            set;
        }

      

        public virtual string Remark
        {
            get;
            set;
        }

        public virtual string ViewCode
        {
            get;
            set;
        }

        public virtual string IpAddress
        {
            get;
            set;
        }

        public virtual string OrderStatus
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

      

        public virtual Membership Membership 
        {
            get; set; 
        }

        public virtual IList<BookingDetail> ListBookingDetail
        {
            get;
            set; 
        }
        #endregion
    }
}
