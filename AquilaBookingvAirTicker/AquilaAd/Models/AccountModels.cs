using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AquilaAd.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceName = "oldPasswordRequired", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        [StringLength(30, ErrorMessageResourceName = "oldPasswordRequired", ErrorMessageResourceType = typeof(Resources.ValidationMessage), MinimumLength = 8)]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "newPasswordRequired", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        [StringLength(30, ErrorMessageResourceName = "newPasswordRequired", ErrorMessageResourceType = typeof(Resources.ValidationMessage), MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "retypePasswordRequired", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu xác nhận")]
        [StringLength(30, ErrorMessageResourceName = "retypePasswordRequired", ErrorMessageResourceType = typeof(Resources.ValidationMessage), MinimumLength = 8)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu đăng nhập không trùng khớp.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessageResourceName = "logonName", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "password", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [StringLength(30, ErrorMessageResourceName = "passwordMinRequired", ErrorMessageResourceType = typeof(Resources.ValidationMessage), MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Nhớ tôi?")]
        public bool RememberMe { get; set; }

        [Display(Name = "Loại tài khoản")]
        public string MembershipType { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


    }

    public class ProfileModel
    {
        [Display(Name = "Tên đăng nhập")]
        public string LoginId { get; set; }

        [Display(Name = "Tên đầy đủ")]
        public string UserName { get; set; }


        [Required(ErrorMessageResourceName = "email", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Địa chỉ email")]
        [StringLength(100, ErrorMessage = "Địa chỉ email không thể trống và chứa tối thiểu 8 ký tự", MinimumLength = 8)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "email", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        public string Email { get; set; }

        [Display(Name = "Đã kích hoạt")]
        public bool IsLockedOut
        {
            get;
            set;
        }

        [Display(Name = "Lần đăng nhập cuối")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        public DateTime LastLoginDate
        { get; set; }
    }

    public class ResetPasswordModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "email", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        [Required(ErrorMessageResourceName = "email", ErrorMessageResourceType = typeof(Resources.ValidationMessage))]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Địa chỉ email")]
        [StringLength(30, ErrorMessage = "Địa chỉ email không hợp lệ, vui lònh nhập đúng địa chỉ email (abc@abc.com.vn)", MinimumLength = 8)]

        public string Email { get; set; }
    }
}