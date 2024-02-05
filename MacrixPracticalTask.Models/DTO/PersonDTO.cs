namespace MacrixPracticalTask.Models.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public int PostalCode { get; set; }
        public string Town { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                var Today = DateTime.Today;

                var personAge = Today.Year - DateOfBirth.Year;

                if (DateOfBirth.Date > Today.AddYears(-personAge))
                {
                    personAge--;
                }

                return personAge;
            }
        }
    }
}
