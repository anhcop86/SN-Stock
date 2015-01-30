using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    public class TransactionDetailHistory
    {
        private string _TransactionNumber = string.Empty;
		private long _TransactionId = 0;
		private DateTime _TransactionDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private string _TypeDescription = string.Empty;
		private string _ShareSymbol = string.Empty;		
		private string _CreditShareHolderCode = string.Empty;
		private string _DebitShareHolderCode = string.Empty;
        private string _CreditShareHolderName = string.Empty;
        private string _DebitShareHolderName = string.Empty;
        private long? _Quantity;
		private string _Note = string.Empty;

		public TransactionDetailHistory() { }
        public TransactionDetailHistory
			(
			 long TransactionId,
            string TransactionNumber,
		DateTime TransactionDate,
        string TypeDescription,
		string ShareSymbol,		
        string CreditShareHolderCode,
        string DebitShareHolderCode,
        string CreditShareHolderName,
        string DebitShareHolderName,
        long Quantity,
		string Note
			)
		{
            this._TransactionId = TransactionId;
            this._TransactionNumber = TransactionNumber;			
			this._TransactionDate = TransactionDate;
            this._TypeDescription = TypeDescription;
			this._ShareSymbol = ShareSymbol;
            this._CreditShareHolderCode = CreditShareHolderCode;
            this._DebitShareHolderCode = DebitShareHolderCode;
            this._CreditShareHolderName = CreditShareHolderName;
            this._DebitShareHolderName = DebitShareHolderName;
            this._Quantity = Quantity;
			this._Note = Note;
		}

        [Required(ErrorMessageResourceName = "transactionNumber", ErrorMessageResourceType = typeof(ValidationMessage))]
        [Display(Name = "Mã giao dịch")]
		public string TransactionNumber
		{
			get { return _TransactionNumber; }
			set { _TransactionNumber = value; }
		}

        [Display(Name = "Mã giao dịch")]
        [Required(ErrorMessageResourceName = "transactionId", ErrorMessageResourceType = typeof(ValidationMessage))]
		public long TransactionId
		{
			get { return _TransactionId; }
			set { _TransactionId = value; }
		}

        [Display(Name = "Ngày")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime TransactionDate
		{
			get { return _TransactionDate; }
			set { _TransactionDate = value; }
		}

        [Required(ErrorMessageResourceName = "shareSymbol", ErrorMessageResourceType = typeof(ValidationMessage))]
        [Display(Name = "Mã cổ phiếu")] 
		public string ShareSymbol
		{
			get { return _ShareSymbol; }
			set { _ShareSymbol = value; }
		}

        [Display(Name = "Khối lượng")]
        [Range(0, 10000000000)]
        [Required(ErrorMessageResourceName = "quantity", ErrorMessageResourceType = typeof(ValidationMessage))]
        public long? Quantity
		{
            get { return _Quantity; }
            set { _Quantity = value; }
		}

        public string CreditShareHolderCode
		{
            get { return _CreditShareHolderCode; }
            set { _CreditShareHolderCode = value; }
		}

        public string DebitShareHolderCode
		{
            get { return _DebitShareHolderCode; }
            set { _DebitShareHolderCode = value; }
		}

        public string CreditShareHolderName
        {
            get { return _CreditShareHolderName; }
            set { _CreditShareHolderName = value; }
        }

        public string DebitShareHolderName
        {
            get { return _DebitShareHolderName; }
            set { _DebitShareHolderName = value; }
        }

        [Display(Name = "Loại hình")]
        [Required(ErrorMessageResourceName = "shareTypeName", ErrorMessageResourceType = typeof(ValidationMessage))]
        public string TypeDescription
		{
            get { return _TypeDescription; }
            set { _TypeDescription = value; }
		}

        [Display(Name = "Ghi chú")]
        [Required(ErrorMessageResourceName = "note", ErrorMessageResourceType = typeof(ValidationMessage))]
		public string Note
		{
			get { return _Note; }
			set { _Note = value; }
		}
    }
}
