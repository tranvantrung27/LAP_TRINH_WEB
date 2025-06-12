using Microsoft.EntityFrameworkCore;

namespace Bai6.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
       options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
