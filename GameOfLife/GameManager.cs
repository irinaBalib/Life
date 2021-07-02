using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class GameManager
    {
        public Field GameField { get; set; }

        public PlayersSetup PlayersSetup { get; private set; }
        public GameManager()
        {
        }

        public void CreatePlayersSetup()
        {
            PlayersSetup = new PlayersSetup();
            PlayersSetup.SetPlayersInput();
        }
        public Field CreateField()
        {
            GameField = new Field(PlayersSetup.FieldDimension);
            GameField.FillField();
            return GameField;
        }

        public void SetInitState()
        {
            int optionInput = PlayersSetup.Option;
            if (optionInput == 1)
            {
                GameField.SetRandomInitField();
            }
            else if (optionInput == 2)
            {
                GameField.SetPredefinedInitField();
            }
            else
            {
                Console.WriteLine("Option not found!");
            }
        }

        public void RunTheGame() 
        {
            CreateField();
            SetInitState();
            ShiftGenerations();
        }

        public void ShiftGenerations()
        {
            int g = 0;
            bool gameEnded = false;
            Console.WriteLine("**Press ESC to exit**");
            
                while (!gameEnded)
                {
                    Console.WriteLine("Generation {0}", g);
                        GameField.ViewField();

                        Thread.Sleep(1000);

                        GameField.UpdateFieldData();
                        Console.SetCursorPosition(0, 1);
                        g++;
                    gameEnded = IsGameOver();
                }
        }
        public bool IsGameOver()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;

            if (!GameField.HasAliveCells())
            {
                Console.WriteLine("No survivors for the next generation!");
                Thread.Sleep(1000);
                Console.ResetColor();
                return true;
            }
            else if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                Console.WriteLine("Game ended by the Player!");
                Thread.Sleep(1000);
                Console.ResetColor();
                return true;
            }
            
            return false;
        }
    }
}
