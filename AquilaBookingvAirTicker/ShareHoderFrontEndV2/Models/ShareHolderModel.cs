using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.Data;

namespace ShareHoderFrontEndV2.Models
{
    public class ShareHolderModel
    {
        #region Properties
        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is int</value>
        [Required]
        [Display(Name = "ShareHolderId")]
        public int ShareHolderId
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Required]
        [Display(Name = "Name")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Address")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string Address
        {
            get;
            set;
        }

        [Display(Name = "Address2")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string Address2
        {
            get;
            set;
        }
        [Display(Name = "Phone")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string Phone
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
       [Display(Name = "Mobile")]
       [StringLength(20, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string Mobile
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
        
        /// <value>This type is nvarchar</value>
        [Display(Name = "Ssn")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string SSN
        {
            get;
            set;
        }

        /// <summary>
        /// 	
        /// </summary>
       [Display(Name = "Nationality")]
       [StringLength(10, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string Nationality
        {
            get;
            set;
        }


        /////////////////////////////////////
        [Required]
        [Display(Name = "ShareHolderGroupId")]
        public int ShareHolderGroupId
        {
            get;
            set;
        }
        ////////////////////////////////////////
        [Display(Name = "ShareHolderCode")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string ShareHolderCode
        {
            get;
            set;
        }
        ////////////////////////////////////////
        [Display(Name = "Dob")]
        public DateTime Dob
        {
            get;
            set;
        }
        //////////////////////////////////////////
        [Display(Name = "BirthPlace")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string BirthPlace
        {
            get;
            set;
        }
        //////////////////////////////////////////
        [Display(Name = "WorkPlace")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string WorkPlace
        {
            get;
            set;
        }
        ///////////////////////////////////////////
        [Display(Name = "IssueDate")]
        public DateTime IssueDate
        {
            get;
            set;
        }
        ///////////////////////////////////////////
        [Display(Name = "IssuePlace")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string IssuePlace
        {
            get;
            set;
        }
        ///////////////////////////////////////////
        [Display(Name = "ContactAddress")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string ContactAddress
        {
            get;
            set;
        }
        ////////////////////////////////////////////
        [Display(Name = "RepTradingPerson")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string RepTradingPerson
        {
            get;
            set;
        }
        //////////////////////////////////////////////
        [Display(Name = "Repssn")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string RepSSN
        {
            get;
            set;
        }
        /////////////////////////////////////////////
        [Display(Name = "RepIssueDate")]
        public DateTime RepIssueDate
        {
            get;
            set;
        }
        ////////////////////////////////////////////
        [Display(Name = "RepIssuePlace")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string RepIssuePlace
        {
            get;
            set;
        }
        ////////////////////////////////////////////
        [Display(Name = "Note")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string Note
        {
            get;
            set;
        }
        ////////////////////////////////////////////
        [Display(Name = "Gender")]
        [StringLength(1, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 1)]
        public string Gender
        {
            get;
            set;
        }
        [Display(Name = "StartDate")]
        public DateTime StartDate
        {
            get;
            set;
        }
        [Display(Name = "AddressCityId")]
        public int AddressCityId
        {
            get;
            set;
        }
        [Display(Name = "AddressLocationId")]
        public int AddressLocationId
        {
            get;
            set;
        }
        [Display(Name = "Address2CityId")]
        public int? Address2CityId
        {
            get;
            set;
        }
        [Display(Name = "Address2LocationId")]
        public int? Address2LocationId
        {
            get;
            set;
        }
       
        [Display(Name = "BankAccountNumber")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 6)]
        public string BankAccountNumber
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "BankId")]
        public int? BankId
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "UserId")]
        public int? UserId
        {
            get;
            set;
        }
        [Display(Name = "ShareHolderType")]
        [StringLength(1, ErrorMessage = "The {0} must be at least {10} characters long.", MinimumLength = 1)]
        public string ShareHolderType
        {
            get;
            set;
        }
        [Display(Name = "ParentShareHolderId")]
        public int ParentShareHolderId
        {
            get;
            set;
        }
        public IList<ShareHolderModel> ListShareHolder { get; set; }
        #endregion
    }
}