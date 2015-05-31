	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace Vfs.WebCrawler.Destination.Entities
{
	[Serializable]
	public class stock_SymbolBase
	{
		
		#region Variable Declarations
		private int				_SymbolID = 0;
		private string				_SourceID = string.Empty;
		private string				_Symbol = string.Empty;
		private int				_MarketID = 0;
		private int				_IndustryID = 0;
		private string				_CompanyType = string.Empty;
		private int				_SecType = 0;
		private bool				_IsListing = false;
		#endregion
		
		#region Constructors
		public stock_SymbolBase() {}
		
		public stock_SymbolBase (
			int SymbolID,
			string SourceID,
			string Symbol,
			int MarketID,
			int IndustryID,
			string CompanyType,
			int SecType,
			bool IsListing)
		
		{
			this._SymbolID = SymbolID;
			this._SourceID = SourceID;
			this._Symbol = Symbol;
			this._MarketID = MarketID;
			this._IndustryID = IndustryID;
			this._CompanyType = CompanyType;
			this._SecType = SecType;
			this._IsListing = IsListing;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int SymbolID
		{
			get { return _SymbolID; }
			set { _SymbolID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string SourceID
		{
			get { return _SourceID; }
			set { _SourceID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string Symbol
		{
			get { return _Symbol; }
			set { _Symbol = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int MarketID
		{
			get { return _MarketID; }
			set { _MarketID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int IndustryID
		{
			get { return _IndustryID; }
			set { _IndustryID = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string CompanyType
		{
			get { return _CompanyType; }
			set { _CompanyType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int SecType
		{
			get { return _SecType; }
			set { _SecType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bit</value>
		public bool IsListing
		{
			get { return _IsListing; }
			set { _IsListing = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum stock_SymbolColumns
	{
		SymbolID,
		SourceID,
		Symbol,
		MarketID,
		IndustryID,
		CompanyType,
		SecType,
		IsListing
	}//End enum
}