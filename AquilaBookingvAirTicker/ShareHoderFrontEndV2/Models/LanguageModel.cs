using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShareHoderFrontEndV2.Models
{
    public class LanguageModel
    {
        [Required]
        [Display(Name = "LanguageID")]

        public Int16 LanguageID
        {
            get;
            set;
        }
                
        [Display(Name = "Description ")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]       
        public string Description
        {
            get;
            set;
        }
    }
}