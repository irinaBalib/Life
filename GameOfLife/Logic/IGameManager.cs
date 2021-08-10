using System;

namespace GameOfLife
{
    public interface IGameManager
    {
        void CreatePlayerSetup();
        void EndGame();
        void NotifyOfExtinction();
        bool IsActionRequired();
        void PauseGame();
        void RunTheGame();
        void SaveGame();
        void GetGameField();
        void ShiftFieldGenerations();
        bool RestartGame();
    }
}