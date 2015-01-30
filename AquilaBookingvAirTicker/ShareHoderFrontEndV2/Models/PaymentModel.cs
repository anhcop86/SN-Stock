using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class PaymentModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string  Mobile { get; set; }

        public int ArrivalTime { get; set; }

        public string Remark { get; set; }

    }
}