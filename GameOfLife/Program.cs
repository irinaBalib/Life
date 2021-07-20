using Autofac;
using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetCursorPosition(Console.WindowWidth / 2-15, 0);  //remove   const - class
            //Console.WriteLine("Welcome to the Game of Life!\n");

            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IGameManager>();
                app.RunTheGame();
            }
          
        }

    }
}
