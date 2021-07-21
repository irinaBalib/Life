namespace GameOfLife
{
    public interface IPlayerSetup
    {
        int PlayerFieldSize { get; set; }
        string PlayerName { get; set; }
        Option PlayerStartOption { get; set; }

        int GetValidatedDimensionInput();
        string GetValidatedNameInput();
        Option GetValidatedOptionInput();
        void SetPlayersInput();
    }
}