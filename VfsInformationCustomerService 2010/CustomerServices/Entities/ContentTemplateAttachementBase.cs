	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace VfsCustomerService.Entities
{
	[Serializable]
	public class ContentTemplateAttachementBase
	{
		
		#region Variable Declarations
		private int				_ContentTemplateAttachementID = 0;
		private string				_AttachementDocument = string.Empty;
		private string				_AttachementDescription = string.Empty;
		private int				_ContentTemplateID = 0;
		private DateTime				_CreatedDate = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_ModifiedDate = new DateTime(1900,1,1,0,0,0,0);
		#endregion
		
		#region Constructors
		public ContentTemplateAttachementBase() {}
		
		public ContentTemplateAttachementBase (
			int ContentTemplateAttachementID,
			string AttachementDocument,
			string AttachementDescription,
			int ContentTemplateID,
			DateTime CreatedDate,
			DateTime ModifiedDate)
		
		{
			this._ContentTemplateAttachementID = ContentTemplateAttachementID;
			this._AttachementDocument = AttachementDocument;
			this._AttachementDescription = AttachementDescription;
			this._ContentTemplateID = ContentTemplateID;
			this._CreatedDate = CreatedDate;
			this._ModifiedDate = ModifiedDate;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ContentTemplateAttachementID
		{
			get { return _ContentTemplateAttachementID; }
			set { _ContentTemplateAttachementID = value; }
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
		public int ContentTemplateID
		{
			get { return _ContentTemplateID; }
			set { _ContentTemplateID = value; }
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
	
	public enum ContentTemplateAttachementColumns
	{
		ContentTemplateAttachementID,
		AttachementDocument,
		AttachementDescription,
		ContentTemplateID,
		CreatedDate,
		ModifiedDate
	}//End enum
}