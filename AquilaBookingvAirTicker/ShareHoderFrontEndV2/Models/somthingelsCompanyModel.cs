using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class CompanyModelelse
    {
        [Required]
        [Display(Name = "Company identification")]
        public Int32 CompanyId { get; set; }

        [Required]
        /*[DataType(DataType.EmailAddress)]*/
        [Display(Name = "Share Symbol")]
        [StringLength(3, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string ShareSymbol { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
       /* [DataType(DataType.Password)]*/
        [Display(Name = "Company Name")]
        public string Name { get; set; }

        /*[DataType(DataType.Password)]*/
        [Display(Name = "English Name")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]       
        public string EnglishName { get; set; }

        [Display(Name = "Martket")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Martket { get; set; }

        [Display(Name = "TotalShare")]
        public Int64 TotalShare { get; set; }

        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Address { get; set; }

        [Display(Name = "Telephone")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Fax { get; set; }

        [Display(Name = "Website")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Website { get; set; }


        [Display(Name = "Capital")]
        public DateTime Capital { get; set; }

        [Display(Name = "IssueDate")]
        public DateTime IssueDate { get; set; }

        public IList<TransactionDetailModel> ListTransactionDetail { get; set; }
    }
}