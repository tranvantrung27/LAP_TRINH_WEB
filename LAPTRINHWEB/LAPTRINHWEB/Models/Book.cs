using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LAPTRINHWEB.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        public string Author { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [StringLength(100)]
        public string? Image { get; set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }

        // Navigation property - thêm ValidateNever để bỏ qua validation
        [ValidateNever]
        public virtual Category Category { get; set; } = null!;
    }
}