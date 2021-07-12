using System;
using System.Collections.Generic;
using System.IO;
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
            PlayersName = GetValidatedNameInput();   

            Console.WriteLine("Please choose game field set up for 0.Generation (1 - for randomly filled, 2 - pre-set, 3 - restore saved game): ");
            PlayersStartOption = GetValidatedOptionInput();

            if (PlayersStartOption == 3)  // if 3 - read from file
            {
                PlayersFieldSize = 0;
            }
            else
            {
                Console.WriteLine("Please input the size of the field (15-40 cells): "); //? need to impl to H&W input, not only square?
                PlayersFieldSize = GetValidatedDimensionInput();
            }
        }
        public string GetValidatedNameInput()
        {
            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                ClearLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Name is required!");
                Console.ResetColor();
                ReturnCursor();
                input = Console.ReadLine();
            }
            return input;
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
                    Console.Write("Invalid input!                             ");
                    ReturnCursor();
                }
                else if(option == 3)
                {
                    string savedGame = @$"C:\Users\irina.baliberdina\Documents\LifeSaved\{PlayersName}.dat";
                    if (!File.Exists(savedGame))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("No saved games found for this Player!");
                        isOptionValid = false;
                        ReturnCursor();
                    }
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
