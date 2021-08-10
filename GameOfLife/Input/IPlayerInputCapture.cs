using GameOfLife.Enums;

namespace GameOfLife.Input
{
    public interface IPlayerInputCapture
    {
        //int FieldSizeInput { get; set; }
        //string PlayerName { get; set; }
        //Option StartOption { get; set; }

        PlayerInput GetPlayersInput();
    }
}