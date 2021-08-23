using GameOfLife.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Application
{
    public class ConsoleKeyControls : IKeyControls
    {
        public bool KeyPressed()
        {
            return Console.KeyAvailable;
        }
        public KeyAction GetKeyAction()
        {
            ConsoleKeyInfo keyPressed;
            keyPressed = Console.ReadKey(true);
            switch (keyPressed.Key)
            {
                case ConsoleKey.Escape:
                    {
                        return KeyAction.Exit;
                    }
                case ConsoleKey.Spacebar:
                    {
                        return KeyAction.PauseOnOff;
                    }
                case ConsoleKey.F12:
                    {
                        return KeyAction.SaveAndExit;
                    }
                case ConsoleKey.F2:
                    {
                        return KeyAction.ChangeFieldSelection;
                    }
                case ConsoleKey.Enter:
                    {
                        return KeyAction.Restart;
                    }
                default:
                    {
                        return KeyAction.NoAction;
                    }
            }



        }
    }
}
