using GameOfLife.Enums;

namespace GameOfLife.Application
{
    
    public interface IKeyControls
    {
        bool KeyPressed();
        KeyAction GetKeyAction();
    }
}