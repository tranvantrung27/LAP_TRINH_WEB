using System.ComponentModel.DataAnnotations.Schema;

namespace LAPTRINHWEB.Models
{

    [Table("DangKy")]
    public class DangKy
    {
        public int MaDK { get; set; }
        public DateTime NgayDK { get; set; }

        // Mối quan hệ với SinhVien (n-1)
        public string MaSV { get; set; }
        public SinhVien SinhVien { get; set; }

        // Mối quan hệ với ChiTietDangKy (1-n)
        public ICollection<ChiTietDangKy> ChiTietDangKys { get; set; }
    }
}
