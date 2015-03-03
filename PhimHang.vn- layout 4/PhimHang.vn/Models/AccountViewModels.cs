﻿using System.ComponentModel.DataAnnotations;

namespace PhimHang.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Tên đầy đủ")]
        [StringLength(64, ErrorMessage = "Tên đầy đủ từ {2} đến {1} ký tự.", MinimumLength = 6)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(64, ErrorMessage = "Tên đăng nhập từ {2} đến {1} ký tự.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ProfileUserViewModel
    {

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [System.Web.Mvc.AllowHtml]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Bạn nhập sai định dạng")]
        [Required(ErrorMessage="Vui lòng nhập tên của bạn")]
        [StringLength(100, ErrorMessage = "Tên của bạn phải từ 6 đến 100 ký tự", MinimumLength = 6)]
        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "Email từ 6 đến 100 ký tự", MinimumLength = 6)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        
        public System.DateTime? BirthDay { get; set; }

        
        [Display(Name = "Ngày tham gia")]
        public string CreatedDate { get; set; }

        [Display(Name = "Xác thực user")]
        public Verify Verify { get; set; }

        [System.Web.Mvc.AllowHtml]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Bạn nhập sai định dạng")]
        [StringLength(128, ErrorMessage="Tối đa 128 ký tự")]        
        public string Status { get; set; }

        [Display(Name = "Mobile")]
        [StringLength(16, ErrorMessage = "Số điện thoại phải ít nhất 10, lớn nhất 16 ký tự", MinimumLength = 10)]
        [RegularExpression("^[0-9]*$",ErrorMessage="Số điện thoại là số")]
        public string Mobile { get; set; }

        public string Avata { get; set; }        
        public byte? JobTitle { get; set; }

        [System.Web.Mvc.AllowHtml]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Bạn nhập sai định dạng")]
        [StringLength(256, ErrorMessage = "Tối đa 256 ký tự")]     
        public string URLFacebook { get; set; }

        [System.Web.Mvc.AllowHtml]
        [RegularExpression(@"[^<>]*", ErrorMessage = "Bạn nhập sai định dạng")]
        [StringLength(512, ErrorMessage = "Tối đa 512 ký tự")]
        public string CVInfo { get; set; }

        public byte? NumberExMarketYear { get; set; }
        public byte? PhilosophyMarket { get; set; }
    }
}
