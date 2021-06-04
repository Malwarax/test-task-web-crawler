using Moq;
using WebCrawler.Logic.Models;
using WebCrawler.Logic.Validators;
using Xunit;

namespace WebCrawler.Logic.Tests.Validators
{

    public class InputValidatorTests
    {
        private readonly Mock<UrlValidator> _urlValidatorMock;
        private readonly Mock<RedirectionValidator> _redirectionValidatorMock;
        private readonly InputValidator _inputValidator;

        public InputValidatorTests()
        {
            _urlValidatorMock = new Mock<UrlValidator>();
            _redirectionValidatorMock = new Mock<RedirectionValidator>();
            _inputValidator = new InputValidator(_urlValidatorMock.Object, _redirectionValidatorMock.Object);
        }

        [Fact]
        public void InputParameters_WithValidInput_ShouldReturnTrue()
        {
            //Arrange
            _urlValidatorMock.Setup(m => m.CheckUrl(It.IsAny<string>())).Returns(new ValidationResultModel { Result=true, Message=""});
            _redirectionValidatorMock.Setup(m => m.CheckRedirection(It.IsAny<string>())).Returns(new ValidationResultModel { Result = true, Message = "" });
            string errors;

            //Act
            bool result = _inputValidator.InputParameters("", out errors);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void InputParameters_WithInvalidInput_ShouldReturnFalse()
        {
            //Arrange
            _urlValidatorMock.Setup(m => m.CheckUrl(It.IsAny<string>())).Returns(new ValidationResultModel { Result = true, Message = "" });
            _redirectionValidatorMock.Setup(m => m.CheckRedirection(It.IsAny<string>())).Returns(new ValidationResultModel { Result = false, Message = "error" });
            string errors;

            //Act
            bool result = _inputValidator.InputParameters("", out errors);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void InputParameters_WithInvalidInput_ShouldReturnMessage()
        {
            //Arrange
            _urlValidatorMock.Setup(m => m.CheckUrl(It.IsAny<string>())).Returns(new ValidationResultModel { Result = true, Message = "" });
            _redirectionValidatorMock.Setup(m => m.CheckRedirection(It.IsAny<string>())).Returns(new ValidationResultModel { Result = false, Message = "error" });
            string errors;

            //Act
            bool result = _inputValidator.InputParameters("", out errors);

            //Assert
            Assert.Equal("error", errors);
        }
    }
}
