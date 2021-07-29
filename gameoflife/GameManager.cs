using GameOfLife.Application;
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
        IKeyControls _keyControls;

        public GameManager(IField field, IPlayerSetup playerSetup, IDataStorage dataStorage, IApplication application, IKeyControls keyControls)
        {
            _field = field;
            _playerSetup = playerSetup;
            _dataStorage = dataStorage;
            _application = application;
            _keyControls = keyControls;
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
                _application.ClearScreen();

            } while (gameContinues);
            Environment.Exit(0);
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
                       IField restoredField = _dataStorage.Restore(_playerSetup.PlayerName);
                        _field.Create(restoredField.Dimension, restoredField.CurrentCells, restoredField.Generation);
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

            if (_keyControls.KeyPressed())    
            {
                KeyAction keyPressed = _keyControls.GetKeyAction();

                switch (keyPressed)
                {
                    case KeyAction.Exit:
                        {
                            EndGame();
                            return true;
                        }
                    case KeyAction.PauseOnOff:
                        {
                            PauseGame();

                            if (IsGameSaveRequested())
                            {
                                return true;
                            }
                            return false;
                        }
                    default:
                        { return false; }
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
        public void PauseGame() 
        {
            string pauseMessage = "**PAUSED** Press SPACEBAR to resume or F12 to save & exit";
            ModifyInfoBar(pauseMessage);
        }

        public void SaveGame()
        {
            _dataStorage.Save(_playerSetup.PlayerName, _field);

            string saveGameMessage = $"~~~~~~~~~~~     Game for Player {_playerSetup.PlayerName} saved. ~~~~~~~~~~~";
            ModifyInfoBar(saveGameMessage);
            Thread.Sleep(2000);
        }
        public bool RestartGame()
        {
            _application.ShowPreExitScreen();

            if (_keyControls.GetKeyAction() == KeyAction.Restart)
            {
                return true;
            }
            return false;
        }
        private void ModifyInfoBar(string message)
        {
            _application.ShowFieldInfoBar(_field.Generation, _field.CountAliveCells(), message);
        }

        private bool IsGameSaveRequested()
        {
            KeyAction keyPressed;
            do
            {
                keyPressed = _keyControls.GetKeyAction();

            } while (keyPressed != KeyAction.SaveAndExit && keyPressed != KeyAction.PauseOnOff);

            if (keyPressed == KeyAction.SaveAndExit)
            {
                SaveGame();
                return true;
            }
            return false;
        }
       
    }
}
