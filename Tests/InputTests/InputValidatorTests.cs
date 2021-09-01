using Autofac.Extras.Moq;
using GameOfLife;
using GameOfLife.Constants;
using GameOfLife.Input;
using Moq;
using System;
using Xunit;

namespace Tests
{
    public class InputValidatorTests
    {
        private readonly InputValidator _validator;
        private readonly Mock<IApplication> _consoleAppMock = new Mock<IApplication>();
        public InputValidatorTests()
        {
            _validator = new InputValidator(_consoleAppMock.Object);
        }
        [Fact]
        public void GetValidatedName_ValidInputShouldBeReturned()
        {
            string fakeInput = "Irina";
            _consoleAppMock.Setup(x => x.ReadInput()).
                Returns(fakeInput);

            string output = _validator.GetValidatedName();

            Assert.Equal(fakeInput, output);
        }

        [Fact]
        public void GetValidatedName_ShouldFailAndNotifyOfBlankNameEntered()
        {
            string fakeInput = null;
            _consoleAppMock.Setup(x => x.ReadInput()).
                Returns(fakeInput); 
            
            Assert.Throws<NullReferenceException>(() => _validator.GetValidatedName());

            _consoleAppMock.Verify(x => x.ShowErrorMessage(TextMessages.BlankName), Times.Once);

           
        }

        [Fact]
        public void GetValidatedName_ShouldFailAndNotifyOfTooLongNameEntered()
        {
            string fakeInput = "asdfghjklzxcvbnmqwertyuiop";
            _consoleAppMock.Setup(x => x.ReadInput()).
                Returns(fakeInput);
            
            Assert.Throws<NullReferenceException>(() => _validator.GetValidatedName());

            _consoleAppMock.Verify(x => x.ShowErrorMessage(TextMessages.LongName));

            
        }
    }
}
