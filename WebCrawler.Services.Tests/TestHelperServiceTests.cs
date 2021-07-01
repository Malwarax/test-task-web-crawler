using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebCrawler.Data;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;
using WebCrawler.Services.Exceptions;
using WebCrawler.Services.Interfaces;
using WebCrawler.Services.Models.Request;
using Xunit;

namespace WebCrawler.Services.Tests
{
    public class TestHelperServiceTests
    {

        private readonly Mock<DbWorker> _dbWorkerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICrawlerService> _crawlerServiceMock;
        private readonly Mock<InputValidator> _inputValidatorMock;
        private readonly TestHelperService _testHelperService;

        public TestHelperServiceTests()
        {
            _dbWorkerMock = new Mock<DbWorker>(new Mock<IRepository<PerformanceResult>>().Object, new Mock<IRepository<Test>>().Object);
            _mapperMock = new Mock<IMapper>();
            _crawlerServiceMock = new Mock<ICrawlerService>();
            _inputValidatorMock = new Mock<InputValidator>(new Mock<UrlValidator>().Object, new Mock<RedirectionValidator>().Object);
            _testHelperService = new TestHelperService(_dbWorkerMock.Object, _mapperMock.Object, _crawlerServiceMock.Object, _inputValidatorMock.Object);
        }

        [Fact]
        public async void CreateNewTest_WithInvalidUrl_ShouldThrowException()
        {
            //Arrange
            string outString = "Custom exception message";
            _inputValidatorMock.Setup(v => v.InputParameters(It.IsAny<string>(), out outString)).Returns(false);

            //Act
            var exception = await Assert.ThrowsAsync<UrlValidationException>(() => _testHelperService.CreateNewTest(new UserInputModel { Url = "" }));

            //Assert
            Assert.Equal(outString, exception.Message);
        }

        [Fact]
        public void GetTestResultsCountByTestId_WithInvalidId_ShouldThrowException()
        {
            //Arrange
            Test test = null;
            _dbWorkerMock.Setup(d => d.GetTestById(It.IsAny<int>())).Returns(test);

            //Act
            var exception = Assert.Throws<TestNotFoundException>(() => _testHelperService.GetTestResultsCountByTestId(0));

            //Assert
            Assert.Equal("Test not found", exception.Message);
        }

        [Fact]
        public void GetTestDetails_WithInvalidId_ShouldThrowException()
        {
            //Arrange
            Test test = null;
            _dbWorkerMock.Setup(d => d.GetTestById(It.IsAny<int>())).Returns(test);

            //Act
            var exception = Assert.Throws<TestNotFoundException>(() => _testHelperService.GetTestDetails(0, new RequestModel()));

            //Assert
            Assert.Equal("Test not found", exception.Message);
        }

        [Fact]
        public void FilterTestDetails_ShouldSupportCategoryFilter()
        {
            //Arrange
            var query = new List<PerformanceResult> { new PerformanceResult { InWebsite = true, InSitemap = false }, new PerformanceResult { InWebsite = false, InSitemap = true } }.AsQueryable();
            _dbWorkerMock.Setup(d => d.GetPerformanceResultsByTestId(It.IsAny<int>())).Returns(query);

            //Act
            var results = _testHelperService.FilterTestDetails(1, new RequestModel { InSitemap = true });

            //Assert
            Assert.Single(results);
            Assert.True(results[0].InSitemap);
        }
    }
}
