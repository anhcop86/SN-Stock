using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore.Domain.Model
{
	[Serializable]
	public class TransactionDetailOfShareHolder
	{
		private string _TransactionNumber = string.Empty;
		private long _TransactionId = 0;
		private DateTime _TransactionDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
		private string _Name = string.Empty;
		private string _ShareSymbol = string.Empty;
		private long? _OpenningBalance;
		private long? _Credit;
		private long? _Debit;
		private long? _CurrentBalance;
		private string _ShareHolderCode = string.Empty;
		private string _InteractiveShareHolder = string.Empty;
		private string _DescriptionType = string.Empty;
		private string _Note = string.Empty;
 
        
		public TransactionDetailOfShareHolder() { }
		public TransactionDetailOfShareHolder
			(
			 long TransactionId,
		DateTime TransactionDate,
		string Name,
		string ShareSymbol,
		long OpenningBalance,
		long Credit,
		long Debit,
		long CurrentBalance,
		string ShareHolderCode,
		string InteractiveShareHolder,
		string DescriptionType,
		string Note
			)
		{
			this._TransactionId = TransactionId;
			this._TransactionDate = TransactionDate;
			this._Name = Name;
			this._ShareSymbol = ShareSymbol;
			this._OpenningBalance = OpenningBalance;
			this._Credit = Credit;
			this._Debit = Debit;
			this._CurrentBalance = CurrentBalance;
			this._ShareHolderCode = ShareHolderCode;
			this._InteractiveShareHolder = InteractiveShareHolder;
			this._DescriptionType = DescriptionType;
			this._Note = Note;
		}
		public string TransactionNumber
		{
			get { return _TransactionNumber; }
			set { _TransactionNumber = value; }
		}
		public long TransactionId
		{
			get { return _TransactionId; }
			set { _TransactionId = value; }
		}
		public DateTime TransactionDate
		{
			get { return _TransactionDate; }
			set { _TransactionDate = value; }
		}
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		public string ShareSymbol
		{
			get { return _ShareSymbol; }
			set { _ShareSymbol = value; }
		}
		public long? OpenningBalance
		{
			get { return _OpenningBalance; }
			set { _OpenningBalance = value; }
		}
		public long? Credit
		{
			get { return _Credit; }
			set { _Credit = value; }
		}
		public long? Debit
		{
			get { return _Debit; }
			set { _Debit = value; }
		}
		public long? CurrentBalance
		{
			get { return _CurrentBalance; }
			set { _CurrentBalance = value; }
		}
		public string ShareHolderCode
		{
			get { return _ShareHolderCode; }
			set { _ShareHolderCode = value; }
		}
		public string InteractiveShareHolder
		{
			get { return _InteractiveShareHolder; }
			set { _InteractiveShareHolder = value; }
		}
		public string DescriptionType
		{
			get { return _DescriptionType; }
			set { _DescriptionType = value; }
		}
		public string Note
		{
			get { return _Note; }
			set { _Note = value; }
		}

       
	}
}
