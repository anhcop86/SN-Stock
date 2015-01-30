using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

namespace ShareHolderCore.Domain.Model
{
	[Serializable]
	public class ShareHolder//: ShareHolderBase
	{
		/*public ShareHolder()
		{ }*/

			#region Variable Declarations
		protected int				_ShareHolderId = 0;
		protected string				_Name = string.Empty;
		protected string				_Address = string.Empty;
		protected string				 _Address2 = string.Empty;
		protected string				_Phone = string.Empty;
		protected string				_Mobile = string.Empty;
		protected string				_SSN = string.Empty;
		protected string				_Nationality = string.Empty;
        protected ShareHolderGroup               _ShareHolderGroup = null;
        protected string              _ShareHolderCode = string.Empty;
        protected DateTime            _DOB = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected string              _BirthPlace = string.Empty;
        protected string              _WorkPlace = string.Empty;
        protected DateTime            _IssueDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected string              _IssuePlace = string.Empty;
        protected string              _ContactAddress = string.Empty;
        protected string              _RepTradingPerson = string.Empty;
        protected string              _RepSSN = string.Empty;
        protected DateTime            _RepIssueDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected string              _RepIssuePlace = string.Empty;
        protected string              _Note = string.Empty;
        protected string _Gender = string.Empty;
        protected DateTime _StartDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected int _AddressCityId = 0;
        protected int _AddressLocationId = 0;
        protected int? _Address2CityId;
        protected int? _Address2LocationId;
        protected int? _BankId;
		protected string _BankAccountNumber = string.Empty;
        private string _ShareHolderType;
        private Int32? _ParentShareHolderId;
		protected Membership _Membership = null;
		protected string _ShareSymbol = string.Empty;
		protected IList<TransactionDetail> _listTransactionDetail;
        protected Guid? _UserId;
		#endregion
		
		#region Constructors
		public ShareHolder() {}
		
		public ShareHolder (
			int ShareHolderId,
			string Name,
			string Address,
      string Address2,
			string Phone,
			string Mobile,
			string SSN,
			string Nationality,
            ShareHolderGroup ShareHolderGroup,
            string ShareHolderCode,
            DateTime DOB,
            string BirthPlace,
            string WorkPlace,
            DateTime IssueDate,
            string IssuePlace,
            string ContactAddress,
            string RepTradingPerson,
            string RepSSN,
            DateTime RepIssueDate,
            string RepIssuePlace,
            string Note,
      string Gender,
      DateTime StartDate,

        int AddressCityId,
        int AddressLocationId,
        int Address2CityId,
        int Address2LocationId,
        int BankId,
        string BankAccountNumber
            )
		
		{
			this._ShareHolderId = ShareHolderId;
			this._Name = Name;
			this._Address = Address;
   this._Address2 = Address2;
			this._Phone = Phone;
			this._Mobile = Mobile;
			this._SSN = SSN;
			this._Nationality = Nationality;
            this._ShareHolderGroup =ShareHolderGroup;
            this._ShareHolderCode = ShareHolderCode;
            this._DOB = DOB;
            this._BirthPlace = BirthPlace;
            this._WorkPlace = WorkPlace;
            this._IssueDate=IssueDate;
            this._IssuePlace=IssuePlace;
            this._ContactAddress=ContactAddress;
            this._RepTradingPerson=RepTradingPerson;
            this._RepSSN=RepSSN;
            this._RepIssuePlace=RepIssuePlace;
            this._RepIssueDate=RepIssueDate;
            this._Note=Note;
            this._Gender = Gender;
            this._StartDate = StartDate;

            this._AddressCityId = AddressCityId;
            this._AddressLocationId = AddressLocationId;
            this._Address2CityId = Address2CityId;
            this._Address2LocationId = Address2LocationId;
            this._BankId = BankId;
            this._BankAccountNumber = BankAccountNumber;
        }
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ShareHolderId
		{
			get { return _ShareHolderId; }
			set { _ShareHolderId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Address
		{
			    get { return _Address; }
			    set { _Address = value; }
		}
     public string Address2
     {
         get { return _Address2; }
         set { _Address2 = value; }
     }		
		public string Phone
		{
			get { return _Phone; }
			set { _Phone = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Mobile
		{
			get { return _Mobile; }
			set { _Mobile = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Ssn
		{
			get { return _SSN; }
			set { _SSN = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Nationality
		{
			get { return _Nationality; }
			set { _Nationality = value; }
		}
	
		///////////////////////////////////////
        public ShareHolderGroup ShareHolderGroup
        {
            get { return _ShareHolderGroup; }
            set { _ShareHolderGroup = value; }
        }
        ////////////////////////////////////////
        public string ShareHolderCode
        {
            get { return _ShareHolderCode; }
            set { _ShareHolderCode = value; }
        }
        ////////////////////////////////////////
        public DateTime Dob
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        //////////////////////////////////////////
        public string BirthPlace
        {
            get { return _BirthPlace; }
            set { _BirthPlace = value; }
        }
        //////////////////////////////////////////
        public string WorkPlace
        {
            get { return _WorkPlace; }
            set { _WorkPlace = value; }
        }
        ///////////////////////////////////////////
        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        ///////////////////////////////////////////
        public string IssuePlace
        {
            get { return _IssuePlace; }
            set { _IssuePlace = value; }
        }
        ///////////////////////////////////////////
        public string ContactAddress
        {
            get { return _ContactAddress; }
            set { _ContactAddress = value; }
        }
        ////////////////////////////////////////////
        public string RepTradingPerson
        {
            get { return _RepTradingPerson; }
            set { _RepTradingPerson = value; }
        }
        //////////////////////////////////////////////
        public string Repssn
        {
            get { return _RepSSN; }
            set { _RepSSN = value; }
        }
        /////////////////////////////////////////////
        public DateTime RepIssueDate
        {
            get { return _RepIssueDate; }
            set { _RepIssueDate = value; }
        }
        ////////////////////////////////////////////
        public string RepIssuePlace
        {
            get { return _RepIssuePlace; }
            set { _RepIssuePlace = value; }
        }
        ////////////////////////////////////////////
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        ////////////////////////////////////////////
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
     
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
     public int AddressCityId
     {
         get { return _AddressCityId; }
         set { _AddressCityId = value; }
     }
     public int AddressLocationId
     {
         get { return _AddressLocationId; }
         set { _AddressLocationId = value; }
     }
     public int? Address2CityId
     {
         get { return _Address2CityId; }
         set { _Address2CityId = value; }
     }
     public int? Address2LocationId
     {
         get { return _Address2LocationId; }
         set { _Address2LocationId = value; }
     }
     public int? BankId
     {
         get { return _BankId; }
         set { _BankId = value; }
     }
     public string BankAccountNumber
     {
         get { return _BankAccountNumber; }
         set { _BankAccountNumber = value; }
     }

     public Guid? UserId
     {
         get { return _UserId; }
         set { _UserId = value; }
     }
	 public Membership Membership
	 {
		 get { return _Membership; }
		 set { _Membership = value; }
	 }

	 public string ShareSymbol 
	 {
		 get { return _ShareSymbol; }
		 set { _ShareSymbol = value; }
	 }

   public string ShareHolderType
   {
       get { return _ShareHolderType; }
       set { _ShareHolderType = value; }
   }

   public Int32? ParentShareHolderId
   {
       get { return _ParentShareHolderId; }
       set { _ParentShareHolderId = value; }

   }

	 public IList<TransactionDetail> ListTransactionDetail
	{
		 get { return _listTransactionDetail; }
		 set { _listTransactionDetail = value; }
	 }
		#endregion
	}
}