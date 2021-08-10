using GameOfLife.Application;
using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.SaveGame;
using GameOfLife.Input;
using System;
using System.Threading;

namespace GameOfLife
{
    public class GameManager : IGameManager
    {
        public PlayerInput PlayerInput { get; set; }
        public TextMessages Message { get; set; }
        IField _field;
        IPlayerInputCapture _inputCapture;
        IGameStorage _dataStorage;
        IApplication _application;
        IKeyControls _keyControls;

        public GameManager(IField field, IPlayerInputCapture inputCapture, IGameStorage dataStorage, IApplication application, IKeyControls keyControls)
        {
            Message = new TextMessages();
            try
            {
                _field = field;
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
           PlayerInput = _inputCapture.GetPlayersInput();

            _application.ClearScreen();  
        }

        public void SetInitFieldState()
        {
            switch (PlayerInput.StartOption)
            {
                case Option.Random:
                    {
                        _field.Create(PlayerInput.FieldSize);  
                        _field.SetRandomInitField();
                        break;
                    }
                case Option.Preset:
                    {
                        _field.Create(PlayerInput.FieldSize);
                        _field.SetPredefinedInitField();
                        break;
                    }
                case Option.Restore:
                    {
                       IField restoredField = _dataStorage.Restore(PlayerInput.PlayerName);
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
            _dataStorage.Save(PlayerInput.PlayerName, _field);

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
