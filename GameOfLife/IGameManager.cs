using System;

namespace GameOfLife
{
    public interface IGameManager
    {
        void CreateField();
        void CreatePlayersSetup();
        void EndGame();
        /*bool*/ void HasNoAliveCells();
        bool IsActionRequired();
        void PauseGame(ConsoleKeyInfo keyPressed);
        void RestoreSavedGame();
        void RunTheGame();
        void SaveGame();
        void SetInitState();
        void ShiftFieldGenerations();
        void ShowPreExitScreen();
       // void ViewFieldInfo();
    }
}