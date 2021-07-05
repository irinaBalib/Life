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
            GameField = new Field(PlayersSetup.PlayersFieldSize);
            GameField.FillField();
            return GameField;
        }

        public void SetInitState()
        {
            int optionInput = PlayersSetup.PlayersStartOption;
            if (optionInput == 1)
            {
                GameField.SetRandomInitField();
            }
            else if (optionInput == 2)
            {
                GameField.SetPredefinedInitField();
            }
            else if (optionInput == 3)
            {
                GameField.ReadFromFile(PlayersSetup.PlayersName);
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
            bool canContinue = true;

                while (canContinue)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("|Controls| ESC - exit | SPACEBAR - pause |");
                  
                    GameField.ViewField();
                    Thread.Sleep(1000);
                    canContinue = !IsActionRequired();
                    
                    GameField.UpdateFieldData();
                }

        }

        public bool IsActionRequired()
        {
            if (IsGameOver())
            {
                return true;
            }
            else if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed;
                keyPressed = Console.ReadKey(true);
                Console.SetCursorPosition(0, 0);

                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game ended by the Player!                  ");
                    Thread.Sleep(2000);
                    Console.ResetColor();
                    return true; 
                }
                else if (keyPressed.Key == ConsoleKey.Spacebar)
                {
                    Console.WriteLine("**PAUSED. Press Spacebar to resume or Enter to save the game**             ");
                    do
                    {
                        keyPressed = Console.ReadKey(true);

                    } while (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Spacebar);

                    if (keyPressed.Key ==  ConsoleKey.Enter)
                    {
                        GameField.WriteToFile(PlayersSetup.PlayersName);
                    }
                }
            }
            return false;
        }

        public bool IsGameOver()
        {
            if (GameField.CountAliveCells() == 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No survivors for the next generation!          ");
                Thread.Sleep(3000);
                Console.ResetColor();
                return true;
            }
            return false;
        }
    }
}
