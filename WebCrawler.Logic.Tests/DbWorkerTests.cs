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
        private readonly Mock<IRepository<OnlySitemapUrl>> _onlySitemapUrlRepositoryMock;
        private readonly Mock<IRepository<OnlyWebsiteUrl>> _onlyWebsiteRepositoryMock;
        private readonly Uri _url;

        public DbWorkerTests()
        {
            _websiteRepositoryMock = new Mock<IRepository<Test>>();
            _performanceResultRepositoryMock = new Mock<IRepository<PerformanceResult>>();
            _onlySitemapUrlRepositoryMock = new Mock<IRepository<OnlySitemapUrl>>();
            _onlyWebsiteRepositoryMock = new Mock<IRepository<OnlyWebsiteUrl>>();
            _dbWorker = new DbWorker(_performanceResultRepositoryMock.Object, _websiteRepositoryMock.Object, _onlySitemapUrlRepositoryMock.Object, _onlyWebsiteRepositoryMock.Object);
            _url = new Uri("https://www.example.com");
        }

        [Fact]
        public void SaveResult_ShouldAddWebsite()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO() };
            var urlList = new List<Uri>();

            //Act
            _dbWorker.SaveResult(_url, performanceResultModel, urlList, urlList);

            //Assert
            _websiteRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Test>(),It.IsAny<CancellationToken>()),Times.Once);
        }
        
        [Fact]
        public void SaveResult_ShouldAddPerformanceResults()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO()};

            //Act
            _dbWorker.SaveResult(_url, performanceResultModel, new List<Uri>(), new List<Uri>());

            //Assert
            _performanceResultRepositoryMock.Verify(r => r.AddRange(It.IsAny<IEnumerable<PerformanceResult>>()), Times.Once);
        }

        [Fact]
        public void SaveResult_ShouldAddOnlySitemapUrls()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO(), new PerformanceResultDTO(), new PerformanceResultDTO() };
            var sitemapUrlsList = new List<Uri>() { new Uri("https://www.example.com/")};

            //Act
            _dbWorker.SaveResult(_url, performanceResultModel, sitemapUrlsList, new List<Uri>());

            //Assert
            _onlySitemapUrlRepositoryMock.Verify(r => r.AddRange(It.IsAny<IEnumerable<OnlySitemapUrl>>()), Times.Once);
        }

        [Fact]
        public void SaveResult_ShouldAddOnlyWebsiteUrls()
        {
            //Arrange
            var performanceResultModel = new List<PerformanceResultDTO>() { new PerformanceResultDTO(), new PerformanceResultDTO(), new PerformanceResultDTO() };
            var websiteUrlsList = new List<Uri>() { new Uri("https://www.example.com/") };

            //Act
            _dbWorker.SaveResult(_url, performanceResultModel, new List<Uri>(), websiteUrlsList);

            //Assert
            _onlyWebsiteRepositoryMock.Verify(r => r.AddRange(It.IsAny<IEnumerable<OnlyWebsiteUrl>>()), Times.Once);
        }
    }
}
