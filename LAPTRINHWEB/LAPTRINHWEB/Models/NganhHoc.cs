using System.ComponentModel.DataAnnotations.Schema;

namespace LAPTRINHWEB.Models
{
    [Table("NganhHoc")]  // Đảm bảo tên bảng khớp với tên bảng trong cơ sở dữ liệu
    public class NganhHoc
    {
        public string MaNganh { get; set; }
        public string TenNganh { get; set; }
    }

}
