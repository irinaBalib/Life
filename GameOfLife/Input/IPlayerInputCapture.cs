using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Input
{
    public interface IPlayerInputCapture
    {
        PlayerInput GetPlayersInput();
        List<int> GetPlayersFieldSelection();
    }
}