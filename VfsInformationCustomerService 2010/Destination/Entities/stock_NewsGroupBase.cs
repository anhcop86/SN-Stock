	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Destination.Entities
{
	[Serializable]
	public class stock_NewsGroupBase
	{
		
		#region Variable Declarations
		private int				_ID = 0;
		private int				_NewsID = 0;
		private int				_NewsGroup = 0;
		#endregion
		
		#region Constructors
		public stock_NewsGroupBase() {}
		
		public stock_NewsGroupBase (
			int ID,
			int NewsID,
			int NewsGroup)
		
		{
			this._ID = ID;
			this._NewsID = NewsID;
			this._NewsGroup = NewsGroup;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int NewsID
		{
			get { return _NewsID; }
			set { _NewsID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int NewsGroup
		{
			get { return _NewsGroup; }
			set { _NewsGroup = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum stock_NewsGroupColumns
	{
		ID,
		NewsID,
		NewsGroup
	}//End enum
}