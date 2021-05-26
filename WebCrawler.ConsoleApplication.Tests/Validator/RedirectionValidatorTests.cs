using Moq;
using System;
using Xunit;

namespace WebCrawler.ConsoleApplication.Tests
{
    public class RedirectionValidatorTests
    {
        private Mock<ConsoleWrapper> consoleMock;
        private RedirectionValidator validator;

        public RedirectionValidatorTests()
        {
            consoleMock = new Mock<ConsoleWrapper>();
            validator = new RedirectionValidator();
        }

        [Fact]
        public void CheckRedirection_WithRedirection_ShouldReturnFalse()
        {
            //Act
            var result = validator.CheckRedirection("http://google.com/", consoleMock.Object);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckRedirection_WithRedirection_ShouldPrintMessage()
        {
            //Act
            var result = validator.CheckRedirection("http://google.com/", consoleMock.Object);

            //Assert
            consoleMock.Verify(c => c.WriteLine("Error. The server is redirecting the request for this url."));
        }

        [Fact]
        public void CheckRedirection_WithoutRedirection_ShouldReturnTrue()
        {
            //Act
            var result = validator.CheckRedirection("https://www.google.com/", consoleMock.Object);

            //Assert
            Assert.True(result);
        }
    }
}
