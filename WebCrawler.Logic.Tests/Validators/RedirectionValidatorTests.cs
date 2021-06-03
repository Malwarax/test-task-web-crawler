using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebCrawler.Logic.Tests.Validators
{
    public class RedirectionValidatorTests
    {
        private readonly RedirectionValidator _validator;

        public RedirectionValidatorTests()
        {

            _validator = new RedirectionValidator();
        }

        [Fact]
        public void CheckRedirection_WithRedirection_ShouldReturnFalse()
        {
            //Act
            var result = _validator.CheckRedirection("http://google.com/");

            //Assert
            Assert.False(result.Result);
        }

        [Fact]
        public void CheckRedirection_WithRedirection_ShouldReturnMessage()
        {
            //Act
            var result = _validator.CheckRedirection("http://google.com/");

            //Assert
            Assert.Equal("Error. The server is redirecting the request for this url.", result.Message);
        }

        [Fact]
        public void CheckRedirection_WithoutConnection_ShouldReturnMessage()
        {
            //Act
            var result = _validator.CheckRedirection("https://qwertyqwerty.test");

            //Assert
            Assert.Equal("Error. No connection could be made.", result.Message);
        }

        [Fact]
        public void CheckRedirection_WithoutRedirection_ShouldReturnTrue()
        {
            //Act
            var result = _validator.CheckRedirection("https://www.google.com/");

            //Assert
            Assert.True(result.Result);
        }

        [Fact]
        public void CheckRedirection_WithoutRedirection_ShouldReturnEmptyMessage()
        {
            //Act
            var result = _validator.CheckRedirection("https://www.google.com/");

            //Assert
            Assert.Equal("", result.Message);
        }
    }
}
