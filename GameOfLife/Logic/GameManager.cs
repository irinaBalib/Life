using GameOfLife.Application;
using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.SaveGame;
using GameOfLife.Input;
using System;
using System.Threading;
using GameOfLife.Grid;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GameOfLife
{
    public class GameManager : IGameManager
    {
        private PlayerInput PlayerInput;
        private List<IField> listOfFields;
       IFieldManager _fieldManager;
        IFieldFactory _factory;
        IPlayerInputCapture _inputCapture;
        IGameStorage _dataStorage;
        IApplication _application;
        IKeyControls _keyControls;

        public GameManager(IFieldFactory factory, IFieldManager fieldManager, IPlayerInputCapture inputCapture, IGameStorage dataStorage, IApplication application, IKeyControls keyControls)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _fieldManager = fieldManager ?? throw new ArgumentNullException(nameof(fieldManager));  
            _inputCapture = inputCapture ?? throw new ArgumentNullException(nameof(inputCapture));
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _keyControls = keyControls ?? throw new ArgumentNullException(nameof(keyControls));
        }
        public void RunTheGame()  
        {
            bool gameContinues;
            do
            { 
                CreatePlayerSetup();
                SetGameField();
                ShiftGenerations();
                
                gameContinues = RestartGame();
                _application.ClearScreen();

            } while (gameContinues);
        }

        private void CreatePlayerSetup()
        {
           PlayerInput = _inputCapture.GetPlayersInput();

            _application.ClearScreen();  
        }
        private void SetGameField()
        {
            listOfFields = new List<IField>();
            switch (PlayerInput.StartOption)
            {
                case Option.Random:
                    {
                        listOfFields.Add(_factory.BuildRandomField(PlayerInput.FieldSize));
                        break;
                    }
                case Option.Preset:
                    {
                        listOfFields.Add(_factory.BuildPresetField(PlayerInput.FieldSize));
                        break;
                    }
                case Option.Multiple:
                    {
                        for (int i = 0; i < NumericData.MultiFieldCount; i++)
                        {
                            listOfFields.Add(_factory.BuildRandomField(PlayerInput.FieldSize));
                        }
                        break;
                    }
                case Option.Restore:
                    {
                        RestoreGame();
                        break;
                    }
            }
           
        }
        private void ShiftGenerations()
        {
            bool canContinue = true;

            while (canContinue)
            {
                _application.ShowFieldInfoBar(GetGeneration(), GetLiveCellCount(), GetLiveFieldCount());
              
                RunGameField();
               
                Thread.Sleep(1000);
                canContinue = !IsActionRequired();
            }
        }

        private void RunGameField()
        {
            LoopFieldData();

            if (listOfFields.Count()>1)
            {
                PrintSelectedFields(NumericData.MultiFieldPrint);
            }
            else
            {
                PrintSelectedFields();
            }
        }
        private void PrintSelectedFields(int fieldCount=1) 
        {
            List<IField> selectedFields = listOfFields.GetRange(0,fieldCount);
           
            for (int i = 0; i < selectedFields.Count; i++)
            {
                _fieldManager.PrintCells(selectedFields[i], i);
            }
        }
        private void LoopFieldData()  
        {
            Parallel.ForEach(listOfFields, field =>
            {
                _fieldManager.CheckCellsForSurvival(field);
                _fieldManager.UpdateFieldData(field);
            });
        }
        private int GetLiveFieldCount()
        {
            var count = 0;
            foreach (IField field in listOfFields)
            {
                if (_fieldManager.CountAliveCells(field) > 0)
                {
                    count++;
                }
            }
            return count;
        }
        private int GetGeneration()
        {
            return listOfFields.FirstOrDefault().Generation;
        }
        private int GetLiveCellCount()
        {
            int liveCellCount = 0;

            foreach (IField field in listOfFields)
            {
                liveCellCount += _fieldManager.CountAliveCells(field);
            }
            return liveCellCount;
        }
        private bool IsActionRequired()
        {
            if (GetLiveCellCount() == 0)
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
        private void NotifyOfExtinction() 
        {
            ModifyInfoBar(TextMessages.Extinction);
            Thread.Sleep(2000);
        }
        private void EndGame()
        {
            ModifyInfoBar(TextMessages.GameEnded);
            Thread.Sleep(2000);
        }
        private void PauseGame() 
        {
            ModifyInfoBar(TextMessages.Paused);
        }
        private void RestoreGame() 
        {
            listOfFields = _dataStorage.Restore(PlayerInput.PlayerName);
        }
        private void SaveGame()
        {
            _dataStorage.Save(PlayerInput.PlayerName, listOfFields);
            
            ModifyInfoBar($" Game for Player {PlayerInput.PlayerName} saved. ");
            Thread.Sleep(2000);
        }
        private bool RestartGame()
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
            _application.ShowFieldInfoBar(GetGeneration(), GetLiveCellCount(), GetLiveFieldCount(), message);
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
