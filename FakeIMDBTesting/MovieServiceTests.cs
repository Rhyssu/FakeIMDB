using Application.Implementations.Services;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Commons.Enums;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FakeIMDBTesting
{
    [TestClass]
    public class MovieServiceTests
    {
        private Mock<IMovieRepository>? _movieRepositoryMock;
        private Mock<ILogger<MovieService>>? _loggerMock;
        private Mock<IMovieService>? _movieServiceMock;
        private Mock<IMovieCache>? _movieCacheMock;
        private IMovieService? _serviceUnderTest;

        [TestInitialize]
        public void Initialize()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);

            _loggerMock = new Mock<ILogger<MovieService>>(MockBehavior.Strict);
            _loggerMock.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()));

            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            _movieCacheMock = new Mock<IMovieCache>(MockBehavior.Strict);

            _serviceUnderTest = new MovieService(_movieCacheMock.Object, _movieRepositoryMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetMovieByID_ShouldReturnMovieInfo()
        { 
            // Arrange
            var movieInfoCache = Enumerable.Empty<MovieInfoCache>().AsQueryable();
            _movieCacheMock.Setup(x => x.GetAllMovies()).Returns(movieInfoCache);
            _movieCacheMock.Setup(x => x.AddMovieToDatabaseAsync(
                It.IsAny<MovieInfoCache>(), 
                CancellationToken.None))
                .Returns(Task.CompletedTask);
            var movieToReturn = new MovieInfo() { Title = "Shrek" };
            _movieRepositoryMock.Setup(x => x.GetMovieByID(
                It.IsAny<string>(),
                It.IsAny<TypeOptions?>(),
                It.IsAny<int?>(),
                It.IsAny<PlotOptions>(),
                CancellationToken.None))
                .ReturnsAsync(movieToReturn);
            // Act 
            var movie = await _serviceUnderTest.GetMovieByID("tt0126029", null, null, PlotOptions.Short, CancellationToken.None);
            
            // Assert
            Assert.AreEqual(movieToReturn.Title, movie.Title);
        }

        [TestMethod]
        public async Task GetMovieByTitle_ShouldReturnMovieInfo()
        {
            // Arrange
            var movieInfoCache = Enumerable.Empty<MovieInfoCache>().AsQueryable();
            _movieCacheMock.Setup(x => x.GetAllMovies()).Returns(movieInfoCache);
            _movieCacheMock.Setup(x => x.AddMovieToDatabaseAsync(
                It.IsAny<MovieInfoCache>(),
                CancellationToken.None))
                .Returns(Task.CompletedTask);

            var movieToReturn = new MovieInfo() { Title = "Shrek" };
            _movieRepositoryMock.Setup(x => x.GetMovieByTitle(
                It.IsAny<string>(),
                It.IsAny<TypeOptions?>(),
                It.IsAny<int?>(),
                It.IsAny<PlotOptions>(),
                CancellationToken.None))
                .ReturnsAsync(movieToReturn);
            // Act
            var movie = await _serviceUnderTest.GetMovieByTitle("Shrek", null, null, PlotOptions.Short, CancellationToken.None);

            // Assert
            Assert.AreEqual(movieToReturn.Title, movie.Title);
        }

        [TestMethod]
        public async Task GetMovieList_ShouldReturnMovieList()
        {
            // Arrange
            var movieListCache = Enumerable.Empty<MovieListCache>().AsQueryable();
            _movieCacheMock.Setup(x => x.GetAllMoviesLists()).Returns(movieListCache);
            _movieCacheMock.Setup(x => x.AddMovieListToDatabaseAsync(
                It.IsAny<MovieListCache>(), 
                CancellationToken.None))
                .Returns(Task.CompletedTask);

            var movieListToReturn = new MovieList() { Search = new List<MovieShortInfo> { new MovieShortInfo() { Title = "Shrek" } } };
            _movieRepositoryMock.Setup(x => x.GetMovieListByTitle(
                It.IsAny<string>(),
                It.IsAny<TypeOptions?>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                CancellationToken.None))
                .ReturnsAsync(movieListToReturn);

            // Act
            var movie = await _serviceUnderTest.GetMovieListByTitle("Shrek", null, null, 1, CancellationToken.None);

            // Assert
            Assert.AreEqual(movieListToReturn.Search.FirstOrDefault().Title, movie.Search.FirstOrDefault().Title);
        }
    }
}
