	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Entities
{
	[Serializable]
	public class SourceBase
	{
		
		#region Variable Declarations
		private int				_SourceId = 0;
		private string				_SiteName = string.Empty;
		private string				_URL = string.Empty;
		#endregion
		
		#region Constructors
		public SourceBase() {}
		
		public SourceBase (
			int SourceId,
			string SiteName,
			string URL)
		
		{
			this._SourceId = SourceId;
			this._SiteName = SiteName;
			this._URL = URL;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int SourceId
		{
			get { return _SourceId; }
			set { _SourceId = value; }
		}
		public int originalSourceId
		{
			get { return originalSourceId; }
			set { originalSourceId = value; }
		} 
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string SiteName
		{
			get { return _SiteName; }
			set { _SiteName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string URL
		{
			get { return _URL; }
			set { _URL = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum SourceColumns
	{
		SourceId,
		SiteName,
		URL
	}//End enum
}