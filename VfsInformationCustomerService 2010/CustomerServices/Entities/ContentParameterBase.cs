	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class ContentParameterBase
	{
		
		#region Variable Declarations
		private int				_ContentParameterID = 0;
		private string				_ContentParameterName = string.Empty;
		private string				_ContentParameterDescription = string.Empty;
		private string				_ContentParameterActive = string.Empty;
		private DateTime				_CreatedDate = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_ModifiedDate = new DateTime(1900,1,1,0,0,0,0);
        private string _ContentParameterValue = string.Empty;
		#endregion
		
		#region Constructors
		public ContentParameterBase() {}
		
		public ContentParameterBase (
			int ContentParameterID,
			string ContentParameterName,
			string ContentParameterDescription,
			string ContentParameterActive,
			DateTime CreatedDate,
			DateTime ModifiedDate,
            string ContentParameterValue)
		
		{
			this._ContentParameterID = ContentParameterID;
			this._ContentParameterName = ContentParameterName;
			this._ContentParameterDescription = ContentParameterDescription;
			this._ContentParameterActive = ContentParameterActive;
			this._CreatedDate = CreatedDate;
			this._ModifiedDate = ModifiedDate;
            this._ContentParameterValue = ContentParameterValue;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ContentParameterID
		{
			get { return _ContentParameterID; }
			set { _ContentParameterID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ContentParameterName
		{
			get { return _ContentParameterName; }
			set { _ContentParameterName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ContentParameterDescription
		{
			get { return _ContentParameterDescription; }
			set { _ContentParameterDescription = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ContentParameterActive
		{
			get { return _ContentParameterActive; }
			set { _ContentParameterActive = value; }
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

        public string ContentParameterValue
        {
            get { return _ContentParameterValue; }
            set { _ContentParameterValue = value; }
        }
		#endregion
	}//End Class
	
	public enum ContentParameterColumns
	{
		ContentParameterID,
		ContentParameterName,
		ContentParameterDescription,
		ContentParameterActive,
		CreatedDate,
		ModifiedDate,
        ContentParameterValue
	}//End enum
}