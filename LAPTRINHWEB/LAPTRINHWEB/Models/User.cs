using System.ComponentModel.DataAnnotations;

namespace LAPTRINHWEB.Models
{
    public class User
    {
        [Required(ErrorMessage = "Tên người dùng không được để trống")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên người dùng phải có độ dài từ 3 đến 50 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Email phải có độ dài tối thiểu là 6 ký tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài tối thiểu là 6 ký tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu phải khớp với mật khẩu")]
        public string ConfirmPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Tên người dùng không được để trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
    }
}