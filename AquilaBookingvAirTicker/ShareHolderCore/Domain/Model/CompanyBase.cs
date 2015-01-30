	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
	[Serializable]
	public class CompanyBase
	{
		
		#region Variable Declarations
		protected int				_CompanyId = 0;
		protected string				_ShareSymbol = string.Empty;
		protected string				_Name = string.Empty;
		protected string				_EnglishName = string.Empty;
		protected string				_Martket = string.Empty;
		protected long				_TotalShare = 0;
        protected string              _Address = string.Empty;
        protected string              _Telephone = string.Empty;
        protected string              _Fax = string.Empty;
        protected string              _Website = string.Empty;
        protected long                _Capital = 0;
        protected DateTime _IssueDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
		protected IList<TransactionDetail> _listTransactionDetail;
		#endregion
		
		#region Constructors
		public CompanyBase() {}
		
		public CompanyBase (
			int CompanyId,
			string ShareSymbol,
			string Name,
			string EnglishName,
			string Martket,
			long TotalShare,
            string Address,
            string Telephone,
            string Fax,
            string Website,
            long Capital,
            DateTime IssueDate)
		
		{
			this._CompanyId = CompanyId;
			this._ShareSymbol = ShareSymbol;
			this._Name = Name;
			this._EnglishName = EnglishName;
			this._Martket = Martket;
			this._TotalShare = TotalShare;
            this._Address = Address;
            this._Telephone = Telephone;
            this._Fax = Fax;
            this._Website = Website;
            this._Capital = Capital;
            this._IssueDate = IssueDate;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
        /// 

        public virtual int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        /// 
        [Required(ErrorMessageResourceName = "shareSymbol", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(10, ErrorMessageResourceName = "shareSymbol", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 3)]
        [Display(Name = "Mã cổ phiếu")]
        public virtual string ShareSymbol
        {
            get { return _ShareSymbol; }
            set { _ShareSymbol = value; }
        }
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        /// 
         [Required(ErrorMessageResourceName = "companyName", ErrorMessageResourceType = typeof(ValidationMessage))]
         [StringLength(255, ErrorMessageResourceName = "companyName", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 6)]
         [Display(Name = "Tên công ty")]
         public virtual string Name
         {
             get;
             set;
         }
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>


        [Display(Name = "Tên tiếng Anh")]       
        public virtual  string EnglishName
		{
			get { return _EnglishName; }
			set { _EnglishName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
        /// 
        [Display(Name = "Sàn")]
		 public virtual  string Martket
		{
			get { return _Martket; }
			set { _Martket = value; }
		}
	
/// <summary>
/// ////////////////////
/// </summary>
        [Display(Name = "Tổng số lượng cổ phiếu")]
        [Range(0, 10000000000)]        
        [Required( ErrorMessageResourceName = "totalShare",ErrorMessageResourceType = typeof(ValidationMessage))]
        public virtual long TotalShare
        {
            get;
            set;
        }
        /// <summary>
        /// ///////////////////////////////
        /// </summary>
        /// 
        [Display(Name = "Địa chỉ")]
         public virtual  string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        ////////////////////////////////////
        [Display(Name = "Số điện thoại")]
        public virtual  string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }
        ////////////////////////////////////
        [Display(Name = "Số fax")]
         public virtual  string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        /////////////////////////////////////
        [Display(Name = "Địa chỉ website")]
        public virtual  string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }
        /////////////////////////////////////
        [Display(Name = "Vốn điều lệ")]
        [Range(100000000, 1000000000000000)]
        [Required(ErrorMessageResourceName = "capital", ErrorMessageResourceType = typeof(ValidationMessage))]
         public virtual  long Capital
        {
            get { return _Capital; }
            set { _Capital = value; }
        }
        ////////////////////////////////////
        [Required(ErrorMessageResourceName = "dataTime", ErrorMessageResourceType = typeof(ValidationMessage))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")] 
        [Display(Name = "Ngày phát hành")]
         public virtual  DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
		 public virtual IList<TransactionDetail> ListTransactionDetail
		 {
			 get { return _listTransactionDetail; }
			 set { _listTransactionDetail = value; }
		 }
		#endregion
	}//End Class
	
	public enum CompanyColumns
	{
		CompanyId,
		ShareSymbol,
		Name,
		EnglishName,
		Martket,
		TotalShare,
        Address,
        Fax,
        Website,
        Capital,
        Telephone,
        IssueDate
	}//End enum

    public enum OrderDirection
    {
        ASC = 0,
        DESC = 1,
    }

}