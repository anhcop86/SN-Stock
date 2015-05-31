	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Destination.Entities
{
	[Serializable]
	public class ExtensionMessageLogBase
	{
		
		#region Variable Declarations
		private long				_ExtensionMessageLogID = 0;
		private long				_ExtensionMessageID = 0;
		private string				_CustomerId = string.Empty;
		#endregion
		
		#region Constructors
		public ExtensionMessageLogBase() {}
		
		public ExtensionMessageLogBase (
			long ExtensionMessageLogID,
			long ExtensionMessageID,
			string CustomerId)
		
		{
			this._ExtensionMessageLogID = ExtensionMessageLogID;
			this._ExtensionMessageID = ExtensionMessageID;
			this._CustomerId = CustomerId;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bigint</value>
		public long ExtensionMessageLogID
		{
			get { return _ExtensionMessageLogID; }
			set { _ExtensionMessageLogID = value; }
		}
	
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
		/// <value>This type is varchar</value>
		public string CustomerId
		{
			get { return _CustomerId; }
			set { _CustomerId = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum ExtensionMessageLogColumns
	{
		ExtensionMessageLogID,
		ExtensionMessageID,
		CustomerId
	}//End enum
}