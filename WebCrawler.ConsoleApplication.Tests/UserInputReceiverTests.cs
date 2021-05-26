using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication.Tests
{

    public class UserInputReceiverTests
    {
        private Mock<ConsoleWrapper> consoleMock;
        private Mock<InputValidator> validatorMock;

        public UserInputReceiverTests()
        {
            consoleMock = new Mock<ConsoleWrapper>();
            validatorMock = new Mock<InputValidator>();
        }

        [Fact]
        public void GetUserInput_WithEmptyInput_ShouldReturnInitialMessage()
        {
            //Arrange
            consoleMock.Setup(c => c.ReadLine()).Returns("");
            var receiver = new UserInputReceiver(consoleMock.Object, validatorMock.Object);

            //Act
            receiver.GetUserInput();

            //Assert
            consoleMock.Verify(c => c.WriteLine("Enter the website url (e.g. https://www.example.com/):"));
        }

        [Fact]
        public void GetUserInput_WithInvalidInput_ShouldReturnFalse()
        {
            //Arrange
            consoleMock.Setup(c => c.ReadLine()).Returns("");
            validatorMock.Setup(v => v.Validate(It.IsAny<string>(), It.IsAny<UrlValidator>(), It.IsAny<RedirectionValidator>())).Returns(false);
            var receiver = new UserInputReceiver(consoleMock.Object, validatorMock.Object);

            //Act
            var result=receiver.GetUserInput();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetUserInput_WithCorrectInput_ShouldReturnTrue()
        {
            //Arrange
            consoleMock.Setup(c => c.ReadLine()).Returns("http://www.example.com/");
            validatorMock.Setup(v => v.Validate(It.IsAny<string>(), It.IsAny<UrlValidator>(), It.IsAny<RedirectionValidator>())).Returns(true);
            var receiver = new UserInputReceiver(consoleMock.Object, validatorMock.Object);

            //Act
            var result = receiver.GetUserInput();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GetUserInput_WithCorrectInput_ShouldReturnUri()
        {
            //Arrange
            consoleMock.Setup(c => c.ReadLine()).Returns("http://www.example.com/");
            validatorMock.Setup(v => v.Validate(It.IsAny<string>(), It.IsAny<UrlValidator>(), It.IsAny<RedirectionValidator>())).Returns(true);
            var receiver = new UserInputReceiver(consoleMock.Object, validatorMock.Object);

            //Act
            receiver.GetUserInput();

            //Assert
            Assert.Equal(new Uri("http://www.example.com/"),receiver.WebsiteUrl);
        }
    }
}
