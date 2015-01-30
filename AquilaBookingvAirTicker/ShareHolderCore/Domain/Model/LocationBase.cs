using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class LocationBase
    {
        #region variable Decraction
        protected int _LocationID = 0;
        protected byte _LanguageID = 0;
        protected string _Name = string.Empty;
		protected IList<LocationCity> _listLocationCity;
        #endregion

        #region constructor
        public LocationBase() { }
        public LocationBase
            (
            int LocationID,
            byte LanguageID,
            string Name
            )
        {
            this._LocationID = LocationID;
            this._LanguageID = LanguageID;
            this._Name = Name;
        }
        #endregion

        #region properties        
         public virtual  int LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }

        [Display(Name = "Mã ngôn ngữ")]
        [Required(ErrorMessageResourceName = "languageID", ErrorMessageResourceType = typeof(ValidationMessage))]
        public virtual  byte LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }

        [Display(Name = "Tên ngôn ngữ")]        
         public virtual  string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

		 public virtual   IList<LocationCity> ListLocationCity
		{
			get { return _listLocationCity; }
			set { _listLocationCity = value; }
		} 

        #endregion
    }
    public enum LocationColumns
    {
        LocationID,
        LanguageID,
        Name
    }
}
