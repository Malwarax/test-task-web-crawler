using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebCrawler.Logic.Tests
{
    public class PerformanceEvaluatorTests
    {
        [Fact]
        public void GetResponceTime_WithGoogleLink_ShouldReturnResponceTime()
        {
            //Arrange
            var link = new Uri("https://www.google.com/");
            var evaluator = new PerformanceEvaluator();

            //Act
            var result = evaluator.GetResponceTime(link).Milliseconds;

            //Assert
            Assert.InRange(result, 50, 1000);
        }
    }
}
