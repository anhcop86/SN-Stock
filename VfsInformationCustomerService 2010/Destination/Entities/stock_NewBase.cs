	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Destination.Entities
{
	[Serializable]
	public class stock_NewBase
	{
		
		#region Variable Declarations
		private int				_NewsID = 0;
		private string				_NewsTitle = string.Empty;
		private string				_NewsDescription = string.Empty;
		private string				_NewsContent = string.Empty;
		private DateTime				_NewsDate = new DateTime(1900,1,1,0,0,0,0);
		private string				_NewsSource = string.Empty;
		private int?				_SymbolID;
		private bool				_UseUrl = false;
		private string				_NewsUrl = string.Empty;
		private int				_LanguageID = 0;
		private bool				_IsApproved = false;
		private string				_ImageUrl = string.Empty;
		#endregion
		
		#region Constructors
		public stock_NewBase() {}
		
		public stock_NewBase (
			int NewsID,
			string NewsTitle,
			string NewsDescription,
			string NewsContent,
			DateTime NewsDate,
			string NewsSource,
			int SymbolID,
			bool UseUrl,
			string NewsUrl,
			int LanguageID,
			bool IsApproved,
			string ImageUrl)
		
		{
			this._NewsID = NewsID;
			this._NewsTitle = NewsTitle;
			this._NewsDescription = NewsDescription;
			this._NewsContent = NewsContent;
			this._NewsDate = NewsDate;
			this._NewsSource = NewsSource;
			this._SymbolID = SymbolID;
			this._UseUrl = UseUrl;
			this._NewsUrl = NewsUrl;
			this._LanguageID = LanguageID;
			this._IsApproved = IsApproved;
			this._ImageUrl = ImageUrl;
		}
		#endregion
		
		#region Properties	
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
		/// <value>This type is nvarchar</value>
		public string NewsTitle
		{
			get { return _NewsTitle; }
			set { _NewsTitle = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string NewsDescription
		{
			get { return _NewsDescription; }
			set { _NewsDescription = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is ntext</value>
		public string NewsContent
		{
			get { return _NewsContent; }
			set { _NewsContent = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is datetime</value>
		public DateTime NewsDate
		{
			get { return _NewsDate; }
			set { _NewsDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string NewsSource
		{
			get { return _NewsSource; }
			set { _NewsSource = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int? SymbolID
		{
			get { return _SymbolID; }
			set { _SymbolID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bit</value>
		public bool UseUrl
		{
			get { return _UseUrl; }
			set { _UseUrl = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string NewsUrl
		{
			get { return _NewsUrl; }
			set { _NewsUrl = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int LanguageID
		{
			get { return _LanguageID; }
			set { _LanguageID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bit</value>
		public bool IsApproved
		{
			get { return _IsApproved; }
			set { _IsApproved = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ImageUrl
		{
			get { return _ImageUrl; }
			set { _ImageUrl = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum stock_NewColumns
	{
		NewsID,
		NewsTitle,
		NewsDescription,
		NewsContent,
		NewsDate,
		NewsSource,
		SymbolID,
		UseUrl,
		NewsUrl,
		LanguageID,
		IsApproved,
		ImageUrl
	}//End enum
}