using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace ShareHoderFrontEndV2.Models
{
    public class LocationModel
    {
        #region properties
        public LocationModel(string LocationName, string LocationNameViet)
        {
            this.LocationName = LocationName;
            this.LocationNameViet = LocationNameViet;
        }
        public LocationModel()
        {
        }
        public string LocationName
        {
            get;
            set;
        }
        public string LocationNameViet
        {
            get;
            set;
        }

      
        #endregion
    }
}