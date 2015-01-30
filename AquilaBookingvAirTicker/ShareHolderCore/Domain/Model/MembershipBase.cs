	
using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareHolderCore.Domain.Model
{
    [Serializable]
    public class MembershipBase
    {

        #region Variable Declarations
        protected Int32 _MemberId = 0;
        protected string _MemberName = string.Empty;
        protected string _Password = string.Empty;
        protected int _PasswordFormat = 0;
        protected string _PasswordSalt = string.Empty;
        protected string _MobilePIN = string.Empty;
        protected string _Email = string.Empty;
        protected string _PasswordQuestion = string.Empty;
        protected string _PasswordAnswer = string.Empty;
        protected bool _IsApproved = false;
        protected bool _IsLockedOut = false;
        protected DateTime _CreateDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected DateTime _LastLoginDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected DateTime _LastPasswordChangedDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected DateTime _LastLockoutDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected int _FailedPasswordAttemptCount = 0;
        protected DateTime _FailedPasswordAttemptWindowStart = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected int _FailedPasswordAnswerAttemptCount = 0;
        protected DateTime _FailedPasswordAnswerAttemptWindowStart = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        protected string _Comment = string.Empty;
        protected string _LoginId = string.Empty;                
        protected Int32 _LoginCount = 0;
        #endregion

        #region Constructors
        public MembershipBase() { }

        public MembershipBase(
            int MemberId,
            string MemberName,
            string Password,
            int PasswordFormat,
            string PasswordSalt,
            string MobilePIN,
            string Email,
            string PasswordQuestion,
            string PasswordAnswer,
            bool IsApproved,
            bool IsLockedOut,
            DateTime CreateDate,
            DateTime LastLoginDate,
            DateTime LastPasswordChangedDate,
            DateTime LastLockoutDate,
            int FailedPasswordAttemptCount,
            DateTime FailedPasswordAttemptWindowStart,
            int FailedPasswordAnswerAttemptCount,
            DateTime FailedPasswordAnswerAttemptWindowStart,
            string Comment,
            string LoginId)
        {
            this._MemberId = MemberId;
            this._MemberName = MemberName;
            this._Password = Password;
            this._PasswordFormat = PasswordFormat;
            this._PasswordSalt = PasswordSalt;
            this._MobilePIN = MobilePIN;
            this._Email = Email;
            this._PasswordQuestion = PasswordQuestion;
            this._PasswordAnswer = PasswordAnswer;
            this._IsApproved = IsApproved;
            this._IsLockedOut = IsLockedOut;
            this._CreateDate = CreateDate;
            this._LastLoginDate = LastLoginDate;
            this._LastPasswordChangedDate = LastPasswordChangedDate;
            this._LastLockoutDate = LastLockoutDate;
            this._FailedPasswordAttemptCount = FailedPasswordAttemptCount;
            this._FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart;
            this._FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount;
            this._FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart;
            this._Comment = Comment;
            this._LoginId = LoginId;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is uniqueidentifier</value>
        public virtual int MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }


        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessageResourceName = "userName", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(128, ErrorMessageResourceName = "userName", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 6)]
        public virtual string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessageResourceName = "password", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(128, ErrorMessageResourceName = "password", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 6)]
        public virtual string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is int</value>
        [Display(Name = "Định đạng mật khẩu")]

        public virtual int PasswordFormat
        {
            get { return _PasswordFormat; }
            set { _PasswordFormat = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "PasswordSalt")]
        public virtual string PasswordSalt
        {
            get { return _PasswordSalt; }
            set { _PasswordSalt = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Mã PIN mobile")]
        public virtual string MobilePIN
        {
            get { return _MobilePIN; }
            set { _MobilePIN = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Địa chỉ Email")]
        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Câu hỏi quên mật khẩu")]
        public virtual string PasswordQuestion
        {
            get { return _PasswordQuestion; }
            set { _PasswordQuestion = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Câu trả lời quên mật khẩu")]
        public virtual string PasswordAnswer
        {
            get { return _PasswordAnswer; }
            set { _PasswordAnswer = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is bit</value>
        [Display(Name = "Duyệt")]
        [Required(ErrorMessageResourceName = "IsApproved", ErrorMessageResourceType = typeof(ValidationMessage))]
        public virtual bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is bit</value>
        [Required(ErrorMessageResourceName = "isLockedOut", ErrorMessageResourceType = typeof(ValidationMessage))]
        [Display(Name = "Hoạt động")]
        public virtual bool IsLockedOut
        {
            get { return _IsLockedOut; }
            set { _IsLockedOut = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is date</value>
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        [Display(Name = "Ngày đăng nhập gần nhất")]
        [Required(ErrorMessageResourceName = "lastLoginDate", ErrorMessageResourceType = typeof(ValidationMessage))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        [Display(Name = "Ngày thây đổi mật khẩu gần nhất")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime LastPasswordChangedDate
        {
            get { return _LastPasswordChangedDate; }
            set { _LastPasswordChangedDate = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        [Display(Name = "Ngày khóa tài khoản")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime LastLockoutDate
        {
            get { return _LastLockoutDate; }
            set { _LastLockoutDate = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is int</value>
        [Display(Name = "FailedPasswordAttemptCount")]
        public virtual int FailedPasswordAttemptCount
        {
            get { return _FailedPasswordAttemptCount; }
            set { _FailedPasswordAttemptCount = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        [Display(Name = "FailedPasswordAttemptWindowStart")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime FailedPasswordAttemptWindowStart
        {
            get { return _FailedPasswordAttemptWindowStart; }
            set { _FailedPasswordAttemptWindowStart = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is int</value>
        [Display(Name = "FailedPasswordAnswerAttemptCount")]
        public virtual int FailedPasswordAnswerAttemptCount
        {
            get { return _FailedPasswordAnswerAttemptCount; }
            set { _FailedPasswordAnswerAttemptCount = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is datetime</value>
        [Display(Name = "FailedPasswordAnswerAttemptWindowStart")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime FailedPasswordAnswerAttemptWindowStart
        {
            get { return _FailedPasswordAnswerAttemptWindowStart; }
            set { _FailedPasswordAnswerAttemptWindowStart = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is ntext</value>
        [Display(Name = "Ghi chú")]
        public virtual string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        /// <summary>
        /// 	
        /// </summary>
        /// <value>This type is nvarchar</value>
        [Display(Name = "Mã đăng nhập")]
        [Required(ErrorMessageResourceName = "loginId", ErrorMessageResourceType = typeof(ValidationMessage))]
        [StringLength(128, ErrorMessageResourceName = "loginId", ErrorMessageResourceType = typeof(ValidationMessage), MinimumLength = 6)]
        public virtual string LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }

        

        [Display(Name = "Số lần đăng nhập")]
        public virtual Int32 LoginCount
        {
            get { return _LoginCount; }
            set { _LoginCount = value; }
        }

        public virtual IList<Booking> ListBooking
        {
            get;
            set;

        }

        public virtual IList<CompareList> ListCompareList
        {
            get;
            set;
        }
        public virtual string Type { get; set; }
        #endregion
    
    }


    //End Class
	
	public enum MembershipColumns
	{
		UserId,
		UserName,
		Password,
		PasswordFormat,
		PasswordSalt,
		MobilePIN,
		Email,
		PasswordQuestion,
		PasswordAnswer,
		IsApproved,
		IsLockedOut,
		CreateDate,
		LastLoginDate,
		LastPasswordChangedDate,
		LastLockoutDate,
		FailedPasswordAttemptCount,
		FailedPasswordAttemptWindowStart,
		FailedPasswordAnswerAttemptCount,
		FailedPasswordAnswerAttemptWindowStart,
		Comment,
		LoginId,
        Type
	}//End enum
}