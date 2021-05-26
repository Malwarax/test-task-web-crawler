using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;

namespace WebCrawler.ConsoleApplication.Tests
{
    public class InputValidatorTests
    {
        private Mock<UrlValidator> ulrValidatorMock;
        private Mock<RedirectionValidator> redirectionValidatorMock;
        private InputValidator validator;

        public InputValidatorTests()
        {
            ulrValidatorMock = new Mock<UrlValidator>();
            redirectionValidatorMock = new Mock<RedirectionValidator>();
            validator = new InputValidator();
        }

        [Fact]
        public void Validate_WithInvalidUrl_ShouldReturnFalse()
        {
            //Arrange
            ulrValidatorMock.Setup(v => v.CheckUrl(It.IsAny<string>(), It.IsAny<ConsoleWrapper>())).Returns(false);

            //Act
            var result = validator.Validate("", ulrValidatorMock.Object, redirectionValidatorMock.Object);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithRedirection_ShouldReturnFalse()
        {
            //Arrange
            redirectionValidatorMock.Setup(v => v.CheckRedirection(It.IsAny<string>(), It.IsAny<ConsoleWrapper>())).Returns(false);

            //Act
            var result = validator.Validate("", ulrValidatorMock.Object, redirectionValidatorMock.Object);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithCorrectInput_ShouldReturnTrue()
        {
            //Arrange
            ulrValidatorMock.Setup(v => v.CheckUrl(It.IsAny<string>(), It.IsAny<ConsoleWrapper>())).Returns(true);
            redirectionValidatorMock.Setup(v => v.CheckRedirection(It.IsAny<string>(), It.IsAny<ConsoleWrapper>())).Returns(true);

            //Act
            var result = validator.Validate("", ulrValidatorMock.Object, redirectionValidatorMock.Object);

            //Assert
            Assert.True(result);
        }
    }
}
