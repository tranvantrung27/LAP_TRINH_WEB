using System.ComponentModel.DataAnnotations.Schema;

namespace LAPTRINHWEB.Models
{
    [Table("ChiTietDangKy")]
    public class ChiTietDangKy
    {
        public int MaDK { get; set; }
        public string MaHP { get; set; }

        public DangKy DangKy { get; set; }
        public HocPhan HocPhan { get; set; }
    }
}
