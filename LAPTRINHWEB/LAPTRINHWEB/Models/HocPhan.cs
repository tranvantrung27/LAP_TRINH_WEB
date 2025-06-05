namespace LAPTRINHWEB.Models
{
    public class HocPhan
    {
        public string MaHP { get; set; }
        public string TenHP { get; set; }
        public int SoTinChi { get; set; }

        // Mối quan hệ với ChiTietDangKy (n-n)
        public ICollection<ChiTietDangKy> ChiTietDangKys { get; set; }
    }
}
