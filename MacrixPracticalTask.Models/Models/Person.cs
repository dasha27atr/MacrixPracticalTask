using System.ComponentModel.DataAnnotations;

namespace MacrixPracticalTask.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "First name can't be longer than 30 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Last name can't be longer than 30 characters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Street name can't be longer than 30 characters")]
        public string StreetName { get; set; }

        [Required]
        [Range(1, 500)]
        public int HouseNumber { get; set; }

        [Range(1, 1000)]
        public int ApartmentNumber { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int PostalCode { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Town name can't be longer than 30 characters")]
        public string Town { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        //[Required]
        //[Range(1, 150)]
        //public int Age { get; set; }
    }
}
