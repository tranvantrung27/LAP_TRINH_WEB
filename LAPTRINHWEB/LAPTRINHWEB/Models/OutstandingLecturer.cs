using System.ComponentModel.DataAnnotations;

namespace LAPTRINHWEB.Models
{
    public class OutstandingLecturer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ và tên")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Chức vụ không được để trống")]
        [Display(Name = "Chức vụ")]
        [StringLength(100, ErrorMessage = "Chức vụ không được vượt quá 100 ký tự")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Bằng cấp không được để trống")]
        [Display(Name = "Bằng cấp")]
        [StringLength(200, ErrorMessage = "Bằng cấp không được vượt quá 200 ký tự")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Khoa không được để trống")]
        [Display(Name = "Khoa")]
        [StringLength(100, ErrorMessage = "Tên khoa không được vượt quá 100 ký tự")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        public string? Phone { get; set; }

        [Range(0, 50, ErrorMessage = "Số năm kinh nghiệm phải từ 0 đến 50")]
        [Display(Name = "Số năm kinh nghiệm")]
        public int YearsOfExperience { get; set; }

        [Display(Name = "Tiểu sử")]
        [StringLength(1000, ErrorMessage = "Tiểu sử không được vượt quá 1000 ký tự")]
        public string? Biography { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [Url(ErrorMessage = "URL ảnh không hợp lệ")]
        public string? AvatarUrl { get; set; }

        [Display(Name = "Thành tích")]
        public List<string> Achievements { get; set; } = new List<string>();

        [Display(Name = "Chuyên môn")]
        [StringLength(500, ErrorMessage = "Chuyên môn không được vượt quá 500 ký tự")]
        public string? Specialization { get; set; }

        [Display(Name = "Ngày bắt đầu công tác")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}