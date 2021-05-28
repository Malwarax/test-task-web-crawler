using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Data;
using WebCrawler.Data;
using System.Threading;
using WebCrawler.EntityFramework;

namespace WebCrawler.Logic.Tests
{
    public class DbWorkerTests
    {
        private DbWorker dbWorker;
        private Mock<IRepository<Website>> websiteRepositoryMock;
        private Mock<IRepository<PerformanceResult>> performanceResultRepositoryMock;

        public DbWorkerTests()
        {
            websiteRepositoryMock = new Mock<IRepository<Website>>();
            performanceResultRepositoryMock = new Mock<IRepository<PerformanceResult>>();
            dbWorker = new DbWorker(performanceResultRepositoryMock.Object, websiteRepositoryMock.Object);
        }

        [Fact]
        public void SaveResult_WithNewWebsite_ShouldSaveWebsite()
        {
            //Arrange
            var ulr = new Uri("https://www.example.com");
            var performanceResult = new List<PerformanceResultModel>() { new PerformanceResultModel() };

            //Act
            dbWorker.SaveResult(ulr, performanceResult);

            //Assert
            websiteRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Website>(),It.IsAny<CancellationToken>()),Times.Once);
        }
        
        [Fact]
        public void SaveResult_WithNewWebsite_ShouldAddNewResults()
        {
            //Arrange
            var ulr = new Uri("https://www.example.com");
            var performanceResult = new List<PerformanceResultModel>() { new PerformanceResultModel(),new PerformanceResultModel(), new PerformanceResultModel() };

            //Act
            dbWorker.SaveResult(ulr, performanceResult);

            //Assert
            performanceResultRepositoryMock.Verify(r => r.AddAsync(It.IsAny<PerformanceResult>(), It.IsAny<CancellationToken>()),Times.Exactly(3));
        }

        //[Fact]
        //public void SaveResult_WithExistingWebsite_ShouldUpdateResults()
        //{
        //    //Arrange
        //    var ulr = new Uri("https://www.example.com");
        //
        //    var performanceResult = new List<PerformanceResultModel>() { new PerformanceResultModel(), new PerformanceResultModel(), new PerformanceResultModel() };
        //
        //    //Act
        //    dbWorker.SaveResult(ulr, performanceResult);
        //
        //    //Assert
        //    performanceResultRepositoryMock.Verify(r => r.Update(It.IsAny<PerformanceResult>()), Times.Exactly(3));
        //}

        //[Fact]
        //public void SaveResult_WithExistingWebsite_ShouldAddNewResults()
        //{

        //}
    }
}
