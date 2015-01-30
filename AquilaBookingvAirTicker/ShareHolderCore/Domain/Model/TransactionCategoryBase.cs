	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class TransactionCategoryBase
    {

        #region Variable Declarations
        private int _TransactionCategoryId = 0;
        private string _Description = string.Empty;
        private IList<TradingTransaction> _listTransaction;
        #endregion

        #region Constructors
        public TransactionCategoryBase() { }

        public TransactionCategoryBase(
            int TransactionCategoryId,
            string Description)
        {
            this._TransactionCategoryId = TransactionCategoryId;
            this._Description = Description;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is int</value>
        public virtual int TransactionCategoryId
        {
            get { return _TransactionCategoryId; }
            set { _TransactionCategoryId = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>         
        [Display(Name = "Mô tả")]        
        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public virtual IList<TradingTransaction> ListTransaction
        {
            get { return _listTransaction; }
            set { _listTransaction = value; }
        }

        #endregion
    }//End Class
	
	public enum TransactionCategoryColumns
	{
		TransactionCategoryId,
		Description
	}//End enum
}