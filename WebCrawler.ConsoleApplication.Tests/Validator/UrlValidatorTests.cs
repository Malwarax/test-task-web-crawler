using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebCrawler.ConsoleApplication.Tests
{
    public class UrlValidatorTests
    {
        private Mock<ConsoleWrapper> consoleMock;
        private UrlValidator validator;

        public UrlValidatorTests()
        {
            consoleMock = new Mock<ConsoleWrapper>();
            validator = new UrlValidator();
        }

        [Fact]
        public void CheckUrl_WithInvalidUrl_ShouldReturnFalse()
        {
            //Act
            var result = validator.CheckUrl("example", consoleMock.Object);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckUrl_WithInvalidUrl_ShouldPrintMessage()
        {
            //Act
            var result = validator.CheckUrl("example", consoleMock.Object);

            //Assert
            consoleMock.Verify(c => c.WriteLine("Error. Invalid Url. The format of the Url could not be determined."));
        }

        [Fact]
        public void CheckUrl_WithInvalidUrlScheme_ShouldPrintMessage()
        {
            //Act
            var result = validator.CheckUrl("example://www.google.com/", consoleMock.Object);

            //Assert
            consoleMock.Verify(c => c.WriteLine("Error. Invalid Url. The Url does not contain Http or Https scheme."));
        }

        [Fact]
        public void CheckUrl_CorrectUrl_ShouldReturnTrue()
        {
            //Act
            var result = validator.CheckUrl("https://www.google.com/", consoleMock.Object);

            //Assert
            Assert.True(result);
        }
    }
}
