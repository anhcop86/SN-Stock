using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class BankModel
    {
        #region Properties
        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is int</value>
        /// 
        [Required]
        [Display(Name = "Bank Id")]
        public  int BankId
        {
            get ;  set; 
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        /// 
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public  string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Required]
        [Display(Name = "Address")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 10)]
        public  string Address
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        ///  [Required]
        [Display(Name = "Phone")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public  string Phone
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        ///  [Required]
        [Display(Name = "Web site")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public  string Website
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        ///         
        [Display(Name = "Fax")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 10)]
        public  string Fax
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>		
        
        #endregion
    }
}