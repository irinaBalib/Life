using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife
{
    public class PlayerSetup
    {
        public string PlayerName { get; set; }  
        public int PlayerFieldSize { get;  set; }
        public int PlayerStartOption { get;  set; }

        public void SetPlayersInput()
        {
            Console.WriteLine("Player's name: ");
            PlayerName = GetValidatedNameInput();   

            Console.WriteLine("Please choose game field set up for 0.Generation (1 - for randomly filled, 2 - pre-set, 3 - restore saved game): ");
            PlayerStartOption = GetValidatedOptionInput();
            
            if (PlayerStartOption != 3) // if 3 - read from file; enum for option
            {
                Console.WriteLine("Please input the size of the field (15-40 cells): "); //? need to impl to H&W input, not only square?
                PlayerFieldSize = GetValidatedDimensionInput();
            }
        }
        public string GetValidatedNameInput()
        {
            var input = Console.ReadLine();

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
            var inputIsValid = false;
            var dimensionInput = 0;

            while (!inputIsValid)
            {
                ClearLine();

                if (!int.TryParse(Console.ReadLine(), out dimensionInput))  
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid input! Please input numbers only.");
                    ReturnCursor();
                }
                else if (dimensionInput < 15 || dimensionInput > 40)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Size is out of range!                     ");
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
            var option = 0;
            var isOptionValid = false;

            while (!isOptionValid)
            {
                isOptionValid = (int.TryParse(Console.ReadLine(), out option))
                    && option > 0 && option < 4;        //enum

                if (!isOptionValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid input!                             ");
                    ReturnCursor();
                }

                if (option == 3)  // TO IMPLEMENT!!
                {
                    string savedGame = @$"C:\Users\irina.baliberdina\source\repos\irinaBalib\Life2\GameOfLife\SavedGames\{PlayerName}.json"; // to improve

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
        public static void ClearLine()  //sep class
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
