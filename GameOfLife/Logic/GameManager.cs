﻿using GameOfLife.Application;
using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.SaveGame;
using GameOfLife.Input;
using System;
using System.Threading;
using GameOfLife.Grid;

namespace GameOfLife
{
    public class GameManager : IGameManager
    {
        public PlayerInput PlayerInput { get; set; }
        public TextMessages Message { get; set; }
        IFieldManager _fieldManager;
        IPlayerInputCapture _inputCapture;
        IGameStorage _dataStorage;
        IApplication _application;
        IKeyControls _keyControls;

        public GameManager(IFieldManager fieldManager, IPlayerInputCapture inputCapture, IGameStorage dataStorage, IApplication application, IKeyControls keyControls)
        {
            Message = new TextMessages();
            try
            {
                _fieldManager = fieldManager;
                _inputCapture = inputCapture;
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
                CreatePlayerSetup();
                GetGameField();
                ShiftFieldGenerations();
                gameContinues = RestartGame();
                _application.ClearScreen();

            } while (gameContinues);
            Environment.Exit(0);
        }

        public void CreatePlayerSetup()
        {
           PlayerInput = _inputCapture.GetPlayersInput();

            _application.ClearScreen();  
        }

        public void GetGameField()
        {
            _fieldManager.SetUpField(PlayerInput.StartOption, PlayerInput.FieldSize, PlayerInput.PlayerName);
        }

        public void ShiftFieldGenerations()
        {
            bool canContinue = true;

            while (canContinue)
            {
                _application.ShowFieldInfoBar(_fieldManager.GetGeneration(), _fieldManager.CountAliveCells());
                _fieldManager.ViewField();
                Thread.Sleep(1000);
                canContinue = !IsActionRequired();
                
                _fieldManager.UpdateFieldData();
            }
        }

        public bool IsActionRequired()
        {
            if (_fieldManager.CountAliveCells() == 0)  
            {
                NotifyOfExtinction(); 
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

        public void NotifyOfExtinction() 
        {
            ModifyInfoBar(TextMessages.Extinction);
            Thread.Sleep(2000);
        }

        public void EndGame()
        {
            ModifyInfoBar(TextMessages.GameEnded);
            Thread.Sleep(2000);
        }
        public void PauseGame() 
        {
            ModifyInfoBar(TextMessages.Paused);
        }

        public void SaveGame()
        {
            _dataStorage.Save(PlayerInput.PlayerName, _fieldManager.GetField()); ;

            ModifyInfoBar($" Game for Player {PlayerInput.PlayerName} saved. ");
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
            _application.ShowFieldInfoBar(_fieldManager.GetGeneration(), _fieldManager.CountAliveCells(), message);
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
