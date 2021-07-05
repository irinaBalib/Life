using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class PlayersSetup
    {
        public string PlayersName { get; private set; }
        public int PlayersFieldSize { get; private set; }
        public int PlayersStartOption { get; private set; }
        public PlayersSetup()
        {
        }

        public void SetPlayersInput()
        {
            Console.WriteLine("Player's name: ");
            string playersName = Console.ReadLine();

            Console.WriteLine("Please input the size of the field (15-40 cells): "); //impl to any shape field, not only square?
            int dimension = GetValidatedDimensionInput();

            Console.WriteLine("Please choose game field set up for 0.Generation (1 - for randomly filled, 2 - pre-set): ");
            int option = GetValidatedOptionInput();

            PlayersName = playersName;
            PlayersFieldSize = dimension;
            PlayersStartOption = option;
        }

        public int GetValidatedDimensionInput()
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
        public int GetValidatedOptionInput()
        {
            int option = 0;
            bool isOptionValid = false;

            while (!isOptionValid)
            {
                isOptionValid = (int.TryParse(Console.ReadLine(), out option))
                    && option > 0 && option < 4;

                
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
    }
}
