using GameOfLife.Constants;
using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Input
{
    public interface IValidator
    {
        int GetValidatedDimension();
        string GetValidatedName();
        Option GetValidatedOption(List<Option> listOfAvailableOptions);
        int GetValidatedIndex(List<int> indexes, int i);
    }
}