using System.ComponentModel.DataAnnotations;

namespace LAPTRINHWEB.Models
{
    public class Student
    {
        [Required(ErrorMessage = "MSSV là bắt buộc")]
        [Display(Name = "MSSV")]
        public string MSSV { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Điểm TB là bắt buộc")]
        [Display(Name = "Điểm TB")]
        [Range(0, 10, ErrorMessage = "Điểm TB phải từ 0 đến 10")]
        public double DiemTB { get; set; }

        [Required(ErrorMessage = "Chuyên ngành là bắt buộc")]
        [Display(Name = "Chuyên ngành")]
        public string ChuyenNganh { get; set; }
    }
}