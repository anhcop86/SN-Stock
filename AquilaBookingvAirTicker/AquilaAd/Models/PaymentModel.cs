using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Models
{
    public class PaymentModel
    {
        public string BookingCode { get; set; }
        public string PaymentDate { get; set; }
        public DateTime PaymentDateFormatDate { get; set; }
        public string HotelName { get; set; }

        //public string  RoomTypeName { get; set; }

        //public string FromDate { get; set; }

        //public string ToDate { get; set; }

        public decimal  OrderAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal MustPaymentAmount { get; set; }

        public decimal DispositAmount { get; set; }

        public int PaymentStatus { get; set; }
        public string PaymentStatusString { get; set; }
        public string DetailURL;

    }
}