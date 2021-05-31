using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebCrawler.ConsoleApplication.Tests
{
    public class UrlValidatorTests
    {
        private readonly Mock<ConsoleWrapper> _consoleMock;
        private readonly UrlValidator _validator;

        public UrlValidatorTests()
        {
            _consoleMock = new Mock<ConsoleWrapper>();
            _validator = new UrlValidator(_consoleMock.Object);
        }

        [Fact]
        public void CheckUrl_WithInvalidUrl_ShouldReturnFalse()
        {
            //Act
            var result = _validator.CheckUrl("example");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckUrl_WithInvalidUrl_ShouldPrintMessage()
        {
            //Act
            var result = _validator.CheckUrl("example");

            //Assert
            _consoleMock.Verify(c => c.WriteLine("Error. Invalid Url. The format of the Url could not be determined."));
        }

        [Fact]
        public void CheckUrl_WithInvalidUrlScheme_ShouldPrintMessage()
        {
            //Act
            var result = _validator.CheckUrl("example://www.google.com/");

            //Assert
            _consoleMock.Verify(c => c.WriteLine("Error. Invalid Url. The Url does not contain Http or Https scheme."));
        }

        [Fact]
        public void CheckUrl_CorrectUrl_ShouldReturnTrue()
        {
            //Act
            var result = _validator.CheckUrl("https://www.google.com/");

            //Assert
            Assert.True(result);
        }
    }
}
