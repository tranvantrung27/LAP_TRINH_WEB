using System.ComponentModel.DataAnnotations;

namespace LAPTRINHWEB.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; } = "nhanvien";
    }
}
