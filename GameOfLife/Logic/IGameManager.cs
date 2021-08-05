using System;

namespace GameOfLife
{
    public interface IGameManager
    {
        void CreatePlayersSetup();
        void EndGame();
        void HasNoAliveCells();
        bool IsActionRequired();
        void PauseGame();
        void RunTheGame();
        void SaveGame();
        void SetInitFieldState();
        void ShiftFieldGenerations();
        bool RestartGame();
    }
}