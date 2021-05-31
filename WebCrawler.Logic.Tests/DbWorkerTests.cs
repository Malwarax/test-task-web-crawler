using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Data;
using WebCrawler.Data;
using System.Threading;
using WebCrawler.EntityFramework;
using System.Linq;

namespace WebCrawler.Logic.Tests
{
    public class DbWorkerTests
    {
        private readonly DbWorker _dbWorker;
        private readonly Mock<IRepository<Test>> _websiteRepositoryMock;
        private readonly Mock<IRepository<PerformanceResult>> _performanceResultRepositoryMock;
        private readonly Uri _url;

        public DbWorkerTests()
        {
            _websiteRepositoryMock = new Mock<IRepository<Test>>();
            _performanceResultRepositoryMock = new Mock<IRepository<PerformanceResult>>();
            _dbWorker = new DbWorker(_performanceResultRepositoryMock.Object, _websiteRepositoryMock.Object);
            _url = new Uri("https://www.example.com");
        }

        [Fact]
        public void SaveResult_ShouldAddWebsite()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO() };

            //Act
            _dbWorker.SaveResult(_url, performanceResultModel);

            //Assert
            _websiteRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Test>(),It.IsAny<CancellationToken>()),Times.Once);
        }
        
        [Fact]
        public void SaveResult_ShouldAddResults()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO(),new PerformanceResultDTO(), new PerformanceResultDTO() };

            //Act
            _dbWorker.SaveResult(_url, performanceResultModel);

            //Assert
            _performanceResultRepositoryMock.Verify(r => r.AddAsync(It.IsAny<PerformanceResult>(), It.IsAny<CancellationToken>()), Times.Exactly(3));
        }
    }
}
