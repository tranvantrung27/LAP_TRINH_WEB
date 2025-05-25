using System.ComponentModel.DataAnnotations;

namespace LAPTRINHWEB.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}