using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class ShareTypeBase
    {

        #region Variable Declarations
        private int _ShareTypeId = 0;
        private string _Description = string.Empty;
        private IList<TransactionDetail> _listTransactionDetail;
        #endregion

        #region Constructors
        public ShareTypeBase() { }

        public ShareTypeBase(
            int ShareTypeId,
            string Description)
        {
            this._ShareTypeId = ShareTypeId;
            this._Description = Description;
        }
        #endregion

        #region Properties
        public virtual int ShareTypeId
        {
            get { return _ShareTypeId; }
            set { _ShareTypeId = value; }
        }

        [Display(Name = "Mô tả")]
        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        #endregion

        public virtual IList<TransactionDetail> ListTransactionDetail
        {
            get { return _listTransactionDetail; }
            set { _listTransactionDetail = value; }
        }
    }//End Class
	
	public enum ShareTypeColumns
	{
		ShareTypeId,
		Description
	}//End enum
}