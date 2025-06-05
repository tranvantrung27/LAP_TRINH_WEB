using LAPTRINHWEB.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SinhVien> SinhViens { get; set; }
    public DbSet<NganhHoc> NganhHocs { get; set; }
    public DbSet<HocPhan> HocPhans { get; set; }
    public DbSet<DangKy> DangKys { get; set; }
    public DbSet<ChiTietDangKy> ChiTietDangKys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<SinhVien>().ToTable("SinhVien");
        // Cấu hình khóa chính cho các bảng
        modelBuilder.Entity<NganhHoc>()
            .HasKey(nh => nh.MaNganh);  // Khóa chính là MaNganh

        modelBuilder.Entity<SinhVien>()
            .HasKey(sv => sv.MaSV);  // Khóa chính là MaSV

        modelBuilder.Entity<HocPhan>()
            .HasKey(hp => hp.MaHP);  // Khóa chính là MaHP

        modelBuilder.Entity<DangKy>()
            .HasKey(dk => dk.MaDK);  // Khóa chính là MaDK

        modelBuilder.Entity<ChiTietDangKy>()
                  .HasKey(c => new { c.MaDK, c.MaHP });
        modelBuilder.Entity<DangKy>()
        .HasOne(d => d.SinhVien)
        .WithMany()
        .HasForeignKey(d => d.MaSV)
        .OnDelete(DeleteBehavior.Cascade);  // Khi SinhVien bị xóa, DangKy sẽ tự động bị xóa

        // Các mối quan hệ khác tương tự nếu có
        modelBuilder.Entity<ChiTietDangKy>()
            .HasOne(cd => cd.DangKy)
            .WithMany()
            .HasForeignKey(cd => cd.MaDK)
            .OnDelete(DeleteBehavior.Cascade);

        // Cấu hình các mối quan hệ
        modelBuilder.Entity<SinhVien>()
            .HasOne(s => s.NganhHoc)
            .WithMany()
            .HasForeignKey(s => s.MaNganh);

        modelBuilder.Entity<DangKy>()
            .HasOne(d => d.SinhVien)
            .WithMany()
            .HasForeignKey(d => d.MaSV);

        modelBuilder.Entity<ChiTietDangKy>()
            .HasOne(cd => cd.DangKy)
            .WithMany()
            .HasForeignKey(cd => cd.MaDK);

        modelBuilder.Entity<ChiTietDangKy>()
            .HasOne(cd => cd.HocPhan)
            .WithMany()
            .HasForeignKey(cd => cd.MaHP);

        // Thêm dữ liệu mẫu (seed data)
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Cuộc sống" },
            new Category { CategoryId = 2, CategoryName = "Lập trình" },
            new Category { CategoryId = 3, CategoryName = "Sức Khỏe" }
        );

        // Seed Books
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "Cho tôi xin một vé đi tuổi thơ",
                Author = "Nguyễn Nhật Ánh",
                Price = 50000,
                Description = "Một cuốn sách hay về tuổi thơ và những kỷ niệm đẹp",
                Image = "books/tuoitho.jpg",
                CategoryId = 1
            },
            new Book
            {
                Id = 2,
                Title = "Lập trình C#",
                Author = "Tô Lê Xuân Việt",
                Price = 120000,
                Description = "Hướng dẫn lập trình C# từ cơ bản đến nâng cao",
                Image = null,
                CategoryId = 2
            }
        );
    }
}
