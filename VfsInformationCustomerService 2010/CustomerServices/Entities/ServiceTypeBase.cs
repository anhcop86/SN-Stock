	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class ServiceTypeBase
	{
		
		#region Variable Declarations
		private int				_ServiceTypeID = 0;
		private string				_ServiceTypeDescription = string.Empty;
		private DateTime				_CreatedDate = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_ModifiedDate = new DateTime(1900,1,1,0,0,0,0);
		#endregion
		
		#region Constructors
		public ServiceTypeBase() {}
		
		public ServiceTypeBase (
			int ServiceTypeID,
			string ServiceTypeDescription,
			DateTime CreatedDate,
			DateTime ModifiedDate)
		
		{
			this._ServiceTypeID = ServiceTypeID;
			this._ServiceTypeDescription = ServiceTypeDescription;
			this._CreatedDate = CreatedDate;
			this._ModifiedDate = ModifiedDate;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ServiceTypeID
		{
			get { return _ServiceTypeID; }
			set { _ServiceTypeID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ServiceTypeDescription
		{
			get { return _ServiceTypeDescription; }
			set { _ServiceTypeDescription = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is datetime</value>
		public DateTime CreatedDate
		{
			get { return _CreatedDate; }
			set { _CreatedDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is datetime</value>
		public DateTime ModifiedDate
		{
			get { return _ModifiedDate; }
			set { _ModifiedDate = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum ServiceTypeColumns
	{
		ServiceTypeID,
		ServiceTypeDescription,
		CreatedDate,
		ModifiedDate
	}//End enum

    public enum MessageServiceType
    {
        Email =1,
        Sms = 2        
    }//End enum
}