using GameOfLife.Constants;
using GameOfLife.Enums;

namespace GameOfLife.Input
{
    public interface IValidator
    {
        int ValidateDimension();
        string ValidateName();
        Option ValidateOption(string playerName);
    }
}