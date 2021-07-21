using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife
{
    public class PlayerSetup : IPlayerSetup
    {
        public string PlayerName { get; set; }
        public int PlayerFieldSize { get; set; }
        public Option PlayerStartOption { get; set; }

        IApplication _application;
        IDataStorage _data; 
        public PlayerSetup(IApplication application, IDataStorage data)
        {
            _application = application;
            _data = data;
        }
        public void SetPlayersInput()
        {
            _application.WriteText("Welcome to the Game of Life!\n\n" +
                "PLAYER'S SETUP\n");

            _application.WriteText("Player's name:");
            PlayerName = GetValidatedNameInput();

           _application.WriteText($"Please choose game field set up for 0.Generation ({(int)Option.RANDOM} - for randomly filled, {(int)Option.PRESET} - pre-set, {(int)Option.RESTORE} - restore saved game): ");
             PlayerStartOption = GetValidatedOptionInput();
             
            if (PlayerStartOption != Option.RESTORE) 
            {
                _application.WriteText("Please input the size of the field(15 - 40 cells): ");
                PlayerFieldSize = GetValidatedDimensionInput();
            }
        }
        public string GetValidatedNameInput()
        {
            var input = _application.ReadInput();

            while (string.IsNullOrEmpty(input))
            {
                _application.ShowErrorMessage("Name is required!");
                
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
                    _application.ShowErrorMessage("Invalid input! Please input numbers only.");
                }
                else if (dimensionInput < 15 || dimensionInput > 40) // to implement hardcoded dim
                {
                    _application.ShowErrorMessage("Size is out of range!");
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
                    && Enum.IsDefined(typeof(Option), optionIndex);
                
                if (!isOptionValid)
                {
                    _application.ShowErrorMessage("Invalid input!");
                }

                if (optionIndex == (int)Option.RESTORE && !_data.DataExists(PlayerName))
                {
                        _application.ShowErrorMessage("No saved games found for this Player!");
                        isOptionValid = false;
                 }
            }
            return (Option)optionIndex;
        }
       
    }
}
