namespace GameOfLife
{
    public interface IPlayerSetup
    {
        int PlayerFieldSize { get; set; }
        string PlayerName { get; set; }
        int PlayerStartOption { get; set; }

        int GetValidatedDimensionInput();
        string GetValidatedNameInput();
        int GetValidatedOptionInput();
        void SetPlayersInput();
    }
}