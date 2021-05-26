using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Linq;

namespace WebCrawler.Logic.Tests
{
    public class PerformanceEvaluationGetterTests
    {
        private PerformanceEvaluationGetter getter;
        private Mock<PerformanceEvaluator> performanceEvaluatorMock;

        public PerformanceEvaluationGetterTests()
        {
            getter = new PerformanceEvaluationGetter();
            performanceEvaluatorMock = new Mock<PerformanceEvaluator>();
        }

        [Fact]
        public void PrepareLinks_WithEmptyLinksList_ShouldReturnEmptyList()
        {
            //Act
            var result = getter.PrepareLinks(new List<Uri>(), performanceEvaluatorMock.Object);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void PrepareLinks_WithOneLink_ShouldReturnResponceTimeResult()
        {
            //Arrange
            performanceEvaluatorMock.Setup(p => p.GetResponceTime(It.IsAny<Uri>())).Returns(new TimeSpan(100));

            //Act
            var result = getter.PrepareLinks(new List<Uri>() {new Uri("https://www.example.com/") },performanceEvaluatorMock.Object);

            //Assert
            Assert.Equal("https://www.example.com/", result[0].Link);
            Assert.Equal(new TimeSpan(100), result[0].ResponseTime);
        }

        [Fact]
        public void PrepareLinks_WithThreeLinks_ShouldReturnOrderedList()
        {
            //Arrange
            var link = new Uri("https://www.example.com/");
            performanceEvaluatorMock.SetupSequence(p => p.GetResponceTime(It.IsAny<Uri>()))
                .Returns(new TimeSpan(300))
                .Returns(new TimeSpan(100))
                .Returns(new TimeSpan(200));

            //Act
            var result = getter.PrepareLinks(new List<Uri>() { link,link,link }, performanceEvaluatorMock.Object);

            //Assert
            Assert.Equal(result.OrderBy(r => r.ResponseTime), result);
        }
    }
}
