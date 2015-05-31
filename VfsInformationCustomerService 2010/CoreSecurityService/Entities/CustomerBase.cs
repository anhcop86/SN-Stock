	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace CoreSecurityService.Entities
{
	[Serializable]
	public class CustomerBase
	{
		
		#region Variable Declarations
		private string				_BranchCode = string.Empty;
		private string				_ContractNumber = string.Empty;
		private string				_CustomerId = string.Empty;
		private int				_BrokerId = 0;
		private string				_CustomerName = string.Empty;
		private string				_CustomerNameViet = string.Empty;
		private string				_CustomerType = string.Empty;
		private string				_DomesticForeign = string.Empty;
		private DateTime				_Dob = new DateTime(1900,1,1,0,0,0,0);
		private string				_Sex = string.Empty;
		private byte[]				_SignatureImage1 = new byte[] { 0 };
		private byte[]				_SignatureImage2 = new byte[] { 0 };
		private DateTime				_OpenDate = new DateTime(1900,1,1,0,0,0,0);
		private DateTime				_CloseDate = new DateTime(1900,1,1,0,0,0,0);
		private int				_CardType = 0;
		private string				_CardIdentity = string.Empty;
		private DateTime				_CardDate = new DateTime(1900,1,1,0,0,0,0);
		private string				_CardIssuer = string.Empty;
		private string				_Address = string.Empty;
		private string				_AddressViet = string.Empty;
		private string				_Tel = string.Empty;
		private string				_Fax = string.Empty;
		private string				_Mobile = string.Empty;
		private string				_Mobile2 = string.Empty;
		private string				_Email = string.Empty;
		private int				_UserCreate = 0;
		private DateTime				_DateCreate = new DateTime(1900,1,1,0,0,0,0);
		private int				_UserModify = 0;
		private DateTime				_DateModify = new DateTime(1900,1,1,0,0,0,0);
		private bool				_ProxyStatus = false;
		private string				_AccountStatus = string.Empty;
		private string				_Notes = string.Empty;
		private string				_WorkingAddress = string.Empty;
		private int				_UserIntroduce = 0;
		private int				_AttitudePoint = 0;
		private int				_DepositPoint = 0;
		private int				_ActionPoint = 0;
		private string				_Country = string.Empty;
		private string				_Website = string.Empty;
		private string				_TaxCode = string.Empty;
		private string				_AccountType = string.Empty;
		private string				_OrderType = string.Empty;
		private string				_ReceiveReport = string.Empty;
		private string				_ReceiveReportBy = string.Empty;
		private string				_MarriageStatus = string.Empty;
		private string				_KnowledgeLevel = string.Empty;
		private string				_Job = string.Empty;
		private string				_OfficeFunction = string.Empty;
		private string				_OfficeTel = string.Empty;
		private string				_OfficeFax = string.Empty;
		private string				_HusbandWifeName = string.Empty;
		private string				_HusbandWifeCardNumber = string.Empty;
		private DateTime				_HusbandWifeCardDate = new DateTime(1900,1,1,0,0,0,0);
		private string				_HusbandWifeCardLocation = string.Empty;
		private string				_HusbandWifeTel = string.Empty;
		private string				_HusbandWifeEmail = string.Empty;
		private string				_JoinStockMarket = string.Empty;
		private string				_InvestKnowledge = string.Empty;
		private string				_InvestedIn = string.Empty;
		private string				_InvestTarget = string.Empty;
		private string				_RiskAccepted = string.Empty;
		private string				_InvestFund = string.Empty;
		private string				_DelegatePersonName = string.Empty;
		private string				_DelegatePersonFunction = string.Empty;
		private string				_DelegatePersonCardNumber = string.Empty;
		private DateTime				_DelegateCardDate = new DateTime(1900,1,1,0,0,0,0);
		private string				_DelegateCardLocation = string.Empty;
		private string				_DelegatePersonTel = string.Empty;
		private string				_DelegatePersonEmail = string.Empty;
		private string				_ChiefAccountancyName = string.Empty;
		private string				_ChiefAccountancyCI = string.Empty;
		private DateTime				_ChiefAccountancyCD = new DateTime(1900,1,1,0,0,0,0);
		private string				_ChiefAccountancyIssuer = string.Empty;
		private byte[]				_ChiefAccountancySign1 = new byte[] { 0 };
		private byte[]				_ChiefAccountancySign2 = new byte[] { 0 };
		private string				_CaProxyName = string.Empty;
		private string				_CaProxyCI = string.Empty;
		private DateTime				_CaProxyCD = new DateTime(1900,1,1,0,0,0,0);
		private string				_CaProxyIssuer = string.Empty;
		private byte[]				_CaProxySign1 = new byte[] { 0 };
		private byte[]				_CaProxySign2 = new byte[] { 0 };
		private byte[]				_CompanySign1 = new byte[] { 0 };
		private byte[]				_CompanySign2 = new byte[] { 0 };
		private string				_TradeCode = string.Empty;
		private int				_CustomerAccount = 0;
		private string				_MobileSms = string.Empty;
		private bool				_IsHere = false;
		private string				_MoneyDepositeNumber = string.Empty;
		private string				_MoneyDepositeLocation = string.Empty;
		private int				_DepartmentId = 0;
		private string				_PublicCompanyManage = string.Empty;
		private string				_PublicCompanyHolder = string.Empty;
		private string				_ParentCompanyName = string.Empty;
		private string				_ParentCompanyAddress = string.Empty;
		private string				_ParentCompanyEmail = string.Empty;
		private string				_ParentCompanyTel = string.Empty;
		private int				_PostType = 0;
		private DateTime				_ReOpenDate = new DateTime(1900,1,1,0,0,0,0);
		private int				_UserTakeCared = 0;
		#endregion
		
		#region Constructors
		public CustomerBase() {}
		
		public CustomerBase (
			string BranchCode,
			string ContractNumber,
			string CustomerId,
			int BrokerId,
			string CustomerName,
			string CustomerNameViet,
			string CustomerType,
			string DomesticForeign,
			DateTime Dob,
			string Sex,
			byte[] SignatureImage1,
			byte[] SignatureImage2,
			DateTime OpenDate,
			DateTime CloseDate,
			int CardType,
			string CardIdentity,
			DateTime CardDate,
			string CardIssuer,
			string Address,
			string AddressViet,
			string Tel,
			string Fax,
			string Mobile,
			string Mobile2,
			string Email,
			int UserCreate,
			DateTime DateCreate,
			int UserModify,
			DateTime DateModify,
			bool ProxyStatus,
			string AccountStatus,
			string Notes,
			string WorkingAddress,
			int UserIntroduce,
			int AttitudePoint,
			int DepositPoint,
			int ActionPoint,
			string Country,
			string Website,
			string TaxCode,
			string AccountType,
			string OrderType,
			string ReceiveReport,
			string ReceiveReportBy,
			string MarriageStatus,
			string KnowledgeLevel,
			string Job,
			string OfficeFunction,
			string OfficeTel,
			string OfficeFax,
			string HusbandWifeName,
			string HusbandWifeCardNumber,
			DateTime HusbandWifeCardDate,
			string HusbandWifeCardLocation,
			string HusbandWifeTel,
			string HusbandWifeEmail,
			string JoinStockMarket,
			string InvestKnowledge,
			string InvestedIn,
			string InvestTarget,
			string RiskAccepted,
			string InvestFund,
			string DelegatePersonName,
			string DelegatePersonFunction,
			string DelegatePersonCardNumber,
			DateTime DelegateCardDate,
			string DelegateCardLocation,
			string DelegatePersonTel,
			string DelegatePersonEmail,
			string ChiefAccountancyName,
			string ChiefAccountancyCI,
			DateTime ChiefAccountancyCD,
			string ChiefAccountancyIssuer,
			byte[] ChiefAccountancySign1,
			byte[] ChiefAccountancySign2,
			string CaProxyName,
			string CaProxyCI,
			DateTime CaProxyCD,
			string CaProxyIssuer,
			byte[] CaProxySign1,
			byte[] CaProxySign2,
			byte[] CompanySign1,
			byte[] CompanySign2,
			string TradeCode,
			int CustomerAccount,
			string MobileSms,
			bool IsHere,
			string MoneyDepositeNumber,
			string MoneyDepositeLocation,
			int DepartmentId,
			string PublicCompanyManage,
			string PublicCompanyHolder,
			string ParentCompanyName,
			string ParentCompanyAddress,
			string ParentCompanyEmail,
			string ParentCompanyTel,
			int PostType,
			DateTime ReOpenDate,
			int UserTakeCared)
		
		{
			this._BranchCode = BranchCode;
			this._ContractNumber = ContractNumber;
			this._CustomerId = CustomerId;
			this._BrokerId = BrokerId;
			this._CustomerName = CustomerName;
			this._CustomerNameViet = CustomerNameViet;
			this._CustomerType = CustomerType;
			this._DomesticForeign = DomesticForeign;
			this._Dob = Dob;
			this._Sex = Sex;
			this._SignatureImage1 = SignatureImage1;
			this._SignatureImage2 = SignatureImage2;
			this._OpenDate = OpenDate;
			this._CloseDate = CloseDate;
			this._CardType = CardType;
			this._CardIdentity = CardIdentity;
			this._CardDate = CardDate;
			this._CardIssuer = CardIssuer;
			this._Address = Address;
			this._AddressViet = AddressViet;
			this._Tel = Tel;
			this._Fax = Fax;
			this._Mobile = Mobile;
			this._Mobile2 = Mobile2;
			this._Email = Email;
			this._UserCreate = UserCreate;
			this._DateCreate = DateCreate;
			this._UserModify = UserModify;
			this._DateModify = DateModify;
			this._ProxyStatus = ProxyStatus;
			this._AccountStatus = AccountStatus;
			this._Notes = Notes;
			this._WorkingAddress = WorkingAddress;
			this._UserIntroduce = UserIntroduce;
			this._AttitudePoint = AttitudePoint;
			this._DepositPoint = DepositPoint;
			this._ActionPoint = ActionPoint;
			this._Country = Country;
			this._Website = Website;
			this._TaxCode = TaxCode;
			this._AccountType = AccountType;
			this._OrderType = OrderType;
			this._ReceiveReport = ReceiveReport;
			this._ReceiveReportBy = ReceiveReportBy;
			this._MarriageStatus = MarriageStatus;
			this._KnowledgeLevel = KnowledgeLevel;
			this._Job = Job;
			this._OfficeFunction = OfficeFunction;
			this._OfficeTel = OfficeTel;
			this._OfficeFax = OfficeFax;
			this._HusbandWifeName = HusbandWifeName;
			this._HusbandWifeCardNumber = HusbandWifeCardNumber;
			this._HusbandWifeCardDate = HusbandWifeCardDate;
			this._HusbandWifeCardLocation = HusbandWifeCardLocation;
			this._HusbandWifeTel = HusbandWifeTel;
			this._HusbandWifeEmail = HusbandWifeEmail;
			this._JoinStockMarket = JoinStockMarket;
			this._InvestKnowledge = InvestKnowledge;
			this._InvestedIn = InvestedIn;
			this._InvestTarget = InvestTarget;
			this._RiskAccepted = RiskAccepted;
			this._InvestFund = InvestFund;
			this._DelegatePersonName = DelegatePersonName;
			this._DelegatePersonFunction = DelegatePersonFunction;
			this._DelegatePersonCardNumber = DelegatePersonCardNumber;
			this._DelegateCardDate = DelegateCardDate;
			this._DelegateCardLocation = DelegateCardLocation;
			this._DelegatePersonTel = DelegatePersonTel;
			this._DelegatePersonEmail = DelegatePersonEmail;
			this._ChiefAccountancyName = ChiefAccountancyName;
			this._ChiefAccountancyCI = ChiefAccountancyCI;
			this._ChiefAccountancyCD = ChiefAccountancyCD;
			this._ChiefAccountancyIssuer = ChiefAccountancyIssuer;
			this._ChiefAccountancySign1 = ChiefAccountancySign1;
			this._ChiefAccountancySign2 = ChiefAccountancySign2;
			this._CaProxyName = CaProxyName;
			this._CaProxyCI = CaProxyCI;
			this._CaProxyCD = CaProxyCD;
			this._CaProxyIssuer = CaProxyIssuer;
			this._CaProxySign1 = CaProxySign1;
			this._CaProxySign2 = CaProxySign2;
			this._CompanySign1 = CompanySign1;
			this._CompanySign2 = CompanySign2;
			this._TradeCode = TradeCode;
			this._CustomerAccount = CustomerAccount;
			this._MobileSms = MobileSms;
			this._IsHere = IsHere;
			this._MoneyDepositeNumber = MoneyDepositeNumber;
			this._MoneyDepositeLocation = MoneyDepositeLocation;
			this._DepartmentId = DepartmentId;
			this._PublicCompanyManage = PublicCompanyManage;
			this._PublicCompanyHolder = PublicCompanyHolder;
			this._ParentCompanyName = ParentCompanyName;
			this._ParentCompanyAddress = ParentCompanyAddress;
			this._ParentCompanyEmail = ParentCompanyEmail;
			this._ParentCompanyTel = ParentCompanyTel;
			this._PostType = PostType;
			this._ReOpenDate = ReOpenDate;
			this._UserTakeCared = UserTakeCared;
		}
		#endregion
		
		#region Properties	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string BranchCode
		{
			get { return _BranchCode; }
			set { _BranchCode = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string ContractNumber
		{
			get { return _ContractNumber; }
			set { _ContractNumber = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string CustomerId
		{
			get { return _CustomerId; }
			set { _CustomerId = value; }
		}
		public string originalCustomerId
		{
			get { return originalCustomerId; }
			set { originalCustomerId = value; }
		} 
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int BrokerId
		{
			get { return _BrokerId; }
			set { _BrokerId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string CustomerName
		{
			get { return _CustomerName; }
			set { _CustomerName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string CustomerNameViet
		{
			get { return _CustomerNameViet; }
			set { _CustomerNameViet = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string CustomerType
		{
			get { return _CustomerType; }
			set { _CustomerType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string DomesticForeign
		{
			get { return _DomesticForeign; }
			set { _DomesticForeign = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime Dob
		{
			get { return _Dob; }
			set { _Dob = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Sex
		{
			get { return _Sex; }
			set { _Sex = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is image</value>
		public byte[] SignatureImage1
		{
			get { return _SignatureImage1; }
			set { _SignatureImage1 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is image</value>
		public byte[] SignatureImage2
		{
			get { return _SignatureImage2; }
			set { _SignatureImage2 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime OpenDate
		{
			get { return _OpenDate; }
			set { _OpenDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime CloseDate
		{
			get { return _CloseDate; }
			set { _CloseDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int CardType
		{
			get { return _CardType; }
			set { _CardType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string CardIdentity
		{
			get { return _CardIdentity; }
			set { _CardIdentity = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime CardDate
		{
			get { return _CardDate; }
			set { _CardDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string CardIssuer
		{
			get { return _CardIssuer; }
			set { _CardIssuer = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string Address
		{
			get { return _Address; }
			set { _Address = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string AddressViet
		{
			get { return _AddressViet; }
			set { _AddressViet = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string Tel
		{
			get { return _Tel; }
			set { _Tel = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string Fax
		{
			get { return _Fax; }
			set { _Fax = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string Mobile
		{
			get { return _Mobile; }
			set { _Mobile = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string Mobile2
		{
			get { return _Mobile2; }
			set { _Mobile2 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string Email
		{
			get { return _Email; }
			set { _Email = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int UserCreate
		{
			get { return _UserCreate; }
			set { _UserCreate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime DateCreate
		{
			get { return _DateCreate; }
			set { _DateCreate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int UserModify
		{
			get { return _UserModify; }
			set { _UserModify = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime DateModify
		{
			get { return _DateModify; }
			set { _DateModify = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bit</value>
		public bool ProxyStatus
		{
			get { return _ProxyStatus; }
			set { _ProxyStatus = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string AccountStatus
		{
			get { return _AccountStatus; }
			set { _AccountStatus = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Notes
		{
			get { return _Notes; }
			set { _Notes = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string WorkingAddress
		{
			get { return _WorkingAddress; }
			set { _WorkingAddress = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int UserIntroduce
		{
			get { return _UserIntroduce; }
			set { _UserIntroduce = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int AttitudePoint
		{
			get { return _AttitudePoint; }
			set { _AttitudePoint = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int DepositPoint
		{
			get { return _DepositPoint; }
			set { _DepositPoint = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int ActionPoint
		{
			get { return _ActionPoint; }
			set { _ActionPoint = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string Country
		{
			get { return _Country; }
			set { _Country = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string Website
		{
			get { return _Website; }
			set { _Website = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string TaxCode
		{
			get { return _TaxCode; }
			set { _TaxCode = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string AccountType
		{
			get { return _AccountType; }
			set { _AccountType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string OrderType
		{
			get { return _OrderType; }
			set { _OrderType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string ReceiveReport
		{
			get { return _ReceiveReport; }
			set { _ReceiveReport = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string ReceiveReportBy
		{
			get { return _ReceiveReportBy; }
			set { _ReceiveReportBy = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string MarriageStatus
		{
			get { return _MarriageStatus; }
			set { _MarriageStatus = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string KnowledgeLevel
		{
			get { return _KnowledgeLevel; }
			set { _KnowledgeLevel = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string Job
		{
			get { return _Job; }
			set { _Job = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string OfficeFunction
		{
			get { return _OfficeFunction; }
			set { _OfficeFunction = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string OfficeTel
		{
			get { return _OfficeTel; }
			set { _OfficeTel = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string OfficeFax
		{
			get { return _OfficeFax; }
			set { _OfficeFax = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string HusbandWifeName
		{
			get { return _HusbandWifeName; }
			set { _HusbandWifeName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string HusbandWifeCardNumber
		{
			get { return _HusbandWifeCardNumber; }
			set { _HusbandWifeCardNumber = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime HusbandWifeCardDate
		{
			get { return _HusbandWifeCardDate; }
			set { _HusbandWifeCardDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string HusbandWifeCardLocation
		{
			get { return _HusbandWifeCardLocation; }
			set { _HusbandWifeCardLocation = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string HusbandWifeTel
		{
			get { return _HusbandWifeTel; }
			set { _HusbandWifeTel = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string HusbandWifeEmail
		{
			get { return _HusbandWifeEmail; }
			set { _HusbandWifeEmail = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string JoinStockMarket
		{
			get { return _JoinStockMarket; }
			set { _JoinStockMarket = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string InvestKnowledge
		{
			get { return _InvestKnowledge; }
			set { _InvestKnowledge = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string InvestedIn
		{
			get { return _InvestedIn; }
			set { _InvestedIn = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string InvestTarget
		{
			get { return _InvestTarget; }
			set { _InvestTarget = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string RiskAccepted
		{
			get { return _RiskAccepted; }
			set { _RiskAccepted = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is char</value>
		public string InvestFund
		{
			get { return _InvestFund; }
			set { _InvestFund = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string DelegatePersonName
		{
			get { return _DelegatePersonName; }
			set { _DelegatePersonName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string DelegatePersonFunction
		{
			get { return _DelegatePersonFunction; }
			set { _DelegatePersonFunction = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string DelegatePersonCardNumber
		{
			get { return _DelegatePersonCardNumber; }
			set { _DelegatePersonCardNumber = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime DelegateCardDate
		{
			get { return _DelegateCardDate; }
			set { _DelegateCardDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string DelegateCardLocation
		{
			get { return _DelegateCardLocation; }
			set { _DelegateCardLocation = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string DelegatePersonTel
		{
			get { return _DelegatePersonTel; }
			set { _DelegatePersonTel = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string DelegatePersonEmail
		{
			get { return _DelegatePersonEmail; }
			set { _DelegatePersonEmail = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ChiefAccountancyName
		{
			get { return _ChiefAccountancyName; }
			set { _ChiefAccountancyName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string ChiefAccountancyCI
		{
			get { return _ChiefAccountancyCI; }
			set { _ChiefAccountancyCI = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime ChiefAccountancyCD
		{
			get { return _ChiefAccountancyCD; }
			set { _ChiefAccountancyCD = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string ChiefAccountancyIssuer
		{
			get { return _ChiefAccountancyIssuer; }
			set { _ChiefAccountancyIssuer = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is image</value>
		public byte[] ChiefAccountancySign1
		{
			get { return _ChiefAccountancySign1; }
			set { _ChiefAccountancySign1 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is image</value>
		public byte[] ChiefAccountancySign2
		{
			get { return _ChiefAccountancySign2; }
			set { _ChiefAccountancySign2 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string CaProxyName
		{
			get { return _CaProxyName; }
			set { _CaProxyName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string CaProxyCI
		{
			get { return _CaProxyCI; }
			set { _CaProxyCI = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime CaProxyCD
		{
			get { return _CaProxyCD; }
			set { _CaProxyCD = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string CaProxyIssuer
		{
			get { return _CaProxyIssuer; }
			set { _CaProxyIssuer = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is image</value>
		public byte[] CaProxySign1
		{
			get { return _CaProxySign1; }
			set { _CaProxySign1 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is image</value>
		public byte[] CaProxySign2
		{
			get { return _CaProxySign2; }
			set { _CaProxySign2 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is image</value>
		public byte[] CompanySign1
		{
			get { return _CompanySign1; }
			set { _CompanySign1 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is image</value>
		public byte[] CompanySign2
		{
			get { return _CompanySign2; }
			set { _CompanySign2 = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string TradeCode
		{
			get { return _TradeCode; }
			set { _TradeCode = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int CustomerAccount
		{
			get { return _CustomerAccount; }
			set { _CustomerAccount = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string MobileSms
		{
			get { return _MobileSms; }
			set { _MobileSms = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is bit</value>
		public bool IsHere
		{
			get { return _IsHere; }
			set { _IsHere = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string MoneyDepositeNumber
		{
			get { return _MoneyDepositeNumber; }
			set { _MoneyDepositeNumber = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string MoneyDepositeLocation
		{
			get { return _MoneyDepositeLocation; }
			set { _MoneyDepositeLocation = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int DepartmentId
		{
			get { return _DepartmentId; }
			set { _DepartmentId = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is ntext</value>
		public string PublicCompanyManage
		{
			get { return _PublicCompanyManage; }
			set { _PublicCompanyManage = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is ntext</value>
		public string PublicCompanyHolder
		{
			get { return _PublicCompanyHolder; }
			set { _PublicCompanyHolder = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ParentCompanyName
		{
			get { return _ParentCompanyName; }
			set { _ParentCompanyName = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ParentCompanyAddress
		{
			get { return _ParentCompanyAddress; }
			set { _ParentCompanyAddress = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is nvarchar</value>
		public string ParentCompanyEmail
		{
			get { return _ParentCompanyEmail; }
			set { _ParentCompanyEmail = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is varchar</value>
		public string ParentCompanyTel
		{
			get { return _ParentCompanyTel; }
			set { _ParentCompanyTel = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smallint</value>
		public int PostType
		{
			get { return _PostType; }
			set { _PostType = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is smalldatetime</value>
		public DateTime ReOpenDate
		{
			get { return _ReOpenDate; }
			set { _ReOpenDate = value; }
		}
	
		/// <summary>
		/// 	
		/// </summary>
		/// <value>This type is int</value>
		public int UserTakeCared
		{
			get { return _UserTakeCared; }
			set { _UserTakeCared = value; }
		}
	
		
		#endregion
	}//End Class
	
	public enum CustomerColumns
	{
		BranchCode,
		ContractNumber,
		CustomerId,
		BrokerId,
		CustomerName,
		CustomerNameViet,
		CustomerType,
		DomesticForeign,
		Dob,
		Sex,
		SignatureImage1,
		SignatureImage2,
		OpenDate,
		CloseDate,
		CardType,
		CardIdentity,
		CardDate,
		CardIssuer,
		Address,
		AddressViet,
		Tel,
		Fax,
		Mobile,
		Mobile2,
		Email,
		UserCreate,
		DateCreate,
		UserModify,
		DateModify,
		ProxyStatus,
		AccountStatus,
		Notes,
		WorkingAddress,
		UserIntroduce,
		AttitudePoint,
		DepositPoint,
		ActionPoint,
		Country,
		Website,
		TaxCode,
		AccountType,
		OrderType,
		ReceiveReport,
		ReceiveReportBy,
		MarriageStatus,
		KnowledgeLevel,
		Job,
		OfficeFunction,
		OfficeTel,
		OfficeFax,
		HusbandWifeName,
		HusbandWifeCardNumber,
		HusbandWifeCardDate,
		HusbandWifeCardLocation,
		HusbandWifeTel,
		HusbandWifeEmail,
		JoinStockMarket,
		InvestKnowledge,
		InvestedIn,
		InvestTarget,
		RiskAccepted,
		InvestFund,
		DelegatePersonName,
		DelegatePersonFunction,
		DelegatePersonCardNumber,
		DelegateCardDate,
		DelegateCardLocation,
		DelegatePersonTel,
		DelegatePersonEmail,
		ChiefAccountancyName,
		ChiefAccountancyCI,
		ChiefAccountancyCD,
		ChiefAccountancyIssuer,
		ChiefAccountancySign1,
		ChiefAccountancySign2,
		CaProxyName,
		CaProxyCI,
		CaProxyCD,
		CaProxyIssuer,
		CaProxySign1,
		CaProxySign2,
		CompanySign1,
		CompanySign2,
		TradeCode,
		CustomerAccount,
		MobileSms,
		IsHere,
		MoneyDepositeNumber,
		MoneyDepositeLocation,
		DepartmentId,
		PublicCompanyManage,
		PublicCompanyHolder,
		ParentCompanyName,
		ParentCompanyAddress,
		ParentCompanyEmail,
		ParentCompanyTel,
		PostType,
		ReOpenDate,
		UserTakeCared
	}//End enum
}