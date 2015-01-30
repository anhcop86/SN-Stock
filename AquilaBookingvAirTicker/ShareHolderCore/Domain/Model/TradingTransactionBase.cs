	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class TradingTransactionBase
    {

        #region Variable Declarations
        private long _TransactionId = 0;
        private string _TransactionNumber = string.Empty;
        private DateTime _TransactionDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private string _Note = string.Empty;
        private int _TransactionCategoryId = 0;
        private string _CompanyName = string.Empty;
        private IList<TransactionDetail> _listTransactionDetail;
        private TransactionCategory transactionCategory;


        #endregion

        #region Constructors
        public TradingTransactionBase() { }

        public TradingTransactionBase(
            long TransactionId,
            string TransactionNumber,
            DateTime TransactionDate,
            string Note,
            int TransactionCategoryId, string CompanyName)
        {
            this._TransactionId = TransactionId;
            this._TransactionNumber = TransactionNumber;
            this._TransactionDate = TransactionDate;
            this._Note = Note;
            this._TransactionCategoryId = TransactionCategoryId;
            this._CompanyName = CompanyName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is bigint</value>

        public virtual long TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Required(ErrorMessageResourceName = "transactionNumber", ErrorMessageResourceType = typeof(ValidationMessage))]
        [Display(Name = "Mã giao dịch")]
        public virtual string TransactionNumber
        {
            get { return _TransactionNumber; }
            set { _TransactionNumber = value; }
        }

        [Display(Name = "Tên công ty")]
        public virtual string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        [Display(Name = "Ngày")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Ghi chú")]
        public virtual string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
       
        /// <value>This type is int</value>
       
        [Display(Name = "Kiểu chuyển nhượng")]
        [Required(ErrorMessageResourceName = "transactionCategoryId", ErrorMessageResourceType = typeof(ValidationMessage))]
        public virtual int TransactionCategoryId
        {
            get { return _TransactionCategoryId; }
            set { _TransactionCategoryId = value; }
        }

        public virtual IList<TransactionDetail> ListTransactionDetail
        {
            get { return _listTransactionDetail; }
            set { _listTransactionDetail = value; }
        }

        public virtual TransactionCategory TransactionCategory
        {
            get { return transactionCategory; }
            set { transactionCategory = value; }
        }
        #endregion
    }//End Class
	
	public enum TransactionColumns
	{
		TransactionId,
		TransactionNumber,
		TransactionDate,
		Note,
		TransactionCategoryId,
        CompanyName
	}//End enum
}