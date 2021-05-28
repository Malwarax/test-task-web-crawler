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
        private DbWorker dbWorker;
        private Mock<IRepository<Website>> websiteRepositoryMock;
        private Mock<IRepository<PerformanceResult>> performanceResultRepositoryMock;
        private Uri url;

        public DbWorkerTests()
        {
            websiteRepositoryMock = new Mock<IRepository<Website>>();
            performanceResultRepositoryMock = new Mock<IRepository<PerformanceResult>>();
            dbWorker = new DbWorker(performanceResultRepositoryMock.Object, websiteRepositoryMock.Object);
            url = new Uri("https://www.example.com");
        }

        [Fact]
        public void SaveResult_WithNewWebsite_ShouldAddWebsite()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO() };

            //Act
            dbWorker.SaveResult(url, performanceResultModel);

            //Assert
            websiteRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Website>(),It.IsAny<CancellationToken>()),Times.Once);
        }
        
        [Fact]
        public void SaveResult_WithNewWebsite_ShouldAddResults()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO(),new PerformanceResultDTO(), new PerformanceResultDTO() };

            //Act
            dbWorker.SaveResult(url, performanceResultModel);

            //Assert
            performanceResultRepositoryMock.Verify(r => r.AddAsync(It.IsAny<PerformanceResult>(), It.IsAny<CancellationToken>()),Times.Exactly(3));
        }

        [Fact]
        public void SaveResult_WithExistingWebsite_ShouldUpdateResults()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO() { Link = "https://www.example.com/example/" } };
            var performanceResult = new PerformanceResult { Link = "https://www.example.com/example/"  };
            var website = new Website { WebsiteLink = url.AbsoluteUri, PerformanceResults = new List<PerformanceResult>() { performanceResult } };
            websiteRepositoryMock.Setup(w => w.GetAll()).Returns(new List<Website>() { website }.AsQueryable());

            //Act
            dbWorker.SaveResult(url, performanceResultModel);
        
            //Assert
            performanceResultRepositoryMock.Verify(r => r.Update(It.IsAny<PerformanceResult>()), Times.Once);
        }

        [Fact]
        public void SaveResult_WithExistingWebsite_ShouldAddResults()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO() { Link = "https://www.example.com/example/" } };
            var performanceResult = new PerformanceResult { Link = "https://www.example.com/example2/" };
            var website = new Website { WebsiteLink = url.AbsoluteUri, PerformanceResults = new List<PerformanceResult>() { performanceResult } };
            websiteRepositoryMock.Setup(w => w.GetAll()).Returns(new List<Website>() { website }.AsQueryable());

            //Act
            dbWorker.SaveResult(url, performanceResultModel);

            //Assert
            performanceResultRepositoryMock.Verify(r => r.AddAsync(It.IsAny<PerformanceResult>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
