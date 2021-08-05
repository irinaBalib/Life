﻿using GameOfLife.Application;
using GameOfLife.Enums;
using System;
using System.Threading;

namespace GameOfLife
{
    public class GameManager : IGameManager
    {
        public Message Message { get; set; }
        IField _field;
        ISetup _playerSetup;
        IDataStorage _dataStorage;
        IApplication _application;
        IKeyControls _keyControls;
        IPlayer _player;

        public GameManager(IField field, ISetup playerSetup, IDataStorage dataStorage, IApplication application, IKeyControls keyControls, IPlayer player)
        {
            Message = new Message();
            try
            {
                _player = player;
                _field = field;
                _playerSetup = playerSetup;
                _dataStorage = dataStorage;
                _application = application;
                _keyControls = keyControls;
            }
            catch (Exception e)
            {

                _application.WriteText(e.Message);
            }
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
            switch (_playerSetup.StartOption)
            {
                case Option.RANDOM:
                    {
                        _field.Create(_playerSetup.FieldSizeInput);  // call Create in init methods? Factory?
                        _field.SetRandomInitField();
                        break;
                    }
                case Option.PRESET:
                    {
                        _field.Create(_playerSetup.FieldSizeInput);
                        _field.SetPredefinedInitField();
                        break;
                    }
                case Option.RESTORE:
                    {
                       IField restoredField = _dataStorage.Restore(_player.Name);
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
            ModifyInfoBar(Message.Extinction);
            Thread.Sleep(2000);
        }

        public void EndGame()
        {
            ModifyInfoBar(Message.GameEnded);
            Thread.Sleep(2000);
        }
        public void PauseGame() 
        {
            ModifyInfoBar(Message.Paused);
        }

        public void SaveGame()
        {
            _dataStorage.Save(_player.Name, _field);

            ModifyInfoBar(Message.GameSaved(_player.Name));
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