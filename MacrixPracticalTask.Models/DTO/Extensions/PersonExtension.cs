using System;

namespace MacrixPracticalTask.Models.DTO.Extensions
{
    public static class PersonExtension
    {
        public static IEnumerable<PersonDTO> CalculateAge(this IEnumerable<PersonDTO> people)
        {
            var Today = DateTime.Today;

            foreach (var person in people)
            {
                person.Age = calculateAge(Today, person.DateOfBirth);
            }

            return people;
        }

        public static PersonDTO CalculateAge(this PersonDTO person)
        {
            var Today = DateTime.Today;

            person.Age = calculateAge(Today, person.DateOfBirth);

            return person;
        }

        private static int calculateAge(DateTime Today, DateTime DateOfBirth)
        {
            var personAge = Today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > Today.AddYears(-personAge))
            {
                personAge--;
            }

            return personAge;
        }
    }
}
