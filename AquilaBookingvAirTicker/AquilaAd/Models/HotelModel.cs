using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AquilaAd.Models
{
    public class HotelModel
    {
        #region Primitive Properties
        
        public  int HotelId
        {
            get;
            set;
        }

        
        [Required(ErrorMessageResourceName = "HotelName", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]        
        [Display(Name = "Tên khách sạn")]
        public string Name { get; set; }

        [Required]
        public  Nullable<int> Star
        {
            get;
            set;
        }

        [Required] 
        public  string HotelAddress
        {
            get;
            set;
        }
        [Required]
        public  string ShortDesc
        {
            get;
            set;
        }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "Page Content")]
        public  string LongDesc
        {
            get;
            set;
        }

       

        [Required]
        public  string Location
        {
            get;
            set;
        }


        [Required]
        public  Nullable<int> ProvinceId
        {
            get;
            set;
        }


        public  string CreatedDate
        {
            get;
            set;
        }

        public  string CreatedBy
        {
            get;
            set;
        }
        public  string MostView
        {
            get;
            set;
        }

        public string BookingCondition
        {
            get;
            set;
        }

        public string Display
        {
            get;
            set;
        }

            


        #endregion

        #region Main methol
            
        #endregion
    }
}