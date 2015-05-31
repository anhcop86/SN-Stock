	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class MessageContentAttachementBase
	{
		
		#region Variable Declarations
		private int				_MessageContentAttachementID = 0;
		private string				_AttachementDocument = string.Empty;
		private string				_AttachementDescription = string.Empty;
		private int				_MessageContentID = 0;
		private DateTime				_CreatedDate = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_ModifiedDate = new DateTime(1900,1,1,0,0,0,0);
		#endregion
		
		#region Constructors
		public MessageContentAttachementBase() {}
		
		public MessageContentAttachementBase (
			int MessageContentAttachementID,
			string AttachementDocument,
			string AttachementDescription,
			int MessageContentID,
			DateTime CreatedDate,
			DateTime ModifiedDate)
		
		{
			this._MessageContentAttachementID = MessageContentAttachementID;
			this._AttachementDocument = AttachementDocument;
			this._AttachementDescription = AttachementDescription;
			this._MessageContentID = MessageContentID;
			this._CreatedDate = CreatedDate;
			this._ModifiedDate = ModifiedDate;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int MessageContentAttachementID
		{
			get { return _MessageContentAttachementID; }
			set { _MessageContentAttachementID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string AttachementDocument
		{
			get { return _AttachementDocument; }
			set { _AttachementDocument = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string AttachementDescription
		{
			get { return _AttachementDescription; }
			set { _AttachementDescription = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int MessageContentID
		{
			get { return _MessageContentID; }
			set { _MessageContentID = value; }
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
	
	public enum MessageContentAttachementColumns
	{
		MessageContentAttachementID,
		AttachementDocument,
		AttachementDescription,
		MessageContentID,
		CreatedDate,
		ModifiedDate
	}//End enum
}