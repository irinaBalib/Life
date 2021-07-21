using System;

namespace GameOfLife
{
    public enum Option
    {
        RANDOM = 1, PRESET, RESTORE
    }
    public interface IGameManager
    {
        void CreatePlayersSetup();
        void EndGame();
        void HasNoAliveCells();
        bool IsActionRequired();
        void PauseGame(ConsoleKeyInfo keyPressed); // TO IMPLEMENT
        void RunTheGame();
        void SaveGame();
        void SetInitFieldState();
        void ShiftFieldGenerations();
        void ShowPreExitScreen();
    }
}