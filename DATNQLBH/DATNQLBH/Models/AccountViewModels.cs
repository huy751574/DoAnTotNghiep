using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DATNQLBH.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có độ dài từ {2} đến {1} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]

        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không giống nhau.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Họ Tên không được để trống.")]
        [StringLength(250, ErrorMessage = "Họ tên phải có độ dài từ {2} đến {1} ký tự.", MinimumLength = 6)]
        [Display(Name = "Họ tên đầy đủ")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Năm sinh không được để trống.")]
        [DataType(DataType.Date)]
        [Display(Name = "Năm Sinh")]
        public System.DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [Display(Name = "Số Điện Thoại")]
        [StringLength(12, ErrorMessage = "Số điện thoại không hợp lệ.", MinimumLength = 9)]
        [RegularExpression("^\\d+$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        [StringLength(500, ErrorMessage = "Địa chỉ không hợp lệ.", MinimumLength = 6)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
       
        public string MaCN { get; set; }

        public int? madangky { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class UserChange
    {
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [Display(Name = "Nhập lại Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Họ Tên không được để trống.")]
        [StringLength(250, ErrorMessage = "Họ tên phải có độ dài từ {0} đến {2} ký tự.", MinimumLength = 6)]
        [Display(Name = "Họ tên đầy đủ")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Năm sinh không được để trống.")]
        [DataType(DataType.Date)]
        [Display(Name = "Năm Sinh")]
        public System.DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        [StringLength(500, ErrorMessage = "địa chỉ phải có độ dài từ {0} đến {2} ký tự.", MinimumLength = 6)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }
    public class ChangeInfoUser
    {
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [Display(Name = "Nhập mật khẩu cũ")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [Display(Name = "Nhập lại Mật khẩu mới")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [Display(Name = "Nhập lại Mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
        [Required(ErrorMessage = "Họ Tên không được để trống.")]
        [StringLength(250, ErrorMessage = "Họ tên phải có độ dài từ {0} đến {2} ký tự.", MinimumLength = 6)]
        [Display(Name = "Họ tên đầy đủ")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Năm sinh không được để trống.")]
        [DataType(DataType.Date)]
        [Display(Name = "Năm Sinh")]
        public System.DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        [StringLength(500, ErrorMessage = "địa chỉ phải có độ dài từ {0} đến {2} ký tự.", MinimumLength = 6)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }
}
