using System;
using System.Collections.Generic;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class CityBase
    {
        #region variable Decraction
        protected int _CityID = 0;
        protected byte _LanguageID = 0;
        protected string _Name = string.Empty;
		protected IList<LocationCity> _listLocationCity;
        #endregion
        //////////////////////////////
        #region Contruction
        public CityBase() { }
        public CityBase
            (
            int CityID,
            byte LanguageID,
            string Name
            )
        {
            this._CityID = CityID;
            this._LanguageID = LanguageID;
            this._Name = Name;
        }
        #endregion
        /////////////////////////////
        #region Properties
        public virtual int CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }
         public virtual  byte LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }
         public virtual  string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        #endregion
        /////////////////////////////

		 public virtual   IList<LocationCity> ListLocationCity
		{
			get { return _listLocationCity; }
			set { _listLocationCity = value; }
		} 
    }
    public enum CityColumns
    {
        CityID,
        LanguageID,
        Name
    }
}
