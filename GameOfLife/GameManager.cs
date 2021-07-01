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
        public void GetPSetpData()
        {
            Console.WriteLine(PlayersSetup.PlayersName);
            Console.WriteLine(PlayersSetup.Option);
            Console.WriteLine(PlayersSetup.FieldDimension);
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
            int g = 0;
            Console.WriteLine("**Press ESC to exit**");
            do
                {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine("Generation {0}", g);
                    GameField.ViewField();

                    Thread.Sleep(1000);
                    
                    GameField.UpdateFieldData();
                    Console.SetCursorPosition(0, 1);
                    g++;
                }

                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
            } while (!IsGameOver());
        }

        public bool IsGameOver()
        {
            if (GameField.HasAliveCells())
            {
                return false;
            }
            return true;
        }
    }
}
