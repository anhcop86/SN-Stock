using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class TransactionDetailModel
    {
        #region Properties
        /// <summary>
        /// 	
        [Required]
        [Display(Name = "TransactionDetailId")]
        public long TransactionDetailId
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        [Required]
        [Display(Name = "TransactionId")]
        public long TransactionId
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "ShareSymbol")]
        public string ShareSymbol
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "ShareHolderId")]
        public int ShareHolderId
        {
            get;
            set;
        }
       
        [Required]
        [Display(Name = "ShareTypeId")]
        public int ShareTypeId
        {
            get;
            set;
        }
       
        [Display(Name = "TransactionType")]
        [StringLength(6, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string TransactionType
        {
            get;
            set;
        }
       
        [Display(Name = "Quantity")]
        public long Quantity
        {
            get;
            set;
        }

        [Display(Name = "BidId")]
        public Guid? BidId
        {
            get;
            set;
        }
        public IList<TransactionDetailModel> ListTransactionDetail
        {
            get;
            set;
        }
        #endregion
    }
}