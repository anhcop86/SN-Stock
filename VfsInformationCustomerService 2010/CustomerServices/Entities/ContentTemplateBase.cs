	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class ContentTemplateBase
	{
		
		#region Variable Declarations
		private int				_ContentTemplateID = 0;
		private int				_ServiceTypeID = 0;
		private string				_Description = string.Empty;
		private string				_Sender = string.Empty;
		private string				_Receiver = string.Empty;
		private string				_Subject = string.Empty;
		private string				_BodyContentType = string.Empty;
		private string				_BodyEncoding = string.Empty;
		private string				_BodyMessage = string.Empty;
		private DateTime				_CreatedDate = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_ModifiedDate = new DateTime(1900,1,1,0,0,0,0);
		#endregion
		
		#region Constructors
		public ContentTemplateBase() {}
		
		public ContentTemplateBase (
			int ContentTemplateID,
			int ServiceTypeID,
			string Description,
			string Sender,
			string Receiver,
			string Subject,
			string BodyContentType,
			string BodyEncoding,
			string BodyMessage,
			DateTime CreatedDate,
			DateTime ModifiedDate)
		
		{
			this._ContentTemplateID = ContentTemplateID;
			this._ServiceTypeID = ServiceTypeID;
			this._Description = Description;
			this._Sender = Sender;
			this._Receiver = Receiver;
			this._Subject = Subject;
			this._BodyContentType = BodyContentType;
			this._BodyEncoding = BodyEncoding;
			this._BodyMessage = BodyMessage;
			this._CreatedDate = CreatedDate;
			this._ModifiedDate = ModifiedDate;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ContentTemplateID
		{
			get { return _ContentTemplateID; }
			set { _ContentTemplateID = value; }
		}
	
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
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Sender
		{
			get { return _Sender; }
			set { _Sender = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Receiver
		{
			get { return _Receiver; }
			set { _Receiver = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Subject
		{
			get { return _Subject; }
			set { _Subject = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string BodyContentType
		{
			get { return _BodyContentType; }
			set { _BodyContentType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string BodyEncoding
		{
			get { return _BodyEncoding; }
			set { _BodyEncoding = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string BodyMessage
		{
			get { return _BodyMessage; }
			set { _BodyMessage = value; }
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
	
	public enum ContentTemplateColumns
	{
		ContentTemplateID,
		ServiceTypeID,
		Description,
		Sender,
		Receiver,
		Subject,
		BodyContentType,
		BodyEncoding,
		BodyMessage,
		CreatedDate,
		ModifiedDate
	}//End enum
}