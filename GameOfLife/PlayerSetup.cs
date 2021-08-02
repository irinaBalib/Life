﻿using GameOfLife.Application;
using GameOfLife.Enums;
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
        public Message Message { get; set; }

        IApplication _application;
        IDataStorage _data; 
        public PlayerSetup(IApplication application, IDataStorage data)
        {
            _application = application;
            _data = data;
            Message = new Message();
        }
        public void SetPlayersInput()
        {
            _application.WriteText(Message.Welcome);

            _application.WriteText(Message.AskName);
            PlayerName = GetValidatedNameInput();

           _application.WriteText(Message.AskStartOption);
             PlayerStartOption = GetValidatedOptionInput();
             
            if (PlayerStartOption != Option.RESTORE) 
            {
                _application.WriteText(Message.AskFieldSize);
                PlayerFieldSize = GetValidatedDimensionInput();
            }
        }
        public string GetValidatedNameInput()
        {
            var input = _application.ReadInput();

            while (string.IsNullOrEmpty(input))
            {
                _application.ShowErrorMessage(Message.BlankName);
                
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
                    _application.ShowErrorMessage(Message.InvalidInput);
                }
                else if (dimensionInput < IField.MinSize || dimensionInput > IField.MaxSize) 
                {
                    _application.ShowErrorMessage(Message.OutOfRange);
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
                    _application.ShowErrorMessage(Message.InvalidInput);
                }

                if (optionIndex == (int)Option.RESTORE && !_data.DataExists(PlayerName))
                {
                        _application.ShowErrorMessage(Message.NoSavedGames);
                        isOptionValid = false;
                 }
            }
            return (Option)optionIndex;
        }
       
    }
}
