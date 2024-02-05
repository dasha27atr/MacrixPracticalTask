using MacrixPracticalTask.Context;
using MacrixPracticalTask.Models;
using MacrixPracticalTask.Models.Paging;
using MacrixPracticalTask.Repository;
using Microsoft.EntityFrameworkCore;

namespace MacrixPracticalTask.Tests
{
    public class PersonRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _options;
        public PersonRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "macrix")
            .Options;

            using var context = new ApplicationDbContext(_options);
            context.People.Add(new Person
            {
                Id = 1,
                FirstName = "Daria",
                LastName = "Atrashevskaya",
                StreetName = "Panchenko",
                HouseNumber = 76,
                PostalCode = 220059,
                Town = "Minsk",
                PhoneNumber = "+375447047452",
                DateOfBirth = DateTime.Parse("27/04/2000")
            });
            context.People.Add(new Person
            {
                Id = 2,
                FirstName = "Karolina",
                LastName = "Sierszulska",
                StreetName = "Os. Lecha",
                HouseNumber = 58,
                ApartmentNumber = 8,
                PostalCode = 123456,
                Town = "Poznan",
                PhoneNumber = "+48123456789",
                DateOfBirth = DateTime.Parse("10/10/1975")
            });
            context.People.Add(new Person
            {
                Id = 3,
                FirstName = "Przemyslaw",
                LastName = "Franowski",
                StreetName = "Aleje Marcinkowskiego",
                HouseNumber = 121,
                ApartmentNumber = 96,
                PostalCode = 654321,
                Town = "Poznan",
                PhoneNumber = "+48123456789",
                DateOfBirth = DateTime.Parse("09/07/1985")
            });
            context.SaveChanges();

            var repo = new PersonRepository(context);
        }

        [Fact]
        public void GetAllPeople_AssertEqualCount()
        {
            using var context = new ApplicationDbContext(_options);
            var repo = new PersonRepository(context);

            var people = repo.GetAllPeople(new PersonParameters());

            Assert.Equal(3, people.Count());
        }

        [Fact]
        public void GetAllPeople_AssertEmptyWithPageNumberTwo()
        {
            using var context = new ApplicationDbContext(_options);
            var repo = new PersonRepository(context);

            PersonParameters personParameters = new()
            {
                PageNumber = 2
            };

            var people = repo.GetAllPeople(personParameters);

            Assert.Empty(people);
        }

        [Fact]
        public void GetPersonById_AssertEmptyWithPageNumberTwo()
        {
            using var context = new ApplicationDbContext(_options);
            var repo = new PersonRepository(context);

            var person = repo.GetPersonById(4);
        }
    }
}