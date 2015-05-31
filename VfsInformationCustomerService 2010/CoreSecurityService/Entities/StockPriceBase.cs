	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace CoreSecurityService.Entities
{
	[Serializable]
	public class StockPriceBase
	{
		
		#region Variable Declarations
		private string				_TradingDate = string.Empty;
		private string				_StockCode = string.Empty;
		private int				_StockNo = 0;
		private string				_StockType = string.Empty;
		private string				_BoardType = string.Empty;
		private decimal				_OpenPrice = 0;
		private decimal				_ClosePrice = 0;
		private decimal				_BasicPrice = 0;
		private decimal				_CeilingPrice = 0;
		private decimal				_FloorPrice = 0;
		private decimal				_AveragePrice = 0;
		private DateTime				_TransactionDate = new DateTime(1900,1,1,0,0,0,0);
		private decimal				_TotalRoom = 0;
		private decimal				_CurrentRoom = 0;
		private string				_Suspension = string.Empty;
		private string				_Delisted = string.Empty;
		private string				_Halted = string.Empty;
		private string				_Split = string.Empty;
		private string				_Benefit = string.Empty;
		private string				_Meeting = string.Empty;
		private string				_Notice = string.Empty;
		#endregion
		
		#region Constructors
		public StockPriceBase() {}
		
		public StockPriceBase (
			string TradingDate,
			string StockCode,
			int StockNo,
			string StockType,
			string BoardType,
			decimal OpenPrice,
			decimal ClosePrice,
			decimal BasicPrice,
			decimal CeilingPrice,
			decimal FloorPrice,
			decimal AveragePrice,
			DateTime TransactionDate,
			decimal TotalRoom,
			decimal CurrentRoom,
			string Suspension,
			string Delisted,
			string Halted,
			string Split,
			string Benefit,
			string Meeting,
			string Notice)
		
		{
			this._TradingDate = TradingDate;
			this._StockCode = StockCode;
			this._StockNo = StockNo;
			this._StockType = StockType;
			this._BoardType = BoardType;
			this._OpenPrice = OpenPrice;
			this._ClosePrice = ClosePrice;
			this._BasicPrice = BasicPrice;
			this._CeilingPrice = CeilingPrice;
			this._FloorPrice = FloorPrice;
			this._AveragePrice = AveragePrice;
			this._TransactionDate = TransactionDate;
			this._TotalRoom = TotalRoom;
			this._CurrentRoom = CurrentRoom;
			this._Suspension = Suspension;
			this._Delisted = Delisted;
			this._Halted = Halted;
			this._Split = Split;
			this._Benefit = Benefit;
			this._Meeting = Meeting;
			this._Notice = Notice;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string TradingDate
		{
			get { return _TradingDate; }
			set { _TradingDate = value; }
		}
	
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string StockCode
		{
			get { return _StockCode; }
			set { _StockCode = value; }
		}
		
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int StockNo
		{
			get { return _StockNo; }
			set { _StockNo = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string StockType
		{
			get { return _StockType; }
			set { _StockType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string BoardType
		{
			get { return _BoardType; }
			set { _BoardType = value; }
		}
		public string originalBoardType
		{
			get { return originalBoardType; }
			set { originalBoardType = value; }
		} 
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is decimal</value>
		public decimal OpenPrice
		{
			get { return _OpenPrice; }
			set { _OpenPrice = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is decimal</value>
		public decimal ClosePrice
		{
			get { return _ClosePrice; }
			set { _ClosePrice = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is decimal</value>
		public decimal BasicPrice
		{
			get { return _BasicPrice; }
			set { _BasicPrice = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is decimal</value>
		public decimal CeilingPrice
		{
			get { return _CeilingPrice; }
			set { _CeilingPrice = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is decimal</value>
		public decimal FloorPrice
		{
			get { return _FloorPrice; }
			set { _FloorPrice = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is decimal</value>
		public decimal AveragePrice
		{
			get { return _AveragePrice; }
			set { _AveragePrice = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime TransactionDate
		{
			get { return _TransactionDate; }
			set { _TransactionDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is decimal</value>
		public decimal TotalRoom
		{
			get { return _TotalRoom; }
			set { _TotalRoom = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is decimal</value>
		public decimal CurrentRoom
		{
			get { return _CurrentRoom; }
			set { _CurrentRoom = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Suspension
		{
			get { return _Suspension; }
			set { _Suspension = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Delisted
		{
			get { return _Delisted; }
			set { _Delisted = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Halted
		{
			get { return _Halted; }
			set { _Halted = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Split
		{
			get { return _Split; }
			set { _Split = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Benefit
		{
			get { return _Benefit; }
			set { _Benefit = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Meeting
		{
			get { return _Meeting; }
			set { _Meeting = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Notice
		{
			get { return _Notice; }
			set { _Notice = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum StockPriceColumns
	{
		TradingDate,
		StockCode,
		StockNo,
		StockType,
		BoardType,
		OpenPrice,
		ClosePrice,
		BasicPrice,
		CeilingPrice,
		FloorPrice,
		AveragePrice,
		TransactionDate,
		TotalRoom,
		CurrentRoom,
		Suspension,
		Delisted,
		Halted,
		Split,
		Benefit,
		Meeting,
		Notice
	}//End enum
}