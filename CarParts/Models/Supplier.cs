using System.ComponentModel.DataAnnotations;

namespace CarParts.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Supplier name is required.")]
        [StringLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact information is required.")]
        [StringLength(200, ErrorMessage = "Contact information cannot exceed 200 characters.")]
        public string ContactInfo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
    }
}
