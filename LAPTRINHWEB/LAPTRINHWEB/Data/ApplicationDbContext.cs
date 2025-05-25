using Microsoft.EntityFrameworkCore;
using LAPTRINHWEB.Models;

namespace LAPTRINHWEB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(100);
            });

            // Configure Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Price).HasColumnType("decimal(18,0)");
                entity.Property(e => e.Image).HasMaxLength(100);

                // Configure relationship
                entity.HasOne(d => d.Category)
                      .WithMany(p => p.Books)
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
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
                    Image = null, // Không có ảnh, sẽ upload sau
                    CategoryId = 2
                },
                new Book
                {
                    Id = 3,
                    Title = "Core Java: Fundamentals, Volume 1",
                    Author = "Cay Horstmann",
                    Price = 800000,
                    Description = "Sách lập trình Java cơ bản và nâng cao",
                    Image = null, // Không có ảnh, sẽ upload sau
                    CategoryId = 2
                },
                new Book
                {
                    Id = 4,
                    Title = "Cuộc Sống Rất Giống Cuộc Đời",
                    Author = "Hải Dỗ",
                    Price = 61000,
                    Description = "Đàn ông tuổi 15 mơ ước thành đàn ông tuổi 20, đàn ông tuổi 20 mơ ước thành đàn ông tuổi 30, đàn ông tuổi 30 mơ ước được trở thành đàn ông tuổi 40 và đàn ông tuổi 40 lại mơ ước đặt chân lên cỗ máy thời gian để quay lại tuổi 30 với toàn bộ tài sản của mình! Vậy đây!",
                    Image = null, // Không có ảnh, sẽ upload sau
                    CategoryId = 1
                }
            );
        }
    }
}