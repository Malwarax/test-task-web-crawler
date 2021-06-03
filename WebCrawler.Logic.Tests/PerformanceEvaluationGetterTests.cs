using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Linq;

namespace WebCrawler.Logic.Tests
{
    public class PerformanceEvaluationGetterTests
    {
        private readonly PerformanceEvaluationGetter _getter;
        private readonly Mock<PerformanceEvaluator> _performanceEvaluatorMock;

        public PerformanceEvaluationGetterTests()
        {
            _performanceEvaluatorMock = new Mock<PerformanceEvaluator>();
            _getter = new PerformanceEvaluationGetter(_performanceEvaluatorMock.Object);
        }

        [Fact]
        public void PrepareLinks_WithEmptyLinksList_ShouldReturnEmptyList()
        {
            //Act
            var result = _getter.PrepareLinks(new List<Uri>(), new List<Uri>());

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void PrepareLinks_WithOneLink_ShouldReturnResponceTimeResult()
        {
            //Arrange
            _performanceEvaluatorMock.Setup(p => p.GetResponceTime(It.IsAny<Uri>())).Returns(100);

            //Act
            var result = _getter.PrepareLinks(new List<Uri>() {new Uri("https://www.example.com/") }, new List<Uri>());

            //Assert
            Assert.Equal("https://www.example.com/", result[0].Link);
            Assert.Equal(100, result[0].ResponseTime);
        }

        [Fact]
        public void PrepareLinks_WithThreeLinks_ShouldReturnOrderedList()
        {
            //Arrange
            var link = new Uri("https://www.example.com/");
            _performanceEvaluatorMock.SetupSequence(p => p.GetResponceTime(It.IsAny<Uri>()))
                .Returns(300)
                .Returns(100)
                .Returns(200);

            //Act
            var result = _getter.PrepareLinks(new List<Uri>() { link,link,link }, new List<Uri>());

            //Assert
            Assert.Equal(result.OrderBy(r => r.ResponseTime), result);
        }

        [Fact]
        public void PrepareLinks_WithFourLinks_ShouldCombineLinks()
        {
            //Arrange
            var websiteUrls = new List<Uri>() { new Uri("https://www.example.com/example1/"), new Uri("https://www.example.com/") };
            var sitemapUrls = new List<Uri>() { new Uri("https://www.example.com/"), new Uri("https://www.example.com/example2/") };

            //Act
            var result = _getter.PrepareLinks(websiteUrls, sitemapUrls);

            //Assert
            Assert.Equal(3, result.Count);
            Assert.Contains("https://www.example.com/example1/", result[0].Link);
            Assert.Contains("https://www.example.com/", result[1].Link);
            Assert.Contains("https://www.example.com/example2/", result[2].Link);
        }

        [Fact]
        public void PrepareLinks_WithFourLinks_ShouldFindUniqueLinks()
        {
            //Arrange
            _performanceEvaluatorMock.Setup(p => p.GetResponceTime(It.IsAny<Uri>())).Returns(100);
            var websiteUrls = new  List<Uri>() { new Uri("https://www.example.com/example1/"), new Uri("https://www.example.com/") };
            var sitemapUrls = new List<Uri>() { new Uri("https://www.example.com/"), new Uri("https://www.example.com/example2/") };

            //Act
            var result = _getter.PrepareLinks(websiteUrls,sitemapUrls);

            //Assert
            Assert.Equal(3, result.Count);

            Assert.True(result[0].InWebsite);
            Assert.False(result[0].InSitemap);

            Assert.True(result[1].InWebsite);
            Assert.True(result[1].InSitemap);

            Assert.False(result[2].InWebsite);
            Assert.True(result[2].InSitemap);
        }


    }
}
