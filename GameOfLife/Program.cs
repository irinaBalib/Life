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

            Console.WriteLine("Please input the size of the field (15-40 cells): ");
            int dimension = GetValidatedDimensionInput();

            Console.WriteLine("Please choose game field set up for 0.Generation (1 - for randomly filled, 2 - pre-set): ");
            int optionInput = GetValidatedOptionInput();

            GameManager manager = new GameManager();
           
            manager.CreateField(dimension);
            manager.SetInitState(optionInput);

            Console.Clear();
            Console.WriteLine("**Press ESC to exit**");
       
            manager.PutGameOn();
           
            Console.Clear();
            Console.WriteLine("GAME OVER");
            
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        public static void ReturnCursor()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
        public static int GetValidatedDimensionInput()
        {
            bool inputIsValid = false;
            int dimensionInput = 0;

            while (!inputIsValid)
            {
                bool isNumber = int.TryParse(Console.ReadLine(), out dimensionInput);

                ClearLine();

                if (!isNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid input! Please input numbers only.");
                    ReturnCursor();

                }
                else if (dimensionInput < 15 || dimensionInput > 40)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Size is out of range!");
                    ReturnCursor();
                }
                else
                {
                    inputIsValid = true;
                }
                Console.ResetColor();
            }
            return dimensionInput;
        }

        public static int GetValidatedOptionInput()
        {
           int option = 0;
           bool isOptionValid = false;
             
            while (!isOptionValid)
            {
                isOptionValid = (int.TryParse(Console.ReadLine(), out option))
                    && option > 0 && option < 3;

                if (!isOptionValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid input!");
                    ReturnCursor();
                }
                Console.ResetColor();
            }
            return option;
        }
    }
}
