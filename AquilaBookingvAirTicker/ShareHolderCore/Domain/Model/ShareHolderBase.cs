using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class ShareHolderBase
    {

        #region Variable Declarations
        private int _ShareHolderId = 0;
        private string _Name = string.Empty;
        private string _Address = string.Empty;
        private string _Address2 = string.Empty;
        private string _Phone = string.Empty;
        private string _Mobile = string.Empty;
        private string _SSN = string.Empty;
        private string _Nationality = string.Empty;
        private ShareHolderGroup _ShareHolderGroup = null;
        private int _ShareHolderCode = 0;
        private int _ShareHolderGroupId = 0;
        private DateTime _DOB = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private string _BirthPlace = string.Empty;
        private string _WorkPlace = string.Empty;
        private DateTime _IssueDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private string _IssuePlace = string.Empty;
        private string _ContactAddress = string.Empty;
        private string _RepTradingPerson = string.Empty;
        private string _RepSSN = string.Empty;
        private DateTime _RepIssueDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private string _RepIssuePlace = string.Empty;
        private string _Note = string.Empty;
        private string _Gender = string.Empty;
        private DateTime _StartDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private int _AddressCityId = 0;
        private int _AddressLocationId = 0;
        private int? _Address2CityId;
        private int? _Address2LocationId;
        private int? _BankId;
        private string _BankAccountNumber = string.Empty;
        private Guid? _UserId;
        private string _ShareHolderType;
        private Int32? _ParentShareHolderId;
        private IList<ShareHolder> _listShareHolder;
        #endregion

        #region Constructors
        public ShareHolderBase() { }

        public ShareHolderBase(
            int ShareHolderId,
            string Name,
            string Address,
      string Address2,
            string Phone,
            string Mobile,
            string SSN,
            string Nationality,
            ShareHolderGroup ShareHolderGroup,
            int ShareHolderCode,
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
            this._ShareHolderGroup = ShareHolderGroup;
            this._ShareHolderCode = ShareHolderCode;
            this._DOB = DOB;
            this._BirthPlace = BirthPlace;
            this._WorkPlace = WorkPlace;
            this._IssueDate = IssueDate;
            this._IssuePlace = IssuePlace;
            this._ContactAddress = ContactAddress;
            this._RepTradingPerson = RepTradingPerson;
            this._RepSSN = RepSSN;
            this._RepIssuePlace = RepIssuePlace;
            this._RepIssueDate = RepIssueDate;
            this._Note = Note;
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
        
        [Display(Name = "Tên cổ đông")]
        [Required(ErrorMessageResourceName = "nameHolder", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(255, ErrorMessageResourceName = "nameHolder", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 6)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>

        [Display(Name = "Địa chỉ thường trú")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        [Display(Name = "Địa chỉ liên lạc")]
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }

        [Display(Name = "Điện thoại")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Điện thoại di động")]
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Số CMND")]
        public string Ssn
        {
            get { return _SSN; }
            set { _SSN = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Quốc tịch")]
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }

        ///////////////////////////////////////

        [Display(Name = "Nhóm tài khoản")]
        [Required(ErrorMessageResourceName = "shareHolderGroupId", ErrorMessageResourceType = typeof(ValidationMessage))]
        public int ShareHolderGroupId
        {
            get { return _ShareHolderGroupId; }
            set { _ShareHolderGroupId = value; }
        }
        ////////////////////////////////////////
        [Display(Name = "Mã cổ đông")]
        [StringLength(50, ErrorMessageResourceName = "shareHolderCode", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 6)]
        public int ShareHolderCode
        {
            get { return _ShareHolderCode; }
            set { _ShareHolderCode = value; }
        }
        ////////////////////////////////////////
        [Display(Name = "Ngày sinh")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")] 
        public DateTime Dob
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        //////////////////////////////////////////
        [Display(Name = "Nơi sinh")]
        public string BirthPlace
        {
            get { return _BirthPlace; }
            set { _BirthPlace = value; }
        }
        //////////////////////////////////////////
        [Display(Name = "Nơi làm việc")]
        public string WorkPlace
        {
            get { return _WorkPlace; }
            set { _WorkPlace = value; }
        }
        ///////////////////////////////////////////
        [Display(Name = "Ngày cấp")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")] 
        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        ///////////////////////////////////////////
        [Display(Name = "Nơi cấp")]
        public string IssuePlace
        {
            get { return _IssuePlace; }
            set { _IssuePlace = value; }
        }
        ///////////////////////////////////////////
        [Display(Name = "Địa chỉ liên hệ người đại diện giao dịch")]
        public string ContactAddress
        {
            get { return _ContactAddress; }
            set { _ContactAddress = value; }
        }
        ////////////////////////////////////////////
        [Display(Name = "Người đại diện giao dịch")]
        public string RepTradingPerson
        {
            get { return _RepTradingPerson; }
            set { _RepTradingPerson = value; }
        }
        //////////////////////////////////////////////
        [Display(Name = "CMND người đại diện")]
        public string Repssn
        {
            get { return _RepSSN; }
            set { _RepSSN = value; }
        }
        /////////////////////////////////////////////
        [Display(Name = "Ngày cấp CMND người đại diện")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")] 
        public DateTime RepIssueDate
        {
            get { return _RepIssueDate; }
            set { _RepIssueDate = value; }
        }
        ////////////////////////////////////////////
        [Display(Name = "Nơi cấp CNMD người đại điện")]
        public string RepIssuePlace
        {
            get { return _RepIssuePlace; }
            set { _RepIssuePlace = value; }
        }
        ////////////////////////////////////////////
        [Display(Name = "Ghi chú")]
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        ////////////////////////////////////////////
        [Display(Name = "Giới tính")]
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        [Display(Name = "Ngày bắt đầu")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")] 
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Display(Name = "Tỉnh/Thành địa chỉ thường trú")]
        public int AddressCityId
        {
            get { return _AddressCityId; }
            set { _AddressCityId = value; }
        }
        [Display(Name = "Quận/Huyện địa chỉ thường trú")]
        public int AddressLocationId
        {
            get { return _AddressLocationId; }
            set { _AddressLocationId = value; }
        }
        [Display(Name = "Tỉnh/Thành địa chỉ tạm trú")]
        public int? Address2CityId
        {
            get { return _Address2CityId; }
            set { _Address2CityId = value; }
        }
        [Display(Name = "Quận/Huyện địa chỉ tạm trú")]
        public int? Address2LocationId
        {
            get { return _Address2LocationId; }
            set { _Address2LocationId = value; }
        }
        [Display(Name = "Id khoản ngân hàng")]
        public int? BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }
        [Display(Name = "Số tài khoản ngân hàng")]
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

        public string ShareHolderType
        {
            get{ return _ShareHolderType;}
            set{ _ShareHolderType = value;}
        }

        public Int32? ParentShareHolderId
        {
            get { return _ParentShareHolderId; }
            set { _ParentShareHolderId = value; }
            
        }

        public IList<ShareHolder> ListShareHolder
        {
            get { return _listShareHolder; }
            set
            {
                _listShareHolder = value;
            }
        }
        #endregion



        
    }//End Class
	
	public enum ShareHolderColumns
	{
		ShareHolderId,
		Name,
		Address,
  Address2,
		Phone,
		Mobile,
		SSN,
		Nationality,
        ShareHolderGroupId,
        ShareHolderCode,
        DOB,
        BirthPlace,
        WorkPlace,
        IssueDate,
        IssuePlace,
        ContactAddress,
        RepTradingPerson,
        RepSSN,
        RepIssueDate,
        RepIssuePlace,
        Note,
     Gender,
     StartDate,

     AddressCityId,
     AddressLocationId,
     Address2CityId,
     Address2LocationId,
     BankId,
     BankAccountNumber
	}//End enum
    //public enum OrderDirection
    //{
    //    ASC = 0,
    //    DESC = 1,
    //}
}