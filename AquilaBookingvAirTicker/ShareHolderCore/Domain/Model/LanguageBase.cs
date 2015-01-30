	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
	[Serializable]
	public class LanguageBase
	{
		
		#region Variable Declarations
		protected int				_LanguageId = 0;
		protected string				_Description = string.Empty;
	
		#endregion
		
		#region Constructors
		public LanguageBase() {}
		
		public  LanguageBase (
			int _LanguageId,
			string _Description)		
		
		{
			this._LanguageId = _LanguageId;
			this._Description = _Description;
		}

        public virtual int LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
		#endregion
		
	
	}//End Class

    public enum LanguageColumns
	{
        LanguageId,
        Description,
		
	}//End enum
}