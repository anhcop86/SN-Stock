using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class HotelOwnerBase
    {
        #region Primitive Properties
        public virtual int HotelOwnerId { get; set; }

        public virtual int MemberId { get; set; }

        public virtual string FullName { get; set; }

        [Required]
        public virtual string PaymentType { get; set; }

        public virtual string Email { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Address { get; set; }

        public virtual string ActiveDate { get; set; }

        public virtual string CloseDate { get; set; }

        public virtual string Remark { get; set; }
         [Required]
        public virtual string CreatedDate { get; set; }
         [Required]
        public virtual string CreatedBy { get; set; }
        #endregion
    }
}
