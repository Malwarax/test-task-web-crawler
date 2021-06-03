using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebCrawler.Logic.Tests.Validators
{
    public class UrlValidatorTests
    {
        private readonly UrlValidator _validator;

        public UrlValidatorTests()
        {
            _validator = new UrlValidator();
        }

        [Fact]
        public void CheckUrl_WithInvalidUrl_ShouldReturnFalse()
        {
            //Act
            var result = _validator.CheckUrl("example");

            //Assert
            Assert.False(result.Result);
        }

        [Fact]
        public void CheckUrl_WithInvalidUrl_ShouldPrintMessage()
        {
            //Act
            var result = _validator.CheckUrl("example");

            //Assert
            Assert.Equal("Error. Invalid Url. The format of the Url could not be determined.", result.Message);
        }

        [Fact]
        public void CheckUrl_WithInvalidUrlScheme_ShouldPrintMessage()
        {
            //Act
            var result = _validator.CheckUrl("example://www.example.com/");

            //Assert
            Assert.Equal("Error. Invalid Url. The Url does not contain Http or Https scheme.", result.Message);
        }

        [Fact]
        public void CheckUrl_WithCorrectUrl_ShouldReturnTrue()
        {
            //Act
            var result = _validator.CheckUrl("https://www.example.com/");

            //Assert
            Assert.True(result.Result);
        }

        [Fact]
        public void CheckUrl_WithCorrectUrl_ShouldReturnEmptyMessage()
        {
            //Act
            var result = _validator.CheckUrl("https://www.example.com/");

            //Assert
            Assert.Equal("", result.Message);
        }
    }
}
