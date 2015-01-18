using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]

        [Display(Name = "Ngày sinh")]
        public System.Nullable<System.DateTime> BirthDay { get; set; }

        
        [Display(Name = "Ngày tham gia")]
        public string CreatedDate { get; set; }

        [Display(Name = "Xác thực user")]
        public Verify Verify { get; set; }

        [StringLength(128)]
        public string Status { get; set; }
    }
}
