using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    public class AvailabilityBase
    {
        #region Primitive Properties

        public virtual long AvailabilityId
        {
            get;
            set;
        }

        //public virtual int HotelId
        //{
        //    get;
        //    set;

        //}
        

        //public virtual byte RoomTypeId
        //{
        //    get;
        //    set;
            
        //}        
        [Required]
        public virtual string FromDate
        {
            get;
            set;
        }

        [Required]
        public virtual string ToDate
        {
            get;
            set;
        }

        [Required]
        public virtual Nullable<int> Quantity
        {
            get;
            set;
        }

        public virtual string IsHotDeal
        {
            get;
            set;
        }

        public virtual decimal PromotionalPrice
        {
            get;
            set;
        }

        public virtual string IsPromotion
        {
            get;
            set;
        }

       

        //public virtual Nullable<byte> CurrencyTypeId
        //{
        //    get;
        //    set;
        //}

        [Required]
        [DataType(DataType.Custom)]
        [Range(1, 10000000)]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public virtual Nullable<decimal> Price
        {
            get;
            set;
        }
        [Required]
        public virtual string CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Người tạo")]
        [Required(ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(14, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        public virtual string CreatedBy
        {
            get;
            set;
        }
        [Required]
        public virtual RoomType RoomType 
        {
            get; set; 
        }

        public virtual CurrencyType CurrencyType 
        {
            get; set; 
        }
        [Required]
        public virtual Hotel Hotel 
        {
            get; set; 
        }

        #endregion
    }
}
