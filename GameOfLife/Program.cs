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

            bool inputIsValid = false;
            int dimension = 0;
            while (!inputIsValid)
            {
                Console.Write("Please input the size of the field (5-40 cells): ");
                bool isNumber = int.TryParse(Console.ReadLine(), out dimension);

                if (!isNumber)
                {
                    Console.WriteLine("Invalid input!");
                }
                else if( dimension < 5 || dimension > 40)
                {
                    Console.WriteLine("Size is out of range!");
                }
                else
                {
                    inputIsValid = true;
                }
            }

            Console.Clear();

            GameManager manager = new GameManager();
           
            manager.CreateField(dimension);
            // manager.SetInitState();
            manager.SetRandomInitField();
          //  Console.WriteLine("**Press ESC to exit**");
            
                manager.PutGameOn();
           
            Console.Clear();
            Console.WriteLine("GAME OVER");
            
        }

        
    }
}
