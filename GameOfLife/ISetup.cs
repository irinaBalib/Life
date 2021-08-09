using GameOfLife.Enums;

namespace GameOfLife
{
    public interface ISetup
    {
        int FieldSizeInput { get; set; }
      string PlayerName { get; set; }
        Option StartOption { get; set; }

        int GetValidatedDimensionInput();
        string GetValidatedNameInput();
        Option GetValidatedOptionInput();
        void SetPlayersInput();
    }
}