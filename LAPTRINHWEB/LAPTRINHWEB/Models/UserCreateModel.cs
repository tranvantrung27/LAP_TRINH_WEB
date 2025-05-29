using System.ComponentModel.DataAnnotations;

namespace LAPTRINHWEB.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "User";
    }
    public class UserCreateViewModel
    {
        [Required]
        public string Username { get; set; }
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
