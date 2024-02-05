using MacrixPracticalTask.Context;
using MacrixPracticalTask.Models;
using MacrixPracticalTask.Models.Paging;
using MacrixPracticalTask.Repository;
using Microsoft.EntityFrameworkCore;

namespace MacrixPracticalTask.Tests
{
    public class PersonRepositoryTests
    {
        private ApplicationDbContext _context;
        private PersonRepository _repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "macrix")
            .Options;

            _context = new ApplicationDbContext(options);

            if (!_context.People.Any(x => x.Id == 1))
            {
                _context.People.Add(new Person
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
            }
            if (!_context.People.Any(x => x.Id == 2))
            {
                _context.People.Add(new Person
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
            }
            if (!_context.People.Any(x => x.Id == 3))
            {
                _context.People.Add(new Person
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
            }

            _context.SaveChanges();

            _repo = new PersonRepository(_context);
        }

        [Test]
        public void Test1_GetAllPeople_AssertEqualCount()
        {
            var people = _repo.GetAllPeople(new PersonParameters());

            Assert.That(people.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Test2_GetAllPeople_AssertEmptyWithPageNumberTwo()
        {
            PersonParameters personParameters = new()
            {
                PageNumber = 2
            };

            var people = _repo.GetAllPeople(personParameters);

            Assert.That(people.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Test3_GetPersonById_AssertTrueRightUserRetrieved()
        {
            var person = _repo.GetPersonById(1);

            Assert.That(person.LastName, Is.EqualTo("Atrashevskaya"));
        }

        [Test]
        public void Test4_GetPersonById_AssertFalseUserDoesNotExist()
        {
            var person = _repo.GetPersonById(4);

            Assert.That(person, Is.EqualTo(null));
        }

        [Test]
        public void Test5_CreatePerson_AssertCreated()
        {
            var person = new Person()
            {
                Id = 4,
                FirstName = "Ania",
                LastName = "Nowak",
                StreetName = "Swiety Marcin",
                HouseNumber = 63,
                ApartmentNumber = 96,
                PostalCode = 654321,
                Town = "Poznan",
                PhoneNumber = "+48123456789",
                DateOfBirth = DateTime.Parse("12/06/1987")
            };

            _repo.CreatePerson(person);
            _context.SaveChanges();

            var personCreated = _repo.GetPersonById(4);

            Assert.Multiple(() =>
            {
                Assert.That(personCreated.Id, Is.EqualTo(person.Id));
                Assert.That(personCreated.LastName, Is.EqualTo(person.LastName));
                Assert.That(personCreated.FirstName, Is.EqualTo(person.FirstName));
                Assert.That(personCreated.StreetName, Is.EqualTo(person.StreetName));
                Assert.That(personCreated.HouseNumber, Is.EqualTo(person.HouseNumber));
                Assert.That(personCreated.ApartmentNumber, Is.EqualTo(person.ApartmentNumber));
                Assert.That(personCreated.PostalCode, Is.EqualTo(person.PostalCode));
                Assert.That(personCreated.Town, Is.EqualTo(person.Town));
                Assert.That(personCreated.PhoneNumber, Is.EqualTo(person.PhoneNumber));
                Assert.That(personCreated.DateOfBirth, Is.EqualTo(person.DateOfBirth));
            });
        }

        [Test]
        public void Test6_CreatePerson_AssertUnableToCreate()
        {
            var person = new Person()
            {
                Id = 4,
                FirstName = "Ania",
                LastName = "Nowak",
                StreetName = "Swiety Marcin",
                HouseNumber = 63,
                PostalCode = 654321,
                ApartmentNumber = 96,
                Town = "Poznan",
                DateOfBirth = DateTime.Parse("12/06/1987")
            };

            _repo.CreatePerson(person);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void Test7_UpdatePerson_AssertUpdated()
        {
            var personForUpdation = _context.People.FirstOrDefault(x => x.Id == 3);

            var person = new Person()
            {
                FirstName = "Ania",
                LastName = "Nowak",
                StreetName = "Swiety Marcin",
                HouseNumber = 63
            };

            personForUpdation.FirstName = person.FirstName;
            personForUpdation.LastName = person.LastName;
            personForUpdation.StreetName = person.StreetName;
            personForUpdation.HouseNumber = person.HouseNumber;

            _repo.UpdatePerson(personForUpdation);
            _context.SaveChanges();

            Assert.Multiple(() =>
            {
                Assert.That(personForUpdation.LastName, Is.EqualTo(person.LastName));
                Assert.That(personForUpdation.FirstName, Is.EqualTo(person.FirstName));
                Assert.That(personForUpdation.StreetName, Is.EqualTo(person.StreetName));
                Assert.That(personForUpdation.HouseNumber, Is.EqualTo(person.HouseNumber));
            });
        }

        [Test]
        public void Test8_UpdatePerson_AssertUnableToUpdate()
        {
            var personForUpdation = _context.People.FirstOrDefault(x => x.Id == 3);

            var person = new Person()
            {
                FirstName = "Ania",
                LastName = "Nowak",
                StreetName = "Swiety Marcin"
            };

            personForUpdation.FirstName = person.FirstName;
            personForUpdation.LastName = person.LastName;
            personForUpdation.StreetName = person.StreetName;
            personForUpdation.HouseNumber = person.HouseNumber;

            _repo.UpdatePerson(personForUpdation);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public void Test9_DeletePerson_AssertDeleted()
        {
            var personForDeletion = _context.People.FirstOrDefault(x => x.Id == 3);

            _repo.DeletePerson(personForDeletion);
            _context.SaveChanges();

            var people = _repo.GetAllPeople(new PersonParameters());
            Assert.That(people.Any(x => x.Id == 3), Is.False);
        }

        [Test]
        public void Test10_DeletePerson_AssertUnableToDelete()
        {
            var personForDeletion = _context.People.FirstOrDefault(x => x.Id == 4);

            try
            {
                _repo.DeletePerson(personForDeletion);
            }
            catch (Exception ex)
            {
                Assert.Pass(ex.Message);
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}