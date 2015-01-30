	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
	[Serializable]
	public class BankBase
	{
		
		#region Variable Declarations
		protected int				_BankId = 0;
		protected string				_Name = string.Empty;
		protected string				_Address = string.Empty;
		protected string				_Phone = string.Empty;
		protected string				_Website = string.Empty;
		protected string				_Fax = string.Empty;
        protected IList<Bank> _listBank;
		protected IList listShareHolder;

		#endregion
		
		#region Constructors
		public BankBase() { listShareHolder = null; }
		
		public BankBase (
			int BankId,
			string Name,
			string Address,
			string Phone,
			string Website,
			string Fax)
		
		{
			this._BankId = BankId;
			this._Name = Name;
			this._Address = Address;
			this._Phone = Phone;
			this._Website = Website;
			this._Fax = Fax;
			listShareHolder = null; 
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public virtual  int BankId
		{
			get { return _BankId; }
			set { _BankId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Tên ngân hàng")]
        [Required(ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(10, ErrorMessageResourceName = "nameBank", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
         public virtual string Name
         {
             get { return _Name; }
             set { _Name = value; }
         }
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        /// 
        [Display(Name = "Địa chỉ")]
		 public virtual  string Address
		{
			get { return _Address; }
			set { _Address = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Số điện thoại")]
		 public virtual  string Phone
		{
			get { return _Phone; }
			set { _Phone = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Website")]
        public virtual  string Website
		{
			get { return _Website; }
			set { _Website = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        [Display(Name = "Số fax")]
        public virtual  string Fax
		{
			get { return _Fax; }
			set { _Fax = value; }
		}


        public virtual IList<Bank> ListBank
        {
            get { return _listBank ;}
            set { _listBank = value; }
        }
             
		/// <summary>
		/// 
		/// </summary>		
		//public virtual IList ListShareHolder
		//{
		//    get
		//    {
		//        if (listShareHolder == null)
		//        {
		//            listShareHolder = new ArrayList();
		//        }
		//        return listShareHolder;
		//    }
		//    set { listShareHolder = value; }
		//} 
		#endregion
	}//End Class
	
	public enum BankColumns
	{
		BankId,
		Name,
		Address,
		Phone,
		Website,
		Fax
	}//End enum
}