using System;

namespace GameOfLife
{
    public interface IGameManager
    {
     //   Field GameField { get; set; }
       // PlayersSetup PlayersSetup { get; set; }

        void CreateField();
        void CreatePlayersSetup();
        void EndGame();
        bool HasNoAliveCells();
        bool IsActionRequired();
        void PauseGame(ConsoleKeyInfo keyPressed);
        void RestoreSavedGame();
        void RunTheGame();
        void SaveGame();
        void SetInitState();
        void ShiftFieldGenerations();
        void ShowPreExitScreen();
        void ViewFieldInfo();
    }
}