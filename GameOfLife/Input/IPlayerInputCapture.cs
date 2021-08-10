using GameOfLife.Enums;

namespace GameOfLife.Input
{
    public interface IPlayerInputCapture
    {
        PlayerInput GetPlayersInput();
    }
}