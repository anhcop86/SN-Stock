using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class CityModel
    {
        #region Properties
        [Required]
        [Display(Name = "Company identification")]
        
        public int CityID
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Language ID ")]       
        public byte LanguageID
        {
            get;
            set;
        }

        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name
        {
            get;
            set;
        }

        public IList<CityModel> ListCityModel
        {
            get;
            set;
        } 
        #endregion
    }
}