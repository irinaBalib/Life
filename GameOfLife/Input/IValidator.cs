using GameOfLife.Constants;
using GameOfLife.Enums;

namespace GameOfLife.Input
{
    public interface IValidator
    {
        TextMessages Message { get; set; }

        int ValidateDimension();
        string ValidateName();
        Option ValidateOption(string playerName);
    }
}