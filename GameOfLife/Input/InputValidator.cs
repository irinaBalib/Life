using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.Logic;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Input
{
    public class InputValidator : IValidator
    {
        IApplication _application;

        public InputValidator(IApplication application, IOptions options)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
        }
        public string ValidateName()
        {
            var input = _application.ReadInput();
            var nameIsValid = string.IsNullOrEmpty(input) && input.Length > NumericData.NameMaxLength;

            while (!nameIsValid)   
            {
                if (string.IsNullOrEmpty(input))
                {
                    _application.ShowErrorMessage(TextMessages.BlankName); 
                }
                else if (input.Length > NumericData.NameMaxLength)
                {
                    _application.ShowErrorMessage(TextMessages.LongName);
                }
                else
                {
                    nameIsValid = true;
                    break;
                }

                input = _application.ReadInput();
            }
            return input;
        }
        public int ValidateDimension()
        {
            var inputIsValid = false;
            var dimensionInput = 0;

            while (!inputIsValid)
            {
                if (!int.TryParse(_application.ReadInput(), out dimensionInput))
                {
                    _application.ShowErrorMessage(TextMessages.InvalidInput);
                }
                else if (dimensionInput < NumericData.FieldMinSize || dimensionInput > NumericData.FieldMaxSize)
                {
                    _application.ShowErrorMessage(TextMessages.OutOfRange);
                }
                else
                {
                    inputIsValid = true;
                }
            }
            return dimensionInput;
        }
        public Option ValidateOption(List<Option> listOfAvailableOptions)
        {
            var optionIndex = 0;
            var isOptionValid = false;

            while (!isOptionValid)
            {
                isOptionValid = (int.TryParse(_application.ReadInput(), out optionIndex))
                && listOfAvailableOptions.Exists(option => (int)option == optionIndex);
                
                if (!isOptionValid)
                {
                    _application.ShowErrorMessage(TextMessages.InvalidInput);
                }
            }
            return (Option)optionIndex;
        }

        public int GetValidatedIndex(List<int> indexes)
        {
            var inputIsValid = false;
            var indexInput = 0;

            while (!inputIsValid)
            {
                if (!int.TryParse(_application.ReadInput(), out indexInput))
                {
                    _application.ShowErrorMessage(TextMessages.InvalidInput);
                }
                else if (indexInput <= 0 || indexInput > NumericData.MultiFieldCount)
                {
                    _application.ShowErrorMessage(TextMessages.OutOfRange);
                }
                else if (indexes.Contains(indexInput))
                {
                    _application.ShowErrorMessage(TextMessages.Duplicate);
                }
                else
                {
                    inputIsValid = true;
                }
            }
            return indexInput;
        }
    }
}
