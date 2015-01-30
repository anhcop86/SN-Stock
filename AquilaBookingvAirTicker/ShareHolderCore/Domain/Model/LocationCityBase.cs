using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class LocationCityBase
    {
        protected int _LocationID = 0;
        protected int _CityID = 0;
        protected int _LocationCityID = 0;
        protected IList<LocationCity> _listLocationCity;


        public LocationCityBase() { }
        public LocationCityBase
            (
            int LocationID,
            int CityID,
            int LocationCityID
            )
        {
            this._LocationCityID = LocationID;
            this._CityID = CityID;
            this._LocationCityID = LocationCityID;
        }

        
        public virtual  int LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }

        [Display(Name = "Mã thành phố")]
         public virtual  int CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }

        [Display(Name = "Mã tỉnh")]        
         public virtual  int LocationCityID
        {
            get { return _LocationCityID; }
            set { _LocationCityID = value; }
        }
         public virtual IList<LocationCity> ListLocationCity
         {
             get { return _listLocationCity; }
             set { _listLocationCity = value; }
         }
    }
    public enum LocationCityColumns
    {
        LocationID,
        CityID,
        LocationCityID
    }
}
