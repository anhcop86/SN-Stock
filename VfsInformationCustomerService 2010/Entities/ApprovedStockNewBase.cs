	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Entities
{
	[Serializable]
	public class ApprovedStockNewBase
	{
		
		#region Variable Declarations
		private long				_NewsId = 0;
		private string				_NewsTitle = string.Empty;
		private string				_NewsDescription = string.Empty;
		private string				_NewsContent = string.Empty;
		private DateTime				_NewsDate = new DateTime(1900,1,1,0,0,0,0);
		private string				_NewsSource = string.Empty;
		private string				_ShareSymbol = string.Empty;
		private bool				_UseUrl = false;
		private string				_NewsUrl = string.Empty;
		private int				_LanguageID = 0;
		private int				_IsApproved = 0;
		private string				_ImageUrl = string.Empty;
		private string				_Comment = string.Empty;
		private int				_LinkId = 0;
		private string				_OriginalUrl = string.Empty;
		private DateTime				_ApprovedDate = new DateTime(1900,1,1,0,0,0,0);
		#endregion
		
		#region Constructors
		public ApprovedStockNewBase() {}
		
		public ApprovedStockNewBase (
			int NewsId,
			string NewsTitle,
			string NewsDescription,
			string NewsContent,
			DateTime NewsDate,
			string NewsSource,
			string ShareSymbol,
			bool UseUrl,
			string NewsUrl,
			int LanguageID,
			int IsApproved,
			string ImageUrl,
			string Comment,
			int LinkId,
			string OriginalUrl,
			DateTime ApprovedDate)
		
		{
			this._NewsId = NewsId;
			this._NewsTitle = NewsTitle;
			this._NewsDescription = NewsDescription;
			this._NewsContent = NewsContent;
			this._NewsDate = NewsDate;
			this._NewsSource = NewsSource;
			this._ShareSymbol = ShareSymbol;
			this._UseUrl = UseUrl;
			this._NewsUrl = NewsUrl;
			this._LanguageID = LanguageID;
			this._IsApproved = IsApproved;
			this._ImageUrl = ImageUrl;
			this._Comment = Comment;
			this._LinkId = LinkId;
			this._OriginalUrl = OriginalUrl;
			this._ApprovedDate = ApprovedDate;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public long NewsId
		{
			get { return _NewsId; }
			set { _NewsId = value; }
		}
		public int originalNewsId
		{
			get { return originalNewsId; }
			set { originalNewsId = value; }
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
		/// <value>This type is nvarchar</value>
		public string ShareSymbol
		{
			get { return _ShareSymbol; }
			set { _ShareSymbol = value; }
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
		/// <value>This type is int</value>
		public int IsApproved
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
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Comment
		{
			get { return _Comment; }
			set { _Comment = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int LinkId
		{
			get { return _LinkId; }
			set { _LinkId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string OriginalUrl
		{
			get { return _OriginalUrl; }
			set { _OriginalUrl = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is datetime</value>
		public DateTime ApprovedDate
		{
			get { return _ApprovedDate; }
			set { _ApprovedDate = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum ApprovedStockNewColumns
	{
		NewsId,
		NewsTitle,
		NewsDescription,
		NewsContent,
		NewsDate,
		NewsSource,
		ShareSymbol,
		UseUrl,
		NewsUrl,
		LanguageID,
		IsApproved,
		ImageUrl,
		Comment,
		LinkId,
		OriginalUrl,
		ApprovedDate
	}//End enum
}