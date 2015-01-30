using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class BookingDetailBase
    {
        #region Primitive Properties

        public virtual long BookingDetailId
        {
            get;
            set;
        }

       

        public virtual int HotelId
        {
            get;
            set;
        }

        public virtual int CancelQuantity
        {
            get;
            set;
        }


        public virtual Nullable<int> Quantity
        {
            get;
            set;
        }

        public virtual Nullable<decimal> Price
        {
            get;
            set;
        }

        public virtual string BookCode
        {
            get;
            set;
        }
        public virtual string FlightNumber
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

        public virtual string Remark
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

        public virtual Booking Booking
        {
            get;
            set;
        }

        public virtual string AirLine { get; set; }

        public virtual string IssueDate { get; set; }

        public virtual string PassengerName { get; set; }

        public virtual string Departure { get; set; }

        public virtual string Arrival { get; set; }

        public virtual decimal Fare { get; set; }

        public virtual decimal Fees { get; set; }

        public virtual string BookingType { get; set; }


        #endregion
    }
}
