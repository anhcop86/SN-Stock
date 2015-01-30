using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShareHoderFrontEndV2.Models
{
    public class BidModel
    {
        [Required]
        [Display(Name = "ID identification")]

        public int ID
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "BidDate")]
        public DateTime BidDate
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
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "ShareSymbol")]
        public string ShareSymbol
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Quantity")]
        public long Quantity
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Price")]

        public double Price
        {
            get;
            set;
        }
        
        [Display(Name = "Status")]
        [StringLength(1, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Status
        {
            get;
            set;
        }
        
        [Display(Name = "ExpiredDate")]
        public DateTime ExpiredDate
        {
            get;
            set;
        }
        
        [Display(Name = "CreateDate")]
        public DateTime CreateDate
        {
            get;
            set;
        }

        public IList<BidModel> ListBid
        {
            get;
            set;
        }
    }
}