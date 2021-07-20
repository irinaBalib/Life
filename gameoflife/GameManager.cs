using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public enum Option
    {
        RANDOM = 1, PRESET, RESTORE
    }
    public class GameManager : IGameManager
    {
        IField _field;
        IPlayerSetup _playerSetup;
        IDataStorage _dataStorage;
        IApplication _application;

        public GameManager(IField field, IPlayerSetup playerSetup, IDataStorage dataStorage, IApplication application)
        {
            _field = field;
            _playerSetup = playerSetup;
            _dataStorage = dataStorage;
            _application = application;
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
                _application.ShowFieldInfoBar(_field.Generation, _field.CountAliveCells());
                _field.ViewField();
                Thread.Sleep(1000);
                canContinue = !IsActionRequired();

                _field.UpdateFieldData();
            }
        }

        public bool IsActionRequired()
        {
            if (_field.CountAliveCells() == 0)  
            {
                HasNoAliveCells();
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

        public void HasNoAliveCells() 
        {
            string extinctionMessage = "xxxxxxxxxx  TOTAL EXTINCTION  xxxxxxxxxxxxxxxxxxxxxx ";
            ModifyInfoBar(extinctionMessage);
            Thread.Sleep(2000);
        }

        public void EndGame()
        {
            string endGameMessage = " ~~~~~~~~~~~     Game ended by the Player! ~~~~~~~~~~~";
            ModifyInfoBar(endGameMessage);
            Thread.Sleep(2000);
        }
        public void PauseGame(ConsoleKeyInfo keyPressed) //naming
        {
            string pauseMessage = "**PAUSED** Press SPACEBAR to resume or ENTER to save & exit";
            ModifyInfoBar(pauseMessage);
           
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

            string saveGameMessage = "~~~~~~~~~~~     Game for Player {0} saved. ~~~~~~~~~~~";
            ModifyInfoBar(saveGameMessage);
            Thread.Sleep(2000);
        }

        private void ModifyInfoBar(string message)
        {
            _application.ShowFieldInfoBar(_field.Generation, _field.CountAliveCells(), message);
        }
        public void ShowPreExitScreen()
        {
            _application.ShowPreExitScreen();
            RunTheGame();
        }
    }
}
