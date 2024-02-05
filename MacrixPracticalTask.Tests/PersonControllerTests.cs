using AutoMapper;
using MacrixPracticalTask.Context;
using MacrixPracticalTask.Controllers;
using MacrixPracticalTask.Logger.ILog;
using MacrixPracticalTask.Models.Paging;
using MacrixPracticalTask.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MacrixPracticalTask.Tests
{
    public class PersonControllerTests
    {
        private readonly Mock<ILoggerManager> _mockLogger;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly Mock<PersonParameters> _mockPersonParameters;
        private readonly PersonController _controller;
        public PersonControllerTests()
        {
            _mockLogger = new Mock<ILoggerManager>();
            _mockDbContext = new Mock<ApplicationDbContext>();
            _mockUnit = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockPersonParameters = new Mock<PersonParameters>();
            _controller = new PersonController(_mockLogger.Object, _mockUnit.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAll(_mockPersonParameters.Object);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        //[Fact]
        //public void GetAll_ActionExecutes_ReturnsOkResult()
        //{
        //    var result = _controller.GetAll(_mockPersonParameters.Object) as OkObjectResult;
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public void Get_WhenCalled_ReturnsOkResult()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //    .UseInMemoryDatabase(databaseName: "MovieListDatabase")
        //    .Options;

        //    // Insert seed data into the database using one instance of the context
        //    using (var context = new MovieDbContext(options))
        //    {
        //        context.Movies.Add(new Movie { Id = 1, Title = "Movie 1", YearOfRelease = 2018, Genre = "Action" });
        //        context.Movies.Add(new Movie { Id = 2, Title = "Movie 2", YearOfRelease = 2018, Genre = "Action" });
        //        context.Movies.Add(new Movie { Id = 3, Title = "Movie 3", YearOfRelease = 2019, Genre = "Action" });
        //        context.SaveChanges();
        //    }

        //    // Use a clean instance of the context to run the test
        //    using (var context = new MovieDbContext(options))
        //    {
        //        var sut = new MovieRepository(context);
        //        //Act
        //        var movies = sut.GetAll();

        //        //Assert
        //        Assert.Equal(3, movies.Count());
        //    }
        //}
    }

    public class personSeedDataFixture : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; private set; } = new ApplicationDbContext();

        public personSeedDataFixture()
        {
            ApplicationDbContext.People.Add(new Movie { Id = 1, Title = "Movie 1", YearOfRelease = 2018, Genre = "Action" });
            ApplicationDbContext.Movies.Add(new Movie { Id = 2, Title = "Movie 2", YearOfRelease = 2018, Genre = "Action" });
            ApplicationDbContext.Movies.Add(new Movie { Id = 3, Title = "Movie 3", YearOfRelease = 2019, Genre = "Action" });
            MovieContext.SaveChanges();
        }

        public void Dispose()
        {
            MovieContext.Dispose();
        }
    }
}