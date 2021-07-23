using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace GameOfLife
{
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
            bool gameContinues;
            do
            { 
                CreatePlayersSetup();
                SetInitFieldState();
                ShiftFieldGenerations();
                gameContinues = RestartGame();
            } while (gameContinues);
           
        }

        public void CreatePlayersSetup()
        {
            _playerSetup.SetPlayersInput();

            _application.ClearScreen();  
        }

        public void SetInitFieldState()
        {
            switch (_playerSetup.PlayerStartOption)
            {
                case Option.RANDOM:
                    {
                        _field.Create(_playerSetup.PlayerFieldSize);  // call Create in init methods?
                        _field.SetRandomInitField();
                        break;
                    }
                case Option.PRESET:
                    {
                        _field.Create(_playerSetup.PlayerFieldSize);
                        _field.SetPredefinedInitField();
                        break;
                    }
                case Option.RESTORE:
                    {
                        _field = _dataStorage.Restore(_playerSetup.PlayerName);
                        _field.Create();
                      
                        break;
                    }
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

            if (Console.KeyAvailable)    // console methods - need to move out
            {
                ConsoleKeyInfo keyPressed;
                keyPressed = Console.ReadKey(true);
                Console.SetCursorPosition(0, 0);

                switch (keyPressed.Key)
                {
                    case ConsoleKey.Escape:
                        {
                            EndGame();
                            return true;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            PauseGame(keyPressed);
                            break;
                        }
                    default:
                        break;
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
           
            do           // Console methods - need to move out! GetKeyPressed()? Enums for keys?
            {
                keyPressed = Console.ReadKey(true);

            } while (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Spacebar);

            if (keyPressed.Key == ConsoleKey.Enter)
            {
                SaveGame();
                RunTheGame(); // to implement
            }
        }

        public void SaveGame()
        {
            _dataStorage.Save(_playerSetup.PlayerName, _field);

            string saveGameMessage = $"~~~~~~~~~~~     Game for Player {_playerSetup.PlayerName} saved. ~~~~~~~~~~~";
            ModifyInfoBar(saveGameMessage);
            Thread.Sleep(2000);
        }

        private void ModifyInfoBar(string message)
        {
            _application.ShowFieldInfoBar(_field.Generation, _field.CountAliveCells(), message);
        }
        public bool RestartGame()
        {
            _application.ShowPreExitScreen();
            return true;
        }
    }
}
