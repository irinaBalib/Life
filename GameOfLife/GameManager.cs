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
            int generation = 0;
            int liveCells = 0;
            bool gameEnded = false;
            Console.WriteLine("**Press ESC to exit**");
            
                while (!gameEnded)
                {
                    liveCells = GameField.CountAliveCells();
                    Console.WriteLine("Generation {0}, live cells count: {1}", generation, liveCells);
                        GameField.ViewField();

                        Thread.Sleep(1000);

                        GameField.UpdateFieldData();
                        Console.SetCursorPosition(0, 1);
                        generation++;
                    gameEnded = IsGameOver();
                }
        }
        public bool IsGameOver()
        {
            if (GameField.CountAliveCells() == 0)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No survivors for the next generation!");
                Thread.Sleep(2000);
                Console.ResetColor();
                return true;
            }
            else if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game ended by the Player!");
                Thread.Sleep(2000);
                Console.ResetColor();
                return true;
            }
            
            return false;
        }
    }
}
