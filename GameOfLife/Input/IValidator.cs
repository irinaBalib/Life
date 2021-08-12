using GameOfLife.Constants;
using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Input
{
    public interface IValidator
    {
        int ValidateDimension();
        string ValidateName();
        Option ValidateOption(/*string playerName*/ List<Option> listOfAvailableOptions);
    }
}