using GameOfLife.Application;
using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife
{
    public class Setup : ISetup
    {
      public string PlayerName { get; set; }
        public int FieldSizeInput { get; set; }
        public Option StartOption { get; set; }
        public TextMessages Message { get; set; }

        IApplication _application;
        IGameStorage _storage;
      
        
        public Setup(IApplication application, IGameStorage storage)
        {
            _application = application;
            _storage = storage;
            Message = new TextMessages();
        }
        public void SetPlayersInput()
        {
            _application.WriteText(TextMessages.Welcome);

               _application.WriteText(TextMessages.AskName);
                PlayerName = GetValidatedNameInput(); ; 
           
            _application.WriteText(AskStartOption());
             StartOption = GetValidatedOptionInput();
             
            if (StartOption != Option.Restore) 
            {
                _application.WriteText(Message.AskFieldSize);
                FieldSizeInput = GetValidatedDimensionInput();
            }
        }
        public string GetValidatedNameInput()
        {
            var input = _application.ReadInput();

            while (string.IsNullOrEmpty(input))
            {
                _application.ShowErrorMessage(TextMessages.BlankName);
                
                input = _application.ReadInput();
            }
            return input;
        }
        public int GetValidatedDimensionInput()
        {
            var inputIsValid = false;
            var dimensionInput = 0;

            while (!inputIsValid)
            {
               if (!int.TryParse(_application.ReadInput(), out dimensionInput))
                {
                    _application.ShowErrorMessage(TextMessages.InvalidInput);
                }
                else if (dimensionInput < NumericData.FieldMinSize || dimensionInput > NumericData.FieldMaxSize) 
                {
                    _application.ShowErrorMessage(TextMessages.OutOfRange);
                }
                else
                {
                    inputIsValid = true;
                }
            }
            return dimensionInput;
        }
        public Option GetValidatedOptionInput()
        {
            var optionIndex = 0;
            var isOptionValid = false;

            while (!isOptionValid)
            {
                isOptionValid = (int.TryParse(_application.ReadInput(), out optionIndex))
                    && Enum.IsDefined(typeof(Option), optionIndex)
                    || (optionIndex == (int)Option.Restore && _storage.DataExists(PlayerName));
                
                if (!isOptionValid)
                {
                    _application.ShowErrorMessage(TextMessages.InvalidInput);
                }
            }
            return (Option)optionIndex;
        }

        public string AskStartOption()  
        {
            string output = "Please choose game field set up ";
           
            if (_storage.DataExists(PlayerName))
            {
                output += $"({ (int)Option.Random} - for randomly filled, { (int)Option.Preset} -pre-set, { (int)Option.Restore} - restore saved game): ";
            }
            else
            {
                output += $"({ (int)Option.Random} - for randomly filled, { (int)Option.Preset} -pre-set): ";
            }
            return output;
        }
    }
}
