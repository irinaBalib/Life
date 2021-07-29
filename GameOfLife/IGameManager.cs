using System;

namespace GameOfLife
{
    public enum Option
    {
        RANDOM = 1, PRESET, RESTORE
    }
    public enum KeyAction
    {
        NoAction, Exit, PauseOnOff, SaveAndExit, Restart
    }
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