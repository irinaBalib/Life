using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Game of Life!");
            Console.WriteLine();

            GameManager manager = new GameManager();

            manager.CreatePlayersSetup();
            Console.Clear();
            manager.RunTheGame();
           
            Console.Clear();
            Console.WriteLine("GAME OVER");
            
        }

    }
}
