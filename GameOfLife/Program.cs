using System;

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
                Console.Write("Please input the size of the field (5-20 cells): ");
                bool isNumber = int.TryParse(Console.ReadLine(), out dimension);

                if (!isNumber)
                {
                    Console.WriteLine("Invalid input!");
                }
                else if( dimension < 5 || dimension > 20)
                {
                    Console.WriteLine("Size is out of range!");
                }
                else
                {
                    inputIsValid = true;
                }
            }

            GameManager manager = new GameManager();


            manager.ViewField(manager.CreateField(dimension));
        }

        
    }
}
