using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class LocationCityModel
    {
        [Required]
        [Display(Name = "Location Id ")]
        public int LocationID
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "City Id ")]
        public int CityID
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "LocationCity Id ")]
        public int LocationCityID
        {
            get;
            set;
        }
        public IList<LocationCityModel> ListLocationCity { get; set; }
    }
}