using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class PaymentPayoo
    {
        public bool CheckOrder { get; set; }
        public string OrderStatus { get; set; }

        public decimal OrderCash { get; set; }

        public string DeliveryDate { get; set; }

        public decimal OrderFee { get; set; }

        public string PaymentDate { get; set; }

        public string ShippingDate { get; set; }

        public string LongMessage { get; set; }        

        public int SeverityCode { get; set; }

    }
   
}