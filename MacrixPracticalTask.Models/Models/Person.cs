using System.ComponentModel.DataAnnotations;

namespace MacrixPracticalTask.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(30, ErrorMessage = "First name can't be longer than 30 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30, ErrorMessage = "Last name can't be longer than 30 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Street name is required")]
        [StringLength(30, ErrorMessage = "Street name can't be longer than 30 characters")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "House number is required")]
        [Range(1, 500)]
        public int HouseNumber { get; set; }

        public int? ApartmentNumber { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [Range(1, 1000000)]
        public int PostalCode { get; set; }

        [Required(ErrorMessage = "Town is required")]
        [StringLength(30, ErrorMessage = "Town name can't be longer than 30 characters")]
        public string Town { get; set; }

        [Required]
        [RegularExpression(@"^\+\d{1,}")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
    }
}
