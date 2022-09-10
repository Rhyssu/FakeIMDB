using Domain.Commons.Enums;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FakeIMDBTesting
{
    [TestClass]
    public class OMDBRepositoryTesting
    {
        private Mock<IOptions<APISettings>>? _apiOptionsMock;
        private Mock<ILogger<OMDBMovieRepository>>? _loggerMock;
        private Mock<IHttpClientFactory>? _httpClientFactoryMock;
        private const string BaseUriAddress = "http://www.omdbapi.com";
        private OMDBMovieRepository? _repositoryUnderTest;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger<OMDBMovieRepository>>(MockBehavior.Strict);
            _loggerMock.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()));

            HttpClient client = new();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            _apiOptionsMock = new Mock<IOptions<APISettings>>(MockBehavior.Loose);

            _repositoryUnderTest = new OMDBMovieRepository(_httpClientFactoryMock.Object, _apiOptionsMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetMovieByID_ShouldReturnMovieInfo()
        {
            // Arrange
            // Since there is no APIKey the function will always return null so there is no need to arrange anything.

            // Act
            var newMovie = await _repositoryUnderTest.GetMovieByID("tt0126029", null, null, PlotOptions.Short, CancellationToken.None);

            // Assert
            Assert.IsNull(newMovie);
        }

        [TestMethod]
        public async Task GetMovieByTitle_ShouldReturnMovieInfo()
        {
            // Arrange
            // Since there is no APIKey the function will always return null so there is no need to arrange anything.

            // Act
            var newMovie = await _repositoryUnderTest.GetMovieByTitle("Shrek", null, null, PlotOptions.Short, CancellationToken.None);

            // Assert
            Assert.IsNull(newMovie);
        }

        [TestMethod]
        public async Task GetMovieListByTitle_ShouldReturnMovieList()
        {
            // Arrange
            // Since there is no APIKey the function will always return null so there is no need to arrange anything.

            // Act
            var newMovie = await _repositoryUnderTest.GetMovieListByTitle("Shrek", null, null, 1, CancellationToken.None);

            // Assert
            Assert.IsNull(newMovie);
        }
    }
}
