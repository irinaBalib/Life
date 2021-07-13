using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2-15, 0);  //remove   const - class
            Console.WriteLine("Welcome to the Game of Life!\n");
           
            GameManager manager = new GameManager();
           
            manager.RunTheGame();
        }

    }
}
