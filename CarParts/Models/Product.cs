using System.ComponentModel.DataAnnotations;

namespace CarParts.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10,000.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Product code is required.")]
        [StringLength(10, ErrorMessage = "Product code cannot exceed 10 characters.")]
        public string Code { get; set; }
    }
}

