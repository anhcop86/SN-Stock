	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Destination.Entities
{
	[Serializable]
	public class ExtensionMessageBase
	{
		
		#region Variable Declarations
		private long				_ExtensionMessageID = 0;
		private string				_Title = string.Empty;
		private string				_Content = string.Empty;
		private DateTime				_CreatedDate = new DateTime(1900,1,1,0,0,0,0);
		#endregion
		
		#region Constructors
		public ExtensionMessageBase() {}
		
		public ExtensionMessageBase (
			long ExtensionMessageID,
			string Title,
			string Content,
			DateTime CreatedDate)
		
		{
			this._ExtensionMessageID = ExtensionMessageID;
			this._Title = Title;
			this._Content = Content;
			this._CreatedDate = CreatedDate;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bigint</value>
		public long ExtensionMessageID
		{
			get { return _ExtensionMessageID; }
			set { _ExtensionMessageID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Title
		{
			get { return _Title; }
			set { _Title = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Content
		{
			get { return _Content; }
			set { _Content = value; }
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
	
		
		#endregion
	}//End Class
	
	public enum ExtensionMessageColumns
	{
		ExtensionMessageID,
		Title,
		Content,
		CreatedDate
	}//End enum
}