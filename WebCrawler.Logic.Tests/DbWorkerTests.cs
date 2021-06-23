using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Data;
using WebCrawler.Data;
using System.Threading;


namespace WebCrawler.Logic.Tests
{
    public class DbWorkerTests
    {
        private readonly DbWorker _dbWorker;
        private readonly Mock<IRepository<Test>> _testRepositoryMock;
        private readonly Mock<IRepository<PerformanceResult>> _performanceResultRepositoryMock;
        private readonly Uri _url;

        public DbWorkerTests()
        {
            _testRepositoryMock = new Mock<IRepository<Test>>();
            _performanceResultRepositoryMock = new Mock<IRepository<PerformanceResult>>();
            _dbWorker = new DbWorker(_performanceResultRepositoryMock.Object, _testRepositoryMock.Object);
            _url = new Uri("https://www.example.com");
        }

        [Fact]
        public void SaveResult_ShouldAddTest()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDto>() { new PerformanceResultDto() };

            //Act
            _dbWorker.SaveResult(_url, performanceResultModel).Wait();

            //Assert
            _testRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Test>(),It.IsAny<CancellationToken>()),Times.Once);
        }
        
        [Fact]
        public void SaveResult_ShouldAddPerformanceResults()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDto>() { new PerformanceResultDto()};

            //Act
            _dbWorker.SaveResult(_url, performanceResultModel).Wait();

            //Assert
            _performanceResultRepositoryMock.Verify(r => r.AddRange(It.IsAny<IEnumerable<PerformanceResult>>()), Times.Once);
        }
    }
}
