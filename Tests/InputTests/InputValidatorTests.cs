using GameOfLife;
using GameOfLife.Input;
using System;
using Xunit;

namespace Tests
{
    public class InputValidatorTests
    {
        private readonly InputValidator _validator;
        private readonly ConsoleApplication _app;
        public InputValidatorTests()
        {
            _app = new ConsoleApplication();
            _validator = new InputValidator(_app);
        }
        [Fact]
        public void GetValidatedName_ValidInputShouldBeReturned()
        {
            //Arrange
            var input = "tester"; //?
            //Act
            var name = _validator.GetValidatedName();
            //Assert
            Assert.Equal(input, name);
        }
    }
}
