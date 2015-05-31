	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class CustomerTypeBase
	{
		
		#region Variable Declarations
		private int				_TypeID = 0;
		private string				_Description = string.Empty;
		#endregion
		
		#region Constructors
		public CustomerTypeBase() {}
		
		public CustomerTypeBase (
			int TypeID,
			string Description)
		
		{
			this._TypeID = TypeID;
			this._Description = Description;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int TypeID
		{
			get { return _TypeID; }
			set { _TypeID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum CustomerTypeColumns
	{
		TypeID,
		Description
	}//End enum
}