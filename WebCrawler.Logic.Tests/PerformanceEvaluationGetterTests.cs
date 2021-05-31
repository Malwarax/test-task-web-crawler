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
            var result = _getter.PrepareLinks(new List<Uri>());

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void PrepareLinks_WithOneLink_ShouldReturnResponceTimeResult()
        {
            //Arrange
            _performanceEvaluatorMock.Setup(p => p.GetResponceTime(It.IsAny<Uri>())).Returns(new TimeSpan(100));

            //Act
            var result = _getter.PrepareLinks(new List<Uri>() {new Uri("https://www.example.com/") });

            //Assert
            Assert.Equal("https://www.example.com/", result[0].Link);
            Assert.Equal(new TimeSpan(100), result[0].ResponseTime);
        }

        [Fact]
        public void PrepareLinks_WithThreeLinks_ShouldReturnOrderedList()
        {
            //Arrange
            var link = new Uri("https://www.example.com/");
            _performanceEvaluatorMock.SetupSequence(p => p.GetResponceTime(It.IsAny<Uri>()))
                .Returns(new TimeSpan(300))
                .Returns(new TimeSpan(100))
                .Returns(new TimeSpan(200));

            //Act
            var result = _getter.PrepareLinks(new List<Uri>() { link,link,link });

            //Assert
            Assert.Equal(result.OrderBy(r => r.ResponseTime), result);
        }
    }
}
