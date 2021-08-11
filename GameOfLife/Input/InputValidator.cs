using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Input
{
    public class InputValidator : IValidator
    {
        IApplication _application;
        IGameStorage _storage;

        public InputValidator(IApplication application, IGameStorage storage)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }
        public string ValidateName()
        {
            var input = _application.ReadInput();

            while (string.IsNullOrEmpty(input))
            {
                _application.ShowErrorMessage(TextMessages.BlankName);

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
        public Option ValidateOption(string playerName)
        {
            var optionIndex = 0;
            var isOptionValid = false;

            while (!isOptionValid)
            {
                isOptionValid = (int.TryParse(_application.ReadInput(), out optionIndex))
                    && Enum.IsDefined(typeof(Option), optionIndex);
                   
                if (optionIndex == (int)Option.Restore && !_storage.DataExists(playerName))
                {
                    isOptionValid = false;
                }

                if (!isOptionValid)
                {
                    _application.ShowErrorMessage(TextMessages.InvalidInput);
                }
            }
            return (Option)optionIndex;
        }
    }
}
