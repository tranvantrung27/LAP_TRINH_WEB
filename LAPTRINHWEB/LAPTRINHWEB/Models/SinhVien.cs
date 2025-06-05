using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LAPTRINHWEB.Models
{
    public class SinhVien
    {
        [Required]
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }

        [AllowNull]  // Bỏ yêu cầu bắt buộc với Hinh
        public string Hinh { get; set; }

        public string MaNganh { get; set; }

        // Mối quan hệ với NganhHoc
        public NganhHoc NganhHoc { get; set; }
    }

}
