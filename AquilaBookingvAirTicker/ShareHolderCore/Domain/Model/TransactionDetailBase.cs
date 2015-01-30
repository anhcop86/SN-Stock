	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
	[Serializable]
	public class TransactionDetailBase
	{
		
		#region Variable Declarations
		private long				_TransactionDetailId = 0;
		private long				_TransactionId = 0;
		private string				_ShareSymbol = string.Empty;
		private ShareHolder				_ShareHolder = null;
		private int				_ShareTypeId = 0;
		private string				_TransactionType = string.Empty;
		private long				_Quantity = 0;
		#endregion
		
		#region Constructors
		public TransactionDetailBase() {}
		
		public TransactionDetailBase (
			long TransactionDetailId,
			long TransactionId,
			string ShareSymbol,
			ShareHolder ShareHolder,
			int ShareTypeId,
			string TransactionType,
			long Quantity)
		
		{
			this._TransactionDetailId = TransactionDetailId;
			this._TransactionId = TransactionId;
			this._ShareSymbol = ShareSymbol;
			this._ShareHolder = ShareHolder;
			this._ShareTypeId = ShareTypeId;
			this._TransactionType = TransactionType;
			this._Quantity = Quantity;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bigint</value>
		 public virtual  long TransactionDetailId
		{
			get { return _TransactionDetailId; }
			set { _TransactionDetailId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bigint</value>
        [Display(Name = "Mã giao dịch")]
        [Required(ErrorMessageResourceName = "transactionId", ErrorMessageResourceType = typeof(ValidationMessage))]
        public virtual  long TransactionId
		{
			get { return _TransactionId; }
			set { _TransactionId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Required(ErrorMessageResourceName = "shareSymbol", ErrorMessageResourceType = typeof(ValidationMessage))]
        [Display(Name = "Mã cổ phiếu")]    
        public virtual  string ShareSymbol
		{
			get { return _ShareSymbol; }
			set { _ShareSymbol = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
        /// <value>This type is int</value>       
		 public virtual ShareHolder ShareHolder
		{
			get { return _ShareHolder; }
			set { _ShareHolder = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
        /// <value>This type is int</value>
        
        [Display(Name = "Loại cổ phiếu")]
        [Required(ErrorMessageResourceName = "shareTypeId", ErrorMessageResourceType = typeof(ValidationMessage))]
		 public virtual  int ShareTypeId
		{
			get { return _ShareTypeId; }
			set { _ShareTypeId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Loại giao dịch")]
        [Required(ErrorMessageResourceName = "transactionType", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(255, ErrorMessageResourceName = "transactionType", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 6)]
		 public virtual  string TransactionType
		{
			get { return _TransactionType; }
			set { _TransactionType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
        /// <value>This type is bigint</value>
        [Display(Name = "Khối lượng")]
        [Range(0, 10000000000)]
        [Required(ErrorMessageResourceName = "quantity", ErrorMessageResourceType = typeof(ValidationMessage))]
		 public virtual  long Quantity
		{
			get { return _Quantity; }
			set { _Quantity = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum TransactionDetailColumns
	{
		TransactionDetailId,
		TransactionId,
		ShareSymbol,
		ShareHolderId,
		ShareTypeId,
		TransactionType,
		Quantity
	}//End enum
}