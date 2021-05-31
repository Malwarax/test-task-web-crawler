using Moq;
using System;
using Xunit;

namespace WebCrawler.ConsoleApplication.Tests
{
    public class RedirectionValidatorTests
    {
        private readonly Mock<ConsoleWrapper> _consoleMock;
        private readonly RedirectionValidator _validator;

        public RedirectionValidatorTests()
        {
            _consoleMock = new Mock<ConsoleWrapper>();
            _validator = new RedirectionValidator(_consoleMock.Object);
        }

        [Fact]
        public void CheckRedirection_WithRedirection_ShouldReturnFalse()
        {
            //Act
            var result = _validator.CheckRedirection("http://google.com/");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckRedirection_WithRedirection_ShouldPrintMessage()
        {
            //Act
            var result = _validator.CheckRedirection("http://google.com/");

            //Assert
            _consoleMock.Verify(c => c.WriteLine("Error. The server is redirecting the request for this url."));
        }

        [Fact]
        public void CheckRedirection_WithoutRedirection_ShouldReturnTrue()
        {
            //Act
            var result = _validator.CheckRedirection("https://www.google.com/");

            //Assert
            Assert.True(result);
        }
    }
}
