	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class MessageCommandBase
	{
		
		#region Variable Declarations
		private int				_MessageCommandID = 0;
		private int				_MessageContentID = 0;
		private int				_Status = 0;
		private DateTime				_ProcessedDateTime = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_CreatedDate = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_ModifiedDate = new DateTime(1900,1,1,0,0,0,0);
		#endregion
		
		#region Constructors
		public MessageCommandBase() {}
		
		public MessageCommandBase (
			int MessageCommandID,
			int MessageContentID,
			int Status,
			DateTime ProcessedDateTime,
			DateTime CreatedDate,
			DateTime ModifiedDate)
		
		{
			this._MessageCommandID = MessageCommandID;
			this._MessageContentID = MessageContentID;
			this._Status = Status;
			this._ProcessedDateTime = ProcessedDateTime;
			this._CreatedDate = CreatedDate;
			this._ModifiedDate = ModifiedDate;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int MessageCommandID
		{
			get { return _MessageCommandID; }
			set { _MessageCommandID = value; }
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
		/// <value>This type is int</value>
		public int Status
		{
			get { return _Status; }
			set { _Status = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is datetime</value>
		public DateTime ProcessedDateTime
		{
			get { return _ProcessedDateTime; }
			set { _ProcessedDateTime = value; }
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
	
	public enum MessageCommandColumns
	{
		MessageCommandID,
		MessageContentID,
		Status,
		ProcessedDateTime,
		CreatedDate,
		ModifiedDate
	}//End enum
}