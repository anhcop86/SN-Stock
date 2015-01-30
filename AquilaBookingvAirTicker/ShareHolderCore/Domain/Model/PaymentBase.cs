using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    public class PaymentBase
    {
        public virtual long PaymentId { get; set; }

        public virtual string PaymentDate { get; set; }

        public virtual string PaymentTime { get; set; }

        public virtual int MemberId { get; set; }

        public virtual long BookingId { get; set; }

        public virtual string BookingCode { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Address { get; set; }

        public virtual string PaymentType { get; set; }

        public virtual string PaymentMethod { get; set; }

        public virtual string CardNumber { get; set; }

        public virtual string CardHolderName { get; set; }

        public virtual string ExpiryDate { get; set; }

        public virtual string CVCCode { get; set; }

        public virtual string BankName { get; set; }

        public virtual string Email { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string CheckInTime { get; set; }

        public virtual string Remark { get; set; }

        public virtual int PayooOrderStatus { get; set; }

        public virtual decimal OrderAmount { get; set; }

        public virtual decimal OrderFee { get; set; }

        public virtual string ShippingDateTime { get; set; }

        public virtual string DeliveryDateTime { get; set; }

        public virtual int PaymentStatus { get; set; }

        public virtual decimal DispositAmount { get; set; }

        public virtual decimal PaymentAmount { get; set; }

        public virtual string PaymentGateWay { get; set; }

        public virtual byte CurrencyTypeId { get; set; }

        public virtual string IpAddress { get; set; }

        public virtual string CreatedDate { get; set; }

        public virtual string CreatedBy { get; set; }


    }
}
