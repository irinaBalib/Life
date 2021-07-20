using System;

namespace GameOfLife
{
    public interface IGameManager
    {
        void CreateField();
        void CreatePlayersSetup();
        void EndGame();
        void HasNoAliveCells();
        bool IsActionRequired();
        void PauseGame(ConsoleKeyInfo keyPressed); // TO IMPLEMENT
        void RestoreSavedGame();
        void RunTheGame();
        void SaveGame();
        void SetInitState();
        void ShiftFieldGenerations();
        void ShowPreExitScreen();
    }
}