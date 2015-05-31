	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Entities
{
	[Serializable]
	public class LinkBase
	{
		
		#region Variable Declarations
		private int				_LinkId = 0;
		private int				_SourceId = 0;
		private string				_Link = string.Empty;
		private string				_LinkShortDescription = string.Empty;
		private string				_LinkDescription = string.Empty;
		#endregion
		
		#region Constructors
		public LinkBase() {}
		
		public LinkBase (
			int LinkId,
			int SourceId,
			string Link,
			string LinkShortDescription,
			string LinkDescription)
		
		{
			this._LinkId = LinkId;
			this._SourceId = SourceId;
			this._Link = Link;
			this._LinkShortDescription = LinkShortDescription;
			this._LinkDescription = LinkDescription;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int LinkId
		{
			get { return _LinkId; }
			set { _LinkId = value; }
		}
		public int originalLinkId
		{
			get { return originalLinkId; }
			set { originalLinkId = value; }
		} 
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int SourceId
		{
			get { return _SourceId; }
			set { _SourceId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Link
		{
			get { return _Link; }
			set { _Link = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string LinkShortDescription
		{
			get { return _LinkShortDescription; }
			set { _LinkShortDescription = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string LinkDescription
		{
			get { return _LinkDescription; }
			set { _LinkDescription = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum LinkColumns
	{
		LinkId,
		SourceId,
		Link,
		LinkShortDescription,
		LinkDescription
	}//End enum
}