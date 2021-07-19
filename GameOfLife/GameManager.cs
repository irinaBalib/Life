using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public class GameManager : IGameManager
    //public or private
    {
        //public Field GameField { get; set; }

        //public PlayersSetup PlayersSetup { get; set; }
        IField _field;
        IPlayerSetup _playerSetup;
        IDataStorage _dataStorage;

        public GameManager(IField field, IPlayerSetup playerSetup, IDataStorage dataStorage)
        {
            _field = field;
            _playerSetup = playerSetup;
            _dataStorage = dataStorage;
        }
        public void RunTheGame()
        {
            CreatePlayersSetup();

            if (_playerSetup.PlayerStartOption == 3)// enum?
            {
                RestoreSavedGame();
            }
            else
            {
                CreateField();
                SetInitState();
            }

            ShiftFieldGenerations();
            ShowPreExitScreen();

        }

        public void CreatePlayersSetup()
        {
            Console.WriteLine("PLAYER'S SETUP\n"); //sep class
            //_playerSetup = new _playerSetup();
            _playerSetup.SetPlayersInput();

            Console.Clear();
        }

        public void RestoreSavedGame()
        {
           // GameField = new Field();
            _dataStorage.Restore(_playerSetup.PlayerName);
        }

        public void CreateField()
        {
            // GameField = new Field(PlayersSetup.PlayersFieldSize);
            _field.FillField(_playerSetup.PlayerFieldSize);
        }

        public void SetInitState()
        {
            int optionInput = _playerSetup.PlayerStartOption;
            if (optionInput == 1)   //enum
            {
                _field.SetRandomInitField();
            }
            else if (optionInput == 2)
            {
                _field.SetPredefinedInitField();
            }
        }

        public void ShiftFieldGenerations()
        {
            bool canContinue = true;

            while (canContinue)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(" |Controls|  ESC - exit  | SPACEBAR - pause |                                  ");
                ViewFieldInfo();
                _field.ViewField();
                Thread.Sleep(1000);
                canContinue = !IsActionRequired();

                _field.UpdateFieldData();
            }
        }

        public void ViewFieldInfo()   // remove
        {
            Console.WriteLine(" Generation {0}       Live cells count: {1}", _field.Generation, _field.CountAliveCells());
        }
        public bool IsActionRequired()
        {
            if (HasNoAliveCells()) // verify if count == 0 in if()
            {
                return true;
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed;
                keyPressed = Console.ReadKey(true);
                Console.SetCursorPosition(0, 0);

                if (keyPressed.Key == ConsoleKey.Escape)  //switch case
                {
                    EndGame();
                    return true;
                }
                else if (keyPressed.Key == ConsoleKey.Spacebar)
                {
                    PauseGame(keyPressed);
                }
            }
            return false;
        }

        public bool HasNoAliveCells() //message only
        {
            if (_field.CountAliveCells() == 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("xxxxxxxxxx  TOTAL EXTINCTION  xxxxxxxxxxxxxxxxxxxxxx ");
                Thread.Sleep(3000);
                Console.ResetColor();
                return true;
            }
            return false;
        }

        public void EndGame()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" ~~~~~~~~~~~     Game ended by the Player! ~~~~~~~~~~~                 ");
            Thread.Sleep(2000);
            Console.ResetColor();
        }
        public void PauseGame(ConsoleKeyInfo keyPressed) //naming
        {
            Console.WriteLine("**PAUSED** Press SPACEBAR to resume or ENTER to save & exit"); //sep class

            do
            {
                keyPressed = Console.ReadKey(true);

            } while (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Spacebar);

            if (keyPressed.Key == ConsoleKey.Enter)
            {
                SaveGame();
            }
        }

        public void SaveGame()
        {
            _dataStorage.Save(_playerSetup.PlayerName, _field);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(" ~~~~~~~~~~~     Game for Player {0} saved. ~~~~~~~~~~~          ", _playerSetup.PlayerName);
            Thread.Sleep(3000);
            ShowPreExitScreen();
        }

        public void ShowPreExitScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 2);
            Console.WriteLine("GAME OVER");
            Console.WriteLine();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 2);
            Console.WriteLine("Press ENTER to start a new game");
            ConsoleKeyInfo keyPressed;
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                RunTheGame();
            }
            else
            {
                Console.Clear();
                Environment.Exit(0);
            }

        }
    }
}
